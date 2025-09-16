using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Application.Features.WorkoutSessions;

public class UpdateSetHandler
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateSetHandler(
        IWorkoutSessionRepository workoutSessionRepository,
        ICurrentUserService currentUserService)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<UpdateSetResponse> HandleAsync(Guid sessionId, UpdateSetRequest request)
    {
        var session = await _workoutSessionRepository.GetByIdWithDetailsAsync(sessionId);
        
        if (session == null)
        {
            throw new ArgumentException("Workout session not found.");
        }

        if (session.UserId != _currentUserService.UserId)
        {
            throw new UnauthorizedAccessException("You can only update your own workout sessions.");
        }

        if (session.Status != WorkoutSessionStatus.InProgress)
        {
            throw new InvalidOperationException("Can only update sets for sessions in progress.");
        }

        // Find existing set record or create new one
        var existingSetRecord = session.SetRecords
            .FirstOrDefault(sr => sr.SetNumber == request.SetNumber);

        if (existingSetRecord != null)
        {
            // Update existing set
            existingSetRecord.Repetitions = request.Repetitions;
            existingSetRecord.Load = request.Load;
            existingSetRecord.Notes = request.Notes;
            existingSetRecord.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            // Create new set record
            var setRecord = new WorkoutSessionRecord
            {
                Id = Guid.NewGuid(),
                WorkoutSessionId = sessionId,
                SetNumber = request.SetNumber,
                Repetitions = request.Repetitions,
                Load = request.Load,
                Notes = request.Notes,
                CompletedAt = DateTime.UtcNow
            };
            
            session.SetRecords.Add(setRecord);
            existingSetRecord = setRecord;
        }

        await _workoutSessionRepository.UpdateAsync(session);

        return new UpdateSetResponse
        {
            SessionId = sessionId,
            SetRecordId = existingSetRecord.Id,
            SetNumber = existingSetRecord.SetNumber,
            Repetitions = existingSetRecord.Repetitions,
            Load = existingSetRecord.Load,
            Notes = existingSetRecord.Notes,
            CompletedAt = existingSetRecord.CompletedAt,
            Message = "Set updated successfully."
        };
    }
}
