using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Users;

namespace Olympo.Domain.Entities.TrainingPlans;

public class UserTrainingPlan : Entity
{
    public Guid UserId { get; set; }
    public Guid TrainingPlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public TrainingPlan TrainingPlan { get; set; } = null!;   
}