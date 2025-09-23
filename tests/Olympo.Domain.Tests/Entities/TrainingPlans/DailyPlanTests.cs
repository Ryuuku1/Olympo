using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Tests.Entities.TrainingPlans;

public class DailyPlanTests
{
    [Fact]
    public void DailyPlan_ShouldInitializeWithDefaultValues()
    {
        // Act
        var dailyPlan = new DailyPlan();

        // Assert
        dailyPlan.Id.ShouldBe(Guid.Empty);
        dailyPlan.WeeklyPlanId.ShouldBe(Guid.Empty);
        dailyPlan.DayNumber.ShouldBe(0);
        dailyPlan.Name.ShouldBe(string.Empty);
        dailyPlan.Notes.ShouldBe(string.Empty);
        dailyPlan.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        dailyPlan.UpdatedAt.ShouldBeNull();
        dailyPlan.WeeklyPlan.ShouldBeNull();
        dailyPlan.Workouts.ShouldNotBeNull();
        dailyPlan.Workouts.ShouldBeEmpty();
    }

    [Fact]
    public void DailyPlan_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var weeklyPlanId = Guid.NewGuid();
        var dayNumber = 3;
        var name = "Push Day";
        var notes = "Focus on chest and shoulders";
        var updatedAt = DateTime.UtcNow;

        // Act
        var dailyPlan = new DailyPlan
        {
            WeeklyPlanId = weeklyPlanId,
            DayNumber = dayNumber,
            Name = name,
            Notes = notes,
            UpdatedAt = updatedAt
        };

        // Assert
        dailyPlan.WeeklyPlanId.ShouldBe(weeklyPlanId);
        dailyPlan.DayNumber.ShouldBe(dayNumber);
        dailyPlan.Name.ShouldBe(name);
        dailyPlan.Notes.ShouldBe(notes);
        dailyPlan.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void DailyPlan_ShouldAllowNavigationPropertiesAssignment()
    {
        // Arrange
        var dailyPlan = new DailyPlan();
        var weeklyPlan = new WeeklyPlan { WeekNumber = 1 };

        // Act
        dailyPlan.WeeklyPlan = weeklyPlan;

        // Assert
        dailyPlan.WeeklyPlan.ShouldBe(weeklyPlan);
    }

    [Fact]
    public void DailyPlan_ShouldAllowWorkoutsCollectionManipulation()
    {
        // Arrange
        var dailyPlan = new DailyPlan();
        var workout1 = new Workout { Sets = 3, Reps = 12 };
        var workout2 = new Workout { Sets = 4, Reps = 8 };

        // Act
        dailyPlan.Workouts.Add(workout1);
        dailyPlan.Workouts.Add(workout2);

        // Assert
        dailyPlan.Workouts.Count.ShouldBe(2);
        dailyPlan.Workouts.ShouldContain(workout1);
        dailyPlan.Workouts.ShouldContain(workout2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public void DailyPlan_ShouldAcceptValidDayNumbers(int dayNumber)
    {
        // Act
        var dailyPlan = new DailyPlan { DayNumber = dayNumber };

        // Assert
        dailyPlan.DayNumber.ShouldBe(dayNumber);
    }

    [Theory]
    [InlineData("Push Day")]
    [InlineData("Pull Day")]
    [InlineData("Legs Day")]
    [InlineData("Full Body")]
    public void DailyPlan_ShouldAcceptValidNames(string name)
    {
        // Act
        var dailyPlan = new DailyPlan { Name = name };

        // Assert
        dailyPlan.Name.ShouldBe(name);
    }
}
