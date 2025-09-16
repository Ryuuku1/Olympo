using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Entities.Exercises;

public class Exercise : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MuscleGroups { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    public ICollection<PersonalRecord> PersonalRecords { get; set; } = new List<PersonalRecord>();
}