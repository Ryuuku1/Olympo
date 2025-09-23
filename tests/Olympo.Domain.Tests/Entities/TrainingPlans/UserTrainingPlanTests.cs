using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Users;

namespace Olympo.Domain.Tests.Entities.TrainingPlans;

public class UserTrainingPlanTests
{
    [Fact]
    public void UserTrainingPlan_ShouldInitializeWithDefaultValues()
    {
        var userTrainingPlan = new UserTrainingPlan();

        userTrainingPlan.Id.ShouldBe(Guid.Empty);
        userTrainingPlan.UserId.ShouldBe(Guid.Empty);
        userTrainingPlan.TrainingPlanId.ShouldBe(Guid.Empty);
        userTrainingPlan.StartDate.ShouldBe(DateTime.MinValue);
        userTrainingPlan.EndDate.ShouldBeNull();
        userTrainingPlan.IsActive.ShouldBeTrue();
        userTrainingPlan.AssignedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        userTrainingPlan.User.ShouldBeNull();
        userTrainingPlan.TrainingPlan.ShouldBeNull();
    }

    [Fact]
    public void UserTrainingPlan_ShouldSetPropertiesCorrectly()
    {
        var userId = Guid.NewGuid();
        var trainingPlanId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(1);
        var endDate = DateTime.UtcNow.AddDays(56);
        var assignedAt = DateTime.UtcNow;

        var userTrainingPlan = new UserTrainingPlan
        {
            UserId = userId,
            TrainingPlanId = trainingPlanId,
            StartDate = startDate,
            EndDate = endDate,
            IsActive = false,
            AssignedAt = assignedAt
        };

        userTrainingPlan.UserId.ShouldBe(userId);
        userTrainingPlan.TrainingPlanId.ShouldBe(trainingPlanId);
        userTrainingPlan.StartDate.ShouldBe(startDate);
        userTrainingPlan.EndDate.ShouldBe(endDate);
        userTrainingPlan.IsActive.ShouldBeFalse();
        userTrainingPlan.AssignedAt.ShouldBe(assignedAt);
    }

    [Fact]
    public void UserTrainingPlan_ShouldAllowNavigationPropertiesAssignment()
    {
        var userTrainingPlan = new UserTrainingPlan();
        var user = new User { Email = "test@example.com", FirstName = "John", LastName = "Doe" };
        var trainingPlan = new TrainingPlan { Name = "Beginner Plan", DurationWeeks = 8 };

        userTrainingPlan.User = user;
        userTrainingPlan.TrainingPlan = trainingPlan;

        userTrainingPlan.User.ShouldBe(user);
        userTrainingPlan.TrainingPlan.ShouldBe(trainingPlan);
    }

    [Fact]
    public void UserTrainingPlan_ShouldCalculateTrainingDurationCorrectly()
    {
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(56);

        var userTrainingPlan = new UserTrainingPlan
        {
            StartDate = startDate,
            EndDate = endDate
        };

        userTrainingPlan.StartDate.ShouldBe(startDate);
        userTrainingPlan.EndDate.ShouldBe(endDate);
        var duration = userTrainingPlan.EndDate.Value - userTrainingPlan.StartDate;
        duration.Days.ShouldBe(56);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void UserTrainingPlan_ShouldAcceptValidActiveStates(bool isActive)
    {
        var userTrainingPlan = new UserTrainingPlan { IsActive = isActive };

        userTrainingPlan.IsActive.ShouldBe(isActive);
    }
}
