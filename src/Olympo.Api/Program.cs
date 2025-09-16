using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Olympo.Application.Features.Authentication;
using Olympo.Application.Features.WorkoutSessions;
using Olympo.Application.Features.Exercises;
using Olympo.Application.Features.UserPlans;
using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Users;
using Olympo.Domain.Entities.Workouts;
using Olympo.Infrastructure.Data;
using Olympo.Infrastructure.Repositories;
using Olympo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository registrations
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();
builder.Services.AddScoped<IUserTrainingPlanRepository, UserTrainingPlanRepository>();
builder.Services.AddScoped<IWorkoutSessionRepository, WorkoutSessionRepository>();

// Service registrations
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();

// Handler registrations
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<StartSessionHandler>();
builder.Services.AddScoped<UpdateSetHandler>();
builder.Services.AddScoped<CompleteSessionHandler>();
builder.Services.AddScoped<GetExercisesHandler>();
builder.Services.AddScoped<GetMyPlanHandler>();

// Memory cache for exercises
builder.Services.AddMemoryCache();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"] ?? "Olympo",
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"] ?? "OlympoUsers",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Authentication endpoints
app.MapPost("/api/auth/register", async (RegisterRequest request, RegisterHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync(request);
        return Results.Ok(response);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/api/auth/login", async (LoginRequest request, LoginHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync(request);
        return Results.Ok(response);
    }
    catch (UnauthorizedAccessException ex)
    {
        return Results.Unauthorized();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Exercise endpoints (cached)
app.MapGet("/api/exercises", async (GetExercisesHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync();
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();

// User Plan endpoints
app.MapGet("/api/myplan", async (GetMyPlanHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync();
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();

// Workout Session endpoints
app.MapPost("/api/sessions/{workoutId}/start", async (Guid workoutId, StartSessionRequest request, StartSessionHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync(workoutId, request);
        return Results.Ok(response);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();

app.MapPut("/api/sessions/{sessionId}", async (Guid sessionId, UpdateSetRequest request, UpdateSetHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync(sessionId, request);
        return Results.Ok(response);
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
    catch (UnauthorizedAccessException ex)
    {
        return Results.Forbid();
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();

app.MapPost("/api/sessions/{sessionId}/complete", async (Guid sessionId, CompleteSessionRequest request, CompleteSessionHandler handler) =>
{
    try
    {
        var response = await handler.HandleAsync(sessionId, request);
        return Results.Ok(response);
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
    catch (UnauthorizedAccessException ex)
    {
        return Results.Forbid();
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await context.Database.EnsureCreatedAsync();
}

app.Run();
