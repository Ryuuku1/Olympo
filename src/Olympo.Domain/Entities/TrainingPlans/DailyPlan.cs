using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Entities.TrainingPlans;

public class DailyPlan : Entity
{
    public Guid WeeklyPlanId { get; set; }
    public int DayNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public WeeklyPlan WeeklyPlan { get; set; } = null!;
    public ICollection<Workout> Workouts { get; set; } = [];
}