using Olympo.Domain.Abstractions;

namespace Olympo.Domain.Entities.TrainingPlans;

public class WeeklyPlan : Entity
{
    public Guid TrainingPlanId { get; set; }
    public int WeekNumber { get; set; }
    public string Notes { get; set; } = string.Empty;

    public TrainingPlan TrainingPlan { get; set; } = null!;
    public ICollection<DailyPlan> DailyPlans { get; set; } = [];
}