namespace Olympo.Application.Features.WorkoutSessions;

public class CompleteSessionRequest
{
    public string Notes { get; set; } = string.Empty;
}

public class CompleteSessionResponse
{
    public Guid SessionId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public int CompletedSets { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public PersonalRecordInfo? NewPersonalRecord { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class PersonalRecordInfo
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public decimal Load { get; set; }
    public int Repetitions { get; set; }
    public decimal PreviousLoad { get; set; }
    public DateTime AchievedAt { get; set; }
}
