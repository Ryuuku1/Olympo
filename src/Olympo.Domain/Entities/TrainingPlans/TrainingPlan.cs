using Olympo.Domain.Abstractions;

namespace Olympo.Domain.Entities.TrainingPlans;

public class TrainingPlan : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationWeeks { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<WeeklyPlan> WeeklyPlans { get; set; } = [];
    public ICollection<UserTrainingPlan> UserTrainingPlans { get; set; } = [];
}