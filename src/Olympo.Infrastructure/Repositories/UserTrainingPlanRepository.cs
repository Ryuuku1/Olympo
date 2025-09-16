using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Infrastructure.Data;

namespace Olympo.Infrastructure.Repositories;

public class UserTrainingPlanRepository : IUserTrainingPlanRepository
{
    private readonly DatabaseContext _context;

    public UserTrainingPlanRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserTrainingPlan?> GetByIdAsync(Guid id)
    {
        return await _context.UserTrainingPlans.FindAsync(id);
    }

    public async Task<UserTrainingPlan?> GetActiveByUserIdAsync(Guid userId)
    {
        return await _context.UserTrainingPlans
            .FirstOrDefaultAsync(utp => utp.UserId == userId && utp.IsActive);
    }

    public async Task<UserTrainingPlan?> GetByUserIdWithDetailsAsync(Guid userId)
    {
        return await _context.UserTrainingPlans
            .Include(utp => utp.TrainingPlan)
                .ThenInclude(tp => tp.WeeklyPlans)
                    .ThenInclude(pw => pw.DailyPlans)
                        .ThenInclude(pd => pd.Workouts)
                            .ThenInclude(w => w.Exercise)
            .FirstOrDefaultAsync(utp => utp.UserId == userId && utp.IsActive);
    }

    public async Task<IEnumerable<UserTrainingPlan>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserTrainingPlans
            .Include(utp => utp.TrainingPlan)
            .Where(utp => utp.UserId == userId)
            .OrderByDescending(utp => utp.AssignedAt)
            .ToListAsync();
    }

    public async Task AddAsync(UserTrainingPlan userTrainingPlan)
    {
        await _context.UserTrainingPlans.AddAsync(userTrainingPlan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserTrainingPlan userTrainingPlan)
    {
        _context.UserTrainingPlans.Update(userTrainingPlan);
        await _context.SaveChangesAsync();
    }

    public async Task DeactivateAsync(Guid id)
    {
        var userTrainingPlan = await _context.UserTrainingPlans.FindAsync(id);
        if (userTrainingPlan != null)
        {
            userTrainingPlan.IsActive = false;
            userTrainingPlan.EndDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
