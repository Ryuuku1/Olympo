using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Domain.Entities.Workouts;

public class Workout : Entity
{
    public Guid DailyPlanId { get; set; }
    public Guid ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? Load { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Guide { get; set; } = string.Empty;
    public int? Order { get; set; }

    public DailyPlan DailyPlan { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = [];
}