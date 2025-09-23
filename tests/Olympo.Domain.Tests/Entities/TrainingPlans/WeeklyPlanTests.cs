using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Domain.Tests.Entities.TrainingPlans;

public class WeeklyPlanTests
{
    [Fact]
    public void WeeklyPlan_ShouldInitializeWithDefaultValues()
    {
        // Act
        var weeklyPlan = new WeeklyPlan();

        // Assert
        weeklyPlan.Id.ShouldBe(Guid.Empty);
        weeklyPlan.TrainingPlanId.ShouldBe(Guid.Empty);
        weeklyPlan.WeekNumber.ShouldBe(0);
        weeklyPlan.Notes.ShouldBe(string.Empty);
        weeklyPlan.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        weeklyPlan.UpdatedAt.ShouldBeNull();
        weeklyPlan.TrainingPlan.ShouldBeNull();
        weeklyPlan.DailyPlans.ShouldNotBeNull();
        weeklyPlan.DailyPlans.ShouldBeEmpty();
    }

    [Fact]
    public void WeeklyPlan_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var trainingPlanId = Guid.NewGuid();
        var weekNumber = 2;
        var notes = "Focus on form this week";
        var updatedAt = DateTime.UtcNow;

        // Act
        var weeklyPlan = new WeeklyPlan
        {
            TrainingPlanId = trainingPlanId,
            WeekNumber = weekNumber,
            Notes = notes,
            UpdatedAt = updatedAt
        };

        // Assert
        weeklyPlan.TrainingPlanId.ShouldBe(trainingPlanId);
        weeklyPlan.WeekNumber.ShouldBe(weekNumber);
        weeklyPlan.Notes.ShouldBe(notes);
        weeklyPlan.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void WeeklyPlan_ShouldAllowNavigationPropertiesAssignment()
    {
        // Arrange
        var weeklyPlan = new WeeklyPlan();
        var trainingPlan = new TrainingPlan { Name = "Push/Pull/Legs" };

        // Act
        weeklyPlan.TrainingPlan = trainingPlan;

        // Assert
        weeklyPlan.TrainingPlan.ShouldBe(trainingPlan);
    }

    [Fact]
    public void WeeklyPlan_ShouldAllowDailyPlansCollectionManipulation()
    {
        // Arrange
        var weeklyPlan = new WeeklyPlan();
        var dailyPlan1 = new DailyPlan { DayNumber = 1, Name = "Push Day" };
        var dailyPlan2 = new DailyPlan { DayNumber = 2, Name = "Pull Day" };
        var dailyPlan3 = new DailyPlan { DayNumber = 3, Name = "Legs Day" };

        // Act
        weeklyPlan.DailyPlans.Add(dailyPlan1);
        weeklyPlan.DailyPlans.Add(dailyPlan2);
        weeklyPlan.DailyPlans.Add(dailyPlan3);

        // Assert
        weeklyPlan.DailyPlans.Count.ShouldBe(3);
        weeklyPlan.DailyPlans.ShouldContain(dailyPlan1);
        weeklyPlan.DailyPlans.ShouldContain(dailyPlan2);
        weeklyPlan.DailyPlans.ShouldContain(dailyPlan3);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(12)]
    public void WeeklyPlan_ShouldAcceptValidWeekNumbers(int weekNumber)
    {
        // Act
        var weeklyPlan = new WeeklyPlan { WeekNumber = weekNumber };

        // Assert
        weeklyPlan.WeekNumber.ShouldBe(weekNumber);
    }
}
