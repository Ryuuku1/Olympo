using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Application.Features.WorkoutSessions;

public class CompleteSessionHandler
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly ICurrentUserService _currentUserService;

    public CompleteSessionHandler(
        IWorkoutSessionRepository workoutSessionRepository,
        ICurrentUserService currentUserService)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CompleteSessionResponse> HandleAsync(Guid sessionId, CompleteSessionRequest request)
    {
        var session = await _workoutSessionRepository.GetByIdWithDetailsAsync(sessionId);
        
        if (session == null)
        {
            throw new ArgumentException("Workout session not found.");
        }

        if (session.UserId != _currentUserService.UserId)
        {
            throw new UnauthorizedAccessException("You can only complete your own workout sessions.");
        }

        if (session.Status != WorkoutSessionStatus.InProgress && session.Status != WorkoutSessionStatus.Paused)
        {
            throw new InvalidOperationException("Session is already completed or cancelled.");
        }

        // Complete the session
        session.EndDateTime = DateTime.UtcNow;
        session.Status = WorkoutSessionStatus.Completed;
        session.Notes = request.Notes;
        
        // Calculate total duration accounting for pauses
        session.TotalActiveDuration = session.EndDateTime.Value - session.StartDateTime;

        // Check for personal records
        PersonalRecordInfo? newPersonalRecord = null;
        if (session.SetRecords.Any())
        {
            // Find the best set (highest load) in this session
            var bestSet = session.SetRecords
                .OrderByDescending(sr => sr.Load)
                .ThenByDescending(sr => sr.Repetitions)
                .First();

            // Check current personal record
            var currentPR = await _workoutSessionRepository.GetPersonalRecordAsync(
                session.UserId, 
                session.Workout.ExerciseId);

            bool isNewRecord = false;
            decimal previousLoad = 0;

            if (currentPR == null)
            {
                // First time doing this exercise
                isNewRecord = true;
            }
            else if (bestSet.Load > currentPR.Load || 
                    (bestSet.Load == currentPR.Load && bestSet.Repetitions > currentPR.Repetitions))
            {
                // New personal record achieved
                isNewRecord = true;
                previousLoad = currentPR.Load;
            }

            if (isNewRecord)
            {
                var personalRecord = new PersonalRecord
                {
                    Id = Guid.NewGuid(),
                    UserId = session.UserId,
                    ExerciseId = session.Workout.ExerciseId,
                    Load = bestSet.Load,
                    Repetitions = bestSet.Repetitions,
                    AchievedAt = DateTime.UtcNow,
                    WorkoutSessionId = sessionId
                };

                if (currentPR == null)
                {
                    await _workoutSessionRepository.AddPersonalRecordAsync(personalRecord);
                }
                else
                {
                    currentPR.Load = bestSet.Load;
                    currentPR.Repetitions = bestSet.Repetitions;
                    currentPR.AchievedAt = DateTime.UtcNow;
                    currentPR.WorkoutSessionId = sessionId;
                    await _workoutSessionRepository.UpdatePersonalRecordAsync(currentPR);
                }

                newPersonalRecord = new PersonalRecordInfo
                {
                    ExerciseId = session.Workout.ExerciseId,
                    ExerciseName = session.Workout.Exercise.Name,
                    Load = bestSet.Load,
                    Repetitions = bestSet.Repetitions,
                    PreviousLoad = previousLoad,
                    AchievedAt = DateTime.UtcNow
                };
            }
        }

        await _workoutSessionRepository.UpdateAsync(session);

        return new CompleteSessionResponse
        {
            SessionId = sessionId,
            StartDateTime = session.StartDateTime,
            EndDateTime = session.EndDateTime.Value,
            TotalDuration = session.TotalActiveDuration.Value,
            CompletedSets = session.SetRecords.Count,
            ExerciseName = session.Workout.Exercise.Name,
            NewPersonalRecord = newPersonalRecord,
            Message = newPersonalRecord != null ? 
                "Workout completed! Congratulations on your new personal record!" : 
                "Workout completed successfully!"
        };
    }
}
