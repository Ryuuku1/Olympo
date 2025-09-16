using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Infrastructure.Data;

namespace Olympo.Infrastructure.Repositories;

public class TrainingPlanRepository : ITrainingPlanRepository
{
    private readonly DatabaseContext _context;

    public TrainingPlanRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<TrainingPlan?> GetByIdAsync(Guid id)
    {
        return await _context.TrainingPlans.FindAsync(id);
    }

    public async Task<TrainingPlan?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.TrainingPlans
            .Include(tp => tp.WeeklyPlans)
                .ThenInclude(pw => pw.DailyPlans)
                    .ThenInclude(pd => pd.Workouts)
                        .ThenInclude(w => w.Exercise)
            .FirstOrDefaultAsync(tp => tp.Id == id);
    }

    public async Task<IEnumerable<TrainingPlan>> GetAllAsync()
    {
        return await _context.TrainingPlans
            .OrderBy(tp => tp.Name)
            .ToListAsync();
    }

    public async Task AddAsync(TrainingPlan trainingPlan)
    {
        await _context.TrainingPlans.AddAsync(trainingPlan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TrainingPlan trainingPlan)
    {
        trainingPlan.UpdatedAt = DateTime.UtcNow;
        _context.TrainingPlans.Update(trainingPlan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var trainingPlan = await _context.TrainingPlans.FindAsync(id);
        if (trainingPlan != null)
        {
            _context.TrainingPlans.Remove(trainingPlan);
            await _context.SaveChangesAsync();
        }
    }
}
