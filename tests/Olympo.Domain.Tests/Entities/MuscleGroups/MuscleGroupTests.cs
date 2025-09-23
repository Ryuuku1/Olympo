using Olympo.Domain.Entities.Exercises;
using Olympo.Domain.Entities.MuscleGroups;

namespace Olympo.Domain.Tests.Entities.MuscleGroups;

public class MuscleGroupTests
{
    [Fact]
    public void MuscleGroup_ShouldInitializeWithDefaultValues()
    {
        // Act
        var muscleGroup = new MuscleGroup();

        // Assert
        muscleGroup.Id.ShouldBe(Guid.Empty);
        muscleGroup.Name.ShouldBe(string.Empty);
        muscleGroup.Description.ShouldBe(string.Empty);
        muscleGroup.Exercises.ShouldNotBeNull();
        muscleGroup.Exercises.ShouldBeEmpty();
    }

    [Fact]
    public void MuscleGroup_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var name = "Chest";
        var description = "Pectoral muscles";

        // Act
        var muscleGroup = new MuscleGroup
        {
            Name = name,
            Description = description
        };

        // Assert
        muscleGroup.Name.ShouldBe(name);
        muscleGroup.Description.ShouldBe(description);
    }

    [Fact]
    public void MuscleGroup_ShouldAllowExerciseCollectionManipulation()
    {
        // Arrange
        var muscleGroup = new MuscleGroup();
        var exercise1 = new Exercise { Name = "Push-up" };
        var exercise2 = new Exercise { Name = "Bench Press" };

        // Act
        muscleGroup.Exercises.Add(exercise1);
        muscleGroup.Exercises.Add(exercise2);

        // Assert
        muscleGroup.Exercises.Count.ShouldBe(2);
        muscleGroup.Exercises.ShouldContain(exercise1);
        muscleGroup.Exercises.ShouldContain(exercise2);
    }
}
