using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Domain.Tests.Entities.TrainingPlans;

public class TrainingPlanTests
{
    [Fact]
    public void TrainingPlan_ShouldInitializeWithDefaultValues()
    {
        // Act
        var trainingPlan = new TrainingPlan();

        // Assert
        trainingPlan.Id.ShouldBe(Guid.Empty);
        trainingPlan.Name.ShouldBe(string.Empty);
        trainingPlan.Description.ShouldBe(string.Empty);
        trainingPlan.DurationWeeks.ShouldBe(0);
        trainingPlan.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        trainingPlan.UpdatedAt.ShouldBeNull();
        trainingPlan.WeeklyPlans.ShouldNotBeNull();
        trainingPlan.WeeklyPlans.ShouldBeEmpty();
        trainingPlan.UserTrainingPlans.ShouldNotBeNull();
        trainingPlan.UserTrainingPlans.ShouldBeEmpty();
    }

    [Fact]
    public void TrainingPlan_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var name = "Beginner Push/Pull/Legs";
        var description = "A 3-day split for beginners";
        var durationWeeks = 8;
        var updatedAt = DateTime.UtcNow;

        // Act
        var trainingPlan = new TrainingPlan
        {
            Name = name,
            Description = description,
            DurationWeeks = durationWeeks,
            UpdatedAt = updatedAt
        };

        // Assert
        trainingPlan.Name.ShouldBe(name);
        trainingPlan.Description.ShouldBe(description);
        trainingPlan.DurationWeeks.ShouldBe(durationWeeks);
        trainingPlan.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void TrainingPlan_ShouldAllowCollectionManipulation()
    {
        // Arrange
        var trainingPlan = new TrainingPlan();
        var weeklyPlan1 = new WeeklyPlan { WeekNumber = 1 };
        var weeklyPlan2 = new WeeklyPlan { WeekNumber = 2 };
        var userTrainingPlan = new UserTrainingPlan();

        // Act
        trainingPlan.WeeklyPlans.Add(weeklyPlan1);
        trainingPlan.WeeklyPlans.Add(weeklyPlan2);
        trainingPlan.UserTrainingPlans.Add(userTrainingPlan);

        // Assert
        trainingPlan.WeeklyPlans.Count.ShouldBe(2);
        trainingPlan.WeeklyPlans.ShouldContain(weeklyPlan1);
        trainingPlan.WeeklyPlans.ShouldContain(weeklyPlan2);
        trainingPlan.UserTrainingPlans.ShouldContain(userTrainingPlan);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(12)]
    [InlineData(16)]
    public void TrainingPlan_ShouldAcceptValidDurationWeeks(int durationWeeks)
    {
        // Act
        var trainingPlan = new TrainingPlan { DurationWeeks = durationWeeks };

        // Assert
        trainingPlan.DurationWeeks.ShouldBe(durationWeeks);
    }
}
