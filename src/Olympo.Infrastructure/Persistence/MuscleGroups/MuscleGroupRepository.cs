using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities.MuscleGroups;

namespace Olympo.Infrastructure.Persistence.MuscleGroups;

public class MuscleGroupRepository : IMuscleGroupRepository
{
    private readonly DatabaseContext _context;

    public MuscleGroupRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<MuscleGroup?> GetByIdAsync(Guid id)
    {
        return await _context.MuscleGroups
            .Include(mg => mg.Exercises)
            .FirstOrDefaultAsync(mg => mg.Id == id);
    }

    public async Task<MuscleGroup?> GetByNameAsync(string name)
    {
        return await _context.MuscleGroups
            .Include(mg => mg.Exercises)
            .FirstOrDefaultAsync(mg => mg.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<IEnumerable<MuscleGroup>> GetAllAsync()
    {
        return await _context.MuscleGroups
            .Include(mg => mg.Exercises)
            .OrderBy(mg => mg.Name)
            .ToListAsync();
    }

    public async Task AddAsync(MuscleGroup muscleGroup)
    {
        await _context.MuscleGroups.AddAsync(muscleGroup);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MuscleGroup muscleGroup)
    {
        _context.MuscleGroups.Update(muscleGroup);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var muscleGroup = await _context.MuscleGroups.FindAsync(id);
        if (muscleGroup != null)
        {
            _context.MuscleGroups.Remove(muscleGroup);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.MuscleGroups
            .AnyAsync(mg => mg.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }
}
