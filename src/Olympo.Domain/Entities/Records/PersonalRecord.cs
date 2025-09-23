using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.Users;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Entities.Records;

public class PersonalRecord : Entity
{
    public Guid UserId { get; set; }
    public Guid ExerciseId { get; set; }
    public decimal Weight { get; set; }
    public int Repetitions { get; set; }
    public DateTime AchievedAt { get; set; }
    public Guid WorkoutSessionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
    public WorkoutSession WorkoutSession { get; set; } = null!;
}