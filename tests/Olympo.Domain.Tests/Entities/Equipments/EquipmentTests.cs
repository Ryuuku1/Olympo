using Olympo.Domain.Entities.Exercises;

namespace Olympo.Domain.Tests.Entities.Equipments;

public class EquipmentTests
{
    [Fact]
    public void Equipment_ShouldInitializeWithDefaultValues()
    {
        // Act
        var equipment = new Domain.Entities.Equipments.Equipment();

        // Assert
        equipment.Id.ShouldBe(Guid.Empty);
        equipment.Name.ShouldBe(string.Empty);
        equipment.Description.ShouldBe(string.Empty);
        equipment.Exercises.ShouldNotBeNull();
        equipment.Exercises.ShouldBeEmpty();
    }

    [Fact]
    public void Equipment_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var name = "Barbell";
        var description = "Olympic barbell for weightlifting";

        // Act
        var equipment = new Domain.Entities.Equipments.Equipment
        {
            Name = name,
            Description = description
        };

        // Assert
        equipment.Name.ShouldBe(name);
        equipment.Description.ShouldBe(description);
    }

    [Fact]
    public void Equipment_ShouldAllowExerciseCollectionManipulation()
    {
        // Arrange
        var equipment = new Domain.Entities.Equipments.Equipment();
        var exercise1 = new Exercise { Name = "Bench Press" };
        var exercise2 = new Exercise { Name = "Squat" };

        // Act
        equipment.Exercises.Add(exercise1);
        equipment.Exercises.Add(exercise2);

        // Assert
        equipment.Exercises.Count.ShouldBe(2);
        equipment.Exercises.ShouldContain(exercise1);
        equipment.Exercises.ShouldContain(exercise2);
    }

    [Theory]
    [InlineData("Barbell")]
    [InlineData("Dumbbell")]
    [InlineData("Kettlebell")]
    [InlineData("Resistance Band")]
    public void Equipment_ShouldAcceptValidNames(string name)
    {
        // Act
        var equipment = new Domain.Entities.Equipments.Equipment { Name = name };

        // Assert
        equipment.Name.ShouldBe(name);
    }

    [Theory]
    [InlineData("Standard Olympic barbell")]
    [InlineData("Adjustable dumbbells")]
    [InlineData("Heavy resistance band")]
    [InlineData("")]
    public void Equipment_ShouldAcceptValidDescriptions(string description)
    {
        // Act
        var equipment = new Domain.Entities.Equipments.Equipment { Description = description };

        // Assert
        equipment.Description.ShouldBe(description);
    }
}
