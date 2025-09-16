namespace Olympo.Domain.Entities.TrainingPlans;

public interface ITrainingPlanRepository
{
    Task<TrainingPlan?> GetByIdAsync(Guid id);
    Task<TrainingPlan?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<TrainingPlan>> GetAllAsync();
    Task AddAsync(TrainingPlan trainingPlan);
    Task UpdateAsync(TrainingPlan trainingPlan);
    Task DeleteAsync(Guid id);
}