using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Entities.Users;

public class User : Entity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public FitnessLevel? FitnessLevel { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<UserTrainingPlan> UserTrainingPlans { get; set; } = [];
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = [];
    public ICollection<PersonalRecord> PersonalRecords { get; set; } = [];
}