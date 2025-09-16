using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Application.Features.WorkoutSessions;

public class StartSessionHandler
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly ICurrentUserService _currentUserService;

    public StartSessionHandler(
        IWorkoutSessionRepository workoutSessionRepository,
        ICurrentUserService currentUserService)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<StartSessionResponse> HandleAsync(Guid workoutId, StartSessionRequest request)
    {
        // Check if user already has an active session
        var existingSession = await _workoutSessionRepository.GetActiveSessionByUserIdAsync(_currentUserService.UserId);
        if (existingSession != null)
        {
            throw new InvalidOperationException("You already have an active workout session. Please complete or cancel it first.");
        }

        // Get workout details would need to be implemented via a workout repository
        // For now, creating the session
        var session = new WorkoutSession
        {
            Id = Guid.NewGuid(),
            UserId = _currentUserService.UserId,
            WorkoutId = workoutId,
            StartDateTime = DateTime.UtcNow,
            Status = WorkoutSessionStatus.InProgress,
            Notes = string.Empty
        };

        await _workoutSessionRepository.AddAsync(session);

        return new StartSessionResponse
        {
            SessionId = session.Id,
            WorkoutId = workoutId,
            StartDateTime = session.StartDateTime,
            Status = session.Status.ToString()
        };
    }
}
