namespace Olympo.Application.Features.WorkoutSessions;

public class StartSessionRequest
{
    public Guid WorkoutId { get; set; }
}

public class StartSessionResponse
{
    public Guid SessionId { get; set; }
    public Guid WorkoutId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int PlannedSets { get; set; }
    public int PlannedReps { get; set; }
    public decimal? PlannedLoad { get; set; }
    public string Guide { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; }
    public string Status { get; set; } = string.Empty;
}
