using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Tests.Entities.Workouts;

public class WorkoutTests
{
    [Fact]
    public void Workout_ShouldInitializeWithDefaultValues()
    {
        // Act
        var workout = new Workout();

        // Assert
        workout.Id.ShouldBe(Guid.Empty);
        workout.DailyPlanId.ShouldBe(Guid.Empty);
        workout.ExerciseId.ShouldBe(Guid.Empty);
        workout.Sets.ShouldBe(0);
        workout.Reps.ShouldBe(0);
        workout.Weight.ShouldBeNull();
        workout.Notes.ShouldBe(string.Empty);
        workout.Guide.ShouldBe(string.Empty);
        workout.Order.ShouldBe(0);
        workout.RestTime.ShouldBeNull();
        workout.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        workout.UpdatedAt.ShouldBeNull();
        workout.DailyPlan.ShouldBeNull();
        workout.Exercise.ShouldBeNull();
        workout.WorkoutSessions.ShouldNotBeNull();
        workout.WorkoutSessions.ShouldBeEmpty();
    }

    [Fact]
    public void Workout_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var dailyPlanId = Guid.NewGuid();
        var exerciseId = Guid.NewGuid();
        var sets = 3;
        var reps = 12;
        var weight = 50.5m;
        var notes = "Focus on form";
        var guide = "Slow and controlled movement";
        var order = 1;
        var restTime = TimeSpan.FromMinutes(2);
        var updatedAt = DateTime.UtcNow;

        // Act
        var workout = new Workout
        {
            DailyPlanId = dailyPlanId,
            ExerciseId = exerciseId,
            Sets = sets,
            Reps = reps,
            Weight = weight,
            Notes = notes,
            Guide = guide,
            Order = order,
            RestTime = restTime,
            UpdatedAt = updatedAt
        };

        // Assert
        workout.DailyPlanId.ShouldBe(dailyPlanId);
        workout.ExerciseId.ShouldBe(exerciseId);
        workout.Sets.ShouldBe(sets);
        workout.Reps.ShouldBe(reps);
        workout.Weight.ShouldBe(weight);
        workout.Notes.ShouldBe(notes);
        workout.Guide.ShouldBe(guide);
        workout.Order.ShouldBe(order);
        workout.RestTime.ShouldBe(restTime);
        workout.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void Workout_ShouldAllowNavigationPropertiesAssignment()
    {
        // Arrange
        var workout = new Workout();
        var dailyPlan = new DailyPlan { Name = "Push Day" };
        var exercise = new Exercise { Name = "Bench Press" };

        // Act
        workout.DailyPlan = dailyPlan;
        workout.Exercise = exercise;

        // Assert
        workout.DailyPlan.ShouldBe(dailyPlan);
        workout.Exercise.ShouldBe(exercise);
    }

    [Fact]
    public void Workout_ShouldAllowWorkoutSessionsCollectionManipulation()
    {
        // Arrange
        var workout = new Workout();
        var workoutSession1 = new WorkoutSession();
        var workoutSession2 = new WorkoutSession();

        // Act
        workout.WorkoutSessions.Add(workoutSession1);
        workout.WorkoutSessions.Add(workoutSession2);

        // Assert
        workout.WorkoutSessions.Count.ShouldBe(2);
        workout.WorkoutSessions.ShouldContain(workoutSession1);
        workout.WorkoutSessions.ShouldContain(workoutSession2);
    }

    [Theory]
    [InlineData(1, 8)]
    [InlineData(3, 12)]
    [InlineData(4, 15)]
    [InlineData(5, 20)]
    public void Workout_ShouldAcceptValidSetsAndReps(int sets, int reps)
    {
        // Act
        var workout = new Workout { Sets = sets, Reps = reps };

        // Assert
        workout.Sets.ShouldBe(sets);
        workout.Reps.ShouldBe(reps);
    }

    [Theory]
    [InlineData(30)] // 30 seconds
    [InlineData(60)] // 1 minute
    [InlineData(120)] // 2 minutes
    [InlineData(300)] // 5 minutes
    public void Workout_ShouldAcceptValidRestTimes(int seconds)
    {
        // Arrange
        var restTime = TimeSpan.FromSeconds(seconds);

        // Act
        var workout = new Workout { RestTime = restTime };

        // Assert
        workout.RestTime.ShouldBe(restTime);
    }
}
