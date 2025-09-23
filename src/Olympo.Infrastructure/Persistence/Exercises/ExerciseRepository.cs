using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities.Exercises;

namespace Olympo.Infrastructure.Persistence.Exercises;

public class ExerciseRepository : IExerciseRepository
{
    private readonly DatabaseContext _context;

    public ExerciseRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Exercise?> GetByIdAsync(Guid id)
    {
        return await _context.Exercises.FindAsync(id);
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync()
    {
        return await _context.Exercises
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Exercise exercise)
    {
        await _context.Exercises.AddAsync(exercise);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise exercise)
    {
        _context.Exercises.Update(exercise);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Exercises
            .AnyAsync(e => e.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }
}
