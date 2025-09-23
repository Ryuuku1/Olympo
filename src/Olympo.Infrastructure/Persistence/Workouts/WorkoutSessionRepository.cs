using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Infrastructure.Persistence.Workouts;

public class WorkoutSessionRepository : IWorkoutSessionRepository
{
    private readonly DatabaseContext _context;

    public WorkoutSessionRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<WorkoutSession?> GetByIdAsync(Guid id)
    {
        return await _context.WorkoutSessions.FindAsync(id);
    }

    public async Task<WorkoutSession?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.WorkoutSessions
            .Include(ws => ws.Workout)
                .ThenInclude(w => w.Exercise)
            .FirstOrDefaultAsync(ws => ws.Id == id);
    }

    public async Task<IEnumerable<WorkoutSession>> GetByUserIdAsync(Guid userId)
    {
        return await _context.WorkoutSessions
            .Include(ws => ws.Workout)
                .ThenInclude(w => w.Exercise)
            .Where(ws => ws.UserId == userId)
            .OrderByDescending(ws => ws.StartDateTime)
            .ToListAsync();
    }

    public async Task<WorkoutSession?> GetActiveSessionByUserIdAsync(Guid userId)
    {
        return await _context.WorkoutSessions
            .Include(ws => ws.Workout)
                .ThenInclude(w => w.Exercise)
            .FirstOrDefaultAsync(ws => ws.UserId == userId && 
                (ws.Status == WorkoutSessionStatus.InProgress || ws.Status == WorkoutSessionStatus.Paused));
    }

    public async Task AddAsync(WorkoutSession session)
    {
        await _context.WorkoutSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkoutSession session)
    {
        _context.WorkoutSessions.Update(session);
        await _context.SaveChangesAsync();
    }
}
