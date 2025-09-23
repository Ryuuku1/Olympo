using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.MuscleGroups;
using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Tests.Entities.Exercises;

public class ExerciseTests
{
    [Fact]
    public void Exercise_ShouldInitializeWithDefaultValues()
    {
        // Act
        var exercise = new Exercise();

        // Assert
        exercise.Id.ShouldBe(Guid.Empty);
        exercise.Name.ShouldBe(string.Empty);
        exercise.Description.ShouldBe(string.Empty);
        exercise.EquipmentId.ShouldBeNull();
        exercise.Instructions.ShouldBe(string.Empty);
        exercise.Category.ShouldBe(ExerciseCategory.Undefined);
        exercise.Difficulty.ShouldBe(ExerciseDifficulty.Undefined);
        exercise.IsActive.ShouldBeTrue();
        exercise.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        exercise.UpdatedAt.ShouldBeNull();
        exercise.Equipment.ShouldBeNull();
        exercise.MuscleGroups.ShouldNotBeNull();
        exercise.MuscleGroups.ShouldBeEmpty();
        exercise.Workouts.ShouldNotBeNull();
        exercise.Workouts.ShouldBeEmpty();
        exercise.PersonalRecords.ShouldNotBeNull();
        exercise.PersonalRecords.ShouldBeEmpty();
    }

    [Fact]
    public void Exercise_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var name = "Push-up";
        var description = "A basic bodyweight exercise";
        var equipmentId = Guid.NewGuid();
        var instructions = "Start in plank position, lower body, push up";
        var category = ExerciseCategory.Strength;
        var difficulty = ExerciseDifficulty.Beginner;
        var updatedAt = DateTime.UtcNow;

        // Act
        var exercise = new Exercise
        {
            Name = name,
            Description = description,
            EquipmentId = equipmentId,
            Instructions = instructions,
            Category = category,
            Difficulty = difficulty,
            IsActive = false,
            UpdatedAt = updatedAt
        };

        // Assert
        exercise.Name.ShouldBe(name);
        exercise.Description.ShouldBe(description);
        exercise.EquipmentId.ShouldBe(equipmentId);
        exercise.Instructions.ShouldBe(instructions);
        exercise.Category.ShouldBe(category);
        exercise.Difficulty.ShouldBe(difficulty);
        exercise.IsActive.ShouldBeFalse();
        exercise.UpdatedAt.ShouldBe(updatedAt);
    }

    [Fact]
    public void Exercise_ShouldAllowEquipmentAssignment()
    {
        // Arrange
        var exercise = new Exercise();
        var equipment = new Domain.Entities.Equipments.Equipment { Name = "Barbell" };

        // Act
        exercise.Equipment = equipment;

        // Assert
        exercise.Equipment.ShouldBe(equipment);
    }

    [Theory]
    [InlineData(ExerciseCategory.Strength)]
    [InlineData(ExerciseCategory.Cardio)]
    [InlineData(ExerciseCategory.Flexibility)]
    [InlineData(ExerciseCategory.Balance)]
    [InlineData(ExerciseCategory.Functional)]
    [InlineData(ExerciseCategory.Sports)]
    public void Exercise_ShouldAcceptValidCategories(ExerciseCategory category)
    {
        // Act
        var exercise = new Exercise { Category = category };

        // Assert
        exercise.Category.ShouldBe(category);
    }

    [Theory]
    [InlineData(ExerciseDifficulty.Beginner)]
    [InlineData(ExerciseDifficulty.Intermediate)]
    [InlineData(ExerciseDifficulty.Advanced)]
    [InlineData(ExerciseDifficulty.Expert)]
    public void Exercise_ShouldAcceptValidDifficulties(ExerciseDifficulty difficulty)
    {
        // Act
        var exercise = new Exercise { Difficulty = difficulty };

        // Assert
        exercise.Difficulty.ShouldBe(difficulty);
    }

    [Fact]
    public void Exercise_ShouldAllowCollectionManipulation()
    {
        // Arrange
        var exercise = new Exercise();
        var muscleGroup = new MuscleGroup { Name = "Chest" };
        var workout = new Workout();
        var personalRecord = new PersonalRecord();

        // Act
        exercise.MuscleGroups.Add(muscleGroup);
        exercise.Workouts.Add(workout);
        exercise.PersonalRecords.Add(personalRecord);

        // Assert
        exercise.MuscleGroups.ShouldContain(muscleGroup);
        exercise.Workouts.ShouldContain(workout);
        exercise.PersonalRecords.ShouldContain(personalRecord);
    }
}
