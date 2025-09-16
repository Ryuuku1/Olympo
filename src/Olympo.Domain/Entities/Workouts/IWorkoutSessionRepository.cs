namespace Olympo.Domain.Entities.Workouts;

public interface IWorkoutSessionRepository
{
    Task<WorkoutSession?> GetByIdAsync(Guid id);
    Task<WorkoutSession?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<WorkoutSession>> GetByUserIdAsync(Guid userId);
    Task<WorkoutSession?> GetActiveSessionByUserIdAsync(Guid userId);
    Task AddAsync(WorkoutSession session);
    Task UpdateAsync(WorkoutSession session);
}