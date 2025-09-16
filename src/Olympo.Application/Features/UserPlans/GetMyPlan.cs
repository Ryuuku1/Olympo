using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Application.Features.UserPlans;

public class GetMyPlanRequest
{
    // No parameters needed - uses current user from JWT
}

public class GetMyPlanResponse
{
    public UserTrainingPlanDto? CurrentPlan { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class UserTrainingPlanDto
{
    public Guid Id { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public string PlanDescription { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int DurationWeeks { get; set; }
    public List<PlanWeekDto> Weeks { get; set; } = new();
}

public class PlanWeekDto
{
    public int WeekNumber { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<PlanDayDto> Days { get; set; } = new();
}

public class PlanDayDto
{
    public int DayNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<WorkoutDto> Workouts { get; set; } = new();
}

public class WorkoutDto
{
    public Guid Id { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? Load { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Guide { get; set; } = string.Empty;
    public int Order { get; set; }
}

public class GetMyPlanHandler
{
    private readonly IUserTrainingPlanRepository _userTrainingPlanRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetMyPlanHandler(
        IUserTrainingPlanRepository userTrainingPlanRepository,
        ICurrentUserService currentUserService)
    {
        _userTrainingPlanRepository = userTrainingPlanRepository;
        _currentUserService = currentUserService;
    }

    public async Task<GetMyPlanResponse> HandleAsync()
    {
        var userPlan = await _userTrainingPlanRepository.GetByUserIdWithDetailsAsync(_currentUserService.UserId);

        if (userPlan == null)
        {
            return new GetMyPlanResponse
            {
                CurrentPlan = null,
                Message = "You don't have an active training plan assigned."
            };
        }

        var planDto = new UserTrainingPlanDto
        {
            Id = userPlan.Id,
            PlanName = userPlan.TrainingPlan.Name,
            PlanDescription = userPlan.TrainingPlan.Description,
            StartDate = userPlan.StartDate,
            DurationWeeks = userPlan.TrainingPlan.DurationWeeks,
            Weeks = userPlan.TrainingPlan.WeeklyPlans
                .OrderBy(pw => pw.WeekNumber)
                .Select(pw => new PlanWeekDto
                {
                    WeekNumber = pw.WeekNumber,
                    Notes = pw.Notes,
                    Days = pw.DailyPlans
                        .OrderBy(pd => pd.DayNumber)
                        .Select(pd => new PlanDayDto
                        {
                            DayNumber = pd.DayNumber,
                            Name = pd.Name,
                            Notes = pd.Notes,
                            Workouts = pd.Workouts
                                .OrderBy(w => w.Order)
                                .Select(w => new WorkoutDto
                                {
                                    Id = w.Id,
                                    ExerciseName = w.Exercise.Name,
                                    Sets = w.Sets,
                                    Reps = w.Reps,
                                    Load = w.Load,
                                    Notes = w.Notes,
                                    Guide = w.Guide,
                                    Order = w.Order
                                }).ToList()
                        }).ToList()
                }).ToList()
        };

        return new GetMyPlanResponse
        {
            CurrentPlan = planDto,
            Message = "Current training plan retrieved successfully."
        };
    }
}
