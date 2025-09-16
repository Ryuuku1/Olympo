namespace Olympo.Domain.Entities.TrainingPlans;

public interface IUserTrainingPlanRepository
{
    Task<UserTrainingPlan?> GetByIdAsync(Guid id);
    Task<UserTrainingPlan?> GetActiveByUserIdAsync(Guid userId);
    Task<UserTrainingPlan?> GetByUserIdWithDetailsAsync(Guid userId);
    Task<IEnumerable<UserTrainingPlan>> GetByUserIdAsync(Guid userId);
    Task AddAsync(UserTrainingPlan userTrainingPlan);
    Task UpdateAsync(UserTrainingPlan userTrainingPlan);
    Task DeactivateAsync(Guid id);
}