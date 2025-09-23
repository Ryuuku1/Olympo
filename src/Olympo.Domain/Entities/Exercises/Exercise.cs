using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Equipments;
using Olympo.Domain.Entities.MuscleGroups;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Entities.Exercises;

public class Exercise : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? EquipmentId { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public ExerciseCategory Category { get; set; } = ExerciseCategory.Undefined;
    public ExerciseDifficulty Difficulty { get; set; } = ExerciseDifficulty.Undefined;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Equipment? Equipment { get; set; }
    public ICollection<MuscleGroup> MuscleGroups { get; set; } = [];
    public ICollection<Workout> Workouts { get; set; } = [];
    public ICollection<PersonalRecord> PersonalRecords { get; set; } = [];
}