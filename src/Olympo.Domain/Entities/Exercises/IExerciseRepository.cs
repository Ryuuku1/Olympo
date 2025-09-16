namespace Olympo.Domain.Entities.Exercises;

public interface IExerciseRepository
{
    Task<Exercise?> GetByIdAsync(Guid id);
    Task<IEnumerable<Exercise>> GetAllAsync();
    Task AddAsync(Exercise exercise);
    Task UpdateAsync(Exercise exercise);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
}