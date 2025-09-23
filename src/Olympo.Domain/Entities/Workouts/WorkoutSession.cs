using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Users;

namespace Olympo.Domain.Entities.Workouts;

public class WorkoutSession : Entity
{
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public TimeSpan? Duration { get; set; }
    public string Notes { get; set; } = string.Empty;
    public WorkoutSessionStatus Status { get; set; } = WorkoutSessionStatus.NotStarted;
    public int? ActualSets { get; set; }
    public int? ActualReps { get; set; }
    public decimal? ActualWeight { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Workout Workout { get; set; } = null!;
    public ICollection<PersonalRecord> PersonalRecords { get; set; } = [];
}