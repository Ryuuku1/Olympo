using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Users;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Tests.Entities.Workouts;

public class WorkoutSessionTests
{
    [Fact]
    public void WorkoutSession_ShouldInitializeWithDefaultValues()
    {
        // Act
        var workoutSession = new WorkoutSession();

        // Assert
        workoutSession.Id.ShouldBe(Guid.Empty);
        workoutSession.UserId.ShouldBe(Guid.Empty);
        workoutSession.WorkoutId.ShouldBe(Guid.Empty);
        workoutSession.StartDateTime.ShouldBe(DateTime.MinValue);
        workoutSession.EndDateTime.ShouldBeNull();
        workoutSession.Duration.ShouldBeNull();
        workoutSession.Notes.ShouldBe(string.Empty);
        workoutSession.Status.ShouldBe(WorkoutSessionStatus.NotStarted);
        workoutSession.ActualSets.ShouldBeNull();
        workoutSession.ActualReps.ShouldBeNull();
        workoutSession.ActualWeight.ShouldBeNull();
        workoutSession.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        workoutSession.UpdatedAt.ShouldBeNull();
        workoutSession.User.ShouldBeNull();
        workoutSession.Workout.ShouldBeNull();
        workoutSession.PersonalRecords.ShouldNotBeNull();
        workoutSession.PersonalRecords.ShouldBeEmpty();
    }

    [Fact]
    public void WorkoutSession_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var workoutId = Guid.NewGuid();
        var startDateTime = DateTime.UtcNow.AddHours(-1);
        var endDateTime = DateTime.UtcNow;
        var duration = TimeSpan.FromHours(1);
        var notes = "Great workout!";
        var status = WorkoutSessionStatus.Completed;
        var actualSets = 3;
        var actualReps = 10;
        var actualWeight = 75.5m;
        var updatedAt = DateTime.UtcNow;

        // Act
        var workoutSession = new WorkoutSession
        {
            UserId = userId,
            WorkoutId = workoutId,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Duration = duration,
            Notes = notes,
            Status = status,
            ActualSets = actualSets,
            ActualReps = actualReps,
            ActualWeight = actualWeight,
            UpdatedAt = updatedAt
        };

        // Assert
        workoutSession.UserId.ShouldBe(userId);
        workoutSession.WorkoutId.ShouldBe(workoutId);
        workoutSession.StartDateTime.ShouldBe(startDateTime);
        workoutSession.EndDateTime.ShouldBe(endDateTime);
        workoutSession.Duration.ShouldBe(duration);
        workoutSession.Notes.ShouldBe(notes);
        workoutSession.Status.ShouldBe(status);
        workoutSession.ActualSets.ShouldBe(actualSets);
        workoutSession.ActualReps.ShouldBe(actualReps);
        workoutSession.ActualWeight.ShouldBe(actualWeight);
        workoutSession.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void WorkoutSession_ShouldAllowNavigationPropertiesAssignment()
    {
        // Arrange
        var workoutSession = new WorkoutSession();
        var user = new User { Email = "test@example.com" };
        var workout = new Workout();

        // Act
        workoutSession.User = user;
        workoutSession.Workout = workout;

        // Assert
        workoutSession.User.ShouldBe(user);
        workoutSession.Workout.ShouldBe(workout);
    }

    [Fact]
    public void WorkoutSession_ShouldAllowPersonalRecordsCollectionManipulation()
    {
        // Arrange
        var workoutSession = new WorkoutSession();
        var personalRecord1 = new PersonalRecord();
        var personalRecord2 = new PersonalRecord();

        // Act
        workoutSession.PersonalRecords.Add(personalRecord1);
        workoutSession.PersonalRecords.Add(personalRecord2);

        // Assert
        workoutSession.PersonalRecords.Count.ShouldBe(2);
        workoutSession.PersonalRecords.ShouldContain(personalRecord1);
        workoutSession.PersonalRecords.ShouldContain(personalRecord2);
    }

    [Theory]
    [InlineData(WorkoutSessionStatus.NotStarted)]
    [InlineData(WorkoutSessionStatus.InProgress)]
    [InlineData(WorkoutSessionStatus.Paused)]
    [InlineData(WorkoutSessionStatus.Completed)]
    [InlineData(WorkoutSessionStatus.Cancelled)]
    public void WorkoutSession_ShouldAcceptValidStatuses(WorkoutSessionStatus status)
    {
        // Act
        var workoutSession = new WorkoutSession { Status = status };

        // Assert
        workoutSession.Status.ShouldBe(status);
    }

    [Fact]
    public void WorkoutSession_ShouldCalculateDurationCorrectly()
    {
        // Arrange
        var startDateTime = DateTime.UtcNow.AddHours(-1);
        var endDateTime = DateTime.UtcNow;
        var expectedDuration = endDateTime - startDateTime;

        // Act
        var workoutSession = new WorkoutSession
        {
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Duration = expectedDuration
        };

        // Assert
        workoutSession.Duration.ShouldBe(expectedDuration);
        workoutSession.Duration.Value.Hours.ShouldBe(1);
    }
}
