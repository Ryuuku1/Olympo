namespace Olympo.Domain.Entities.MuscleGroups;

public interface IMuscleGroupRepository
{
    Task<MuscleGroup?> GetByIdAsync(Guid id);
    Task<MuscleGroup?> GetByNameAsync(string name);
    Task<IEnumerable<MuscleGroup>> GetAllAsync();
    Task AddAsync(MuscleGroup muscleGroup);
    Task UpdateAsync(MuscleGroup muscleGroup);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
}
