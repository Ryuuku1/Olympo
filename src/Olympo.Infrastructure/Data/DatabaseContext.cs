using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Users;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Infrastructure.Data;

public class DatabaseContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    private readonly ConcurrentDictionary<Guid, IDbContextTransaction> _transactions = new();

    public DbSet<User> Users { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<TrainingPlan> TrainingPlans { get; set; }
    public DbSet<WeeklyPlan> WeeklyPlans { get; set; }
    public DbSet<DailyPlan> DailyPlans { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<UserTrainingPlan> UserTrainingPlans { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }
    public DbSet<PersonalRecord> PersonalRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<Guid> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (IsInMemoryDb()) return Guid.Empty;

        var transactionId = Guid.NewGuid();
        var transaction = await Database.BeginTransactionAsync(cancellationToken);

        if (!_transactions.TryAdd(transactionId, transaction))
        {
            throw new InvalidOperationException("Transaction could not be added to the dictionary.");
        }

        return transactionId;
    }

    public async Task CommitTransactionAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        if (IsInMemoryDb()) return;

        if (!_transactions.TryRemove(transactionId, out var transaction))
        {
            throw new InvalidOperationException("No transaction is active.");
        }

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        if (IsInMemoryDb()) return;

        if (!_transactions.TryRemove(transactionId, out var transaction))
        {
            throw new InvalidOperationException("No transaction is active.");
        }

        await transaction.RollbackAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private bool IsInMemoryDb()
    {
        return Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
    }
}
