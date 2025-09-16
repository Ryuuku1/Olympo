using Microsoft.Extensions.Caching.Memory;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Exercises;

namespace Olympo.Application.Features.Exercises;

public class GetExercisesRequest
{
    // No parameters needed for getting all exercises
}

public class GetExercisesResponse
{
    public List<ExerciseDto> Exercises { get; set; } = new();
}

public class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MuscleGroups { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}

public class GetExercisesHandler
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMemoryCache _cache;
    private const string ExercisesCacheKey = "all_exercises";
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public GetExercisesHandler(IExerciseRepository exerciseRepository, IMemoryCache cache)
    {
        _exerciseRepository = exerciseRepository;
        _cache = cache;
    }

    public async Task<GetExercisesResponse> HandleAsync()
    {
        // Try to get from cache first
        if (_cache.TryGetValue(ExercisesCacheKey, out GetExercisesResponse? cachedResponse))
        {
            return cachedResponse!;
        }

        // If not in cache, get from database
        var exercises = await _exerciseRepository.GetAllAsync();
        
        var response = new GetExercisesResponse
        {
            Exercises = exercises.Select(e => new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                MuscleGroups = e.MuscleGroups,
                Equipment = e.Equipment,
                Instructions = e.Instructions
            }).ToList()
        };

        // Cache the response
        _cache.Set(ExercisesCacheKey, response, _cacheDuration);

        return response;
    }
}
