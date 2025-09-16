namespace Olympo.Application.Features.WorkoutSessions;

public class UpdateSetRequest
{
    public int SetNumber { get; set; }
    public int Repetitions { get; set; }
    public decimal Load { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class UpdateSetResponse
{
    public Guid SessionId { get; set; }
    public Guid SetRecordId { get; set; }
    public int SetNumber { get; set; }
    public int Repetitions { get; set; }
    public decimal Load { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CompletedAt { get; set; }
    public string Message { get; set; } = string.Empty;
}
