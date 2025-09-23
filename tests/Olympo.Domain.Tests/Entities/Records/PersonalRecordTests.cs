using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Users;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Tests.Entities.Records;

public class PersonalRecordTests
{
    [Fact]
    public void PersonalRecord_ShouldInitializeWithDefaultValues()
    {
        // Act
        var personalRecord = new PersonalRecord();

        // Assert
        personalRecord.Id.ShouldBe(Guid.Empty);
        personalRecord.UserId.ShouldBe(Guid.Empty);
        personalRecord.ExerciseId.ShouldBe(Guid.Empty);
        personalRecord.Weight.ShouldBe(0);
        personalRecord.Repetitions.ShouldBe(0);
        personalRecord.AchievedAt.ShouldBe(DateTime.MinValue);
        personalRecord.WorkoutSessionId.ShouldBe(Guid.Empty);
        personalRecord.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        personalRecord.UpdatedAt.ShouldBeNull();
        personalRecord.User.ShouldBeNull();
        personalRecord.Exercise.ShouldBeNull();
        personalRecord.WorkoutSession.ShouldBeNull();
    }

    [Fact]
    public void PersonalRecord_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var exerciseId = Guid.NewGuid();
        var workoutSessionId = Guid.NewGuid();
        var weight = 100.5m;
        var repetitions = 12;
        var achievedAt = DateTime.UtcNow.AddDays(-1);
        var updatedAt = DateTime.UtcNow;

        // Act
        var personalRecord = new PersonalRecord
        {
            UserId = userId,
            ExerciseId = exerciseId,
            WorkoutSessionId = workoutSessionId,
            Weight = weight,
            Repetitions = repetitions,
            AchievedAt = achievedAt,
            UpdatedAt = updatedAt
        };

        // Assert
        personalRecord.UserId.ShouldBe(userId);
        personalRecord.ExerciseId.ShouldBe(exerciseId);
        personalRecord.WorkoutSessionId.ShouldBe(workoutSessionId);
        personalRecord.Weight.ShouldBe(weight);
        personalRecord.Repetitions.ShouldBe(repetitions);
        personalRecord.AchievedAt.ShouldBe(achievedAt);
        personalRecord.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void PersonalRecord_ShouldAllowNavigationPropertiesAssignment()
    {
        // Arrange
        var personalRecord = new PersonalRecord();
        var user = new User { Email = "test@example.com" };
        var exercise = new Exercise { Name = "Bench Press" };
        var workoutSession = new WorkoutSession();

        // Act
        personalRecord.User = user;
        personalRecord.Exercise = exercise;
        personalRecord.WorkoutSession = workoutSession;

        // Assert
        personalRecord.User.ShouldBe(user);
        personalRecord.Exercise.ShouldBe(exercise);
        personalRecord.WorkoutSession.ShouldBe(workoutSession);
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(50.25)]
    [InlineData(200.75)]
    [InlineData(999.99)]
    public void PersonalRecord_ShouldAcceptValidWeights(decimal weight)
    {
        // Act
        var personalRecord = new PersonalRecord { Weight = weight };

        // Assert
        personalRecord.Weight.ShouldBe(weight);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(15)]
    [InlineData(100)]
    public void PersonalRecord_ShouldAcceptValidRepetitions(int repetitions)
    {
        // Act
        var personalRecord = new PersonalRecord { Repetitions = repetitions };

        // Assert
        personalRecord.Repetitions.ShouldBe(repetitions);
    }
}
