using Olympo.Domain.Entities.Records;
using Olympo.Domain.Entities.TrainingPlans;
using Olympo.Domain.Entities.Users;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Domain.Tests.Entities.Users;

public class UserTests
{
    [Fact]
    public void User_ShouldInitializeWithDefaultValues()
    {
        // Act
        var user = new User();

        // Assert
        user.Id.ShouldBe(Guid.Empty);
        user.Email.ShouldBe(string.Empty);
        user.PasswordHash.ShouldBe(string.Empty);
        user.FirstName.ShouldBe(string.Empty);
        user.LastName.ShouldBe(string.Empty);
        user.DateOfBirth.ShouldBeNull();
        user.Weight.ShouldBeNull();
        user.Height.ShouldBeNull();
        user.FitnessLevel.ShouldBe(FitnessLevel.Unknown);
        user.Role.ShouldBe(UserRole.User);
        user.IsActive.ShouldBeTrue();
        user.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        user.UpdatedAt.ShouldBeNull();
        user.LastLoginAt.ShouldBeNull();
        user.UserTrainingPlans.ShouldNotBeNull();
        user.UserTrainingPlans.ShouldBeEmpty();
        user.WorkoutSessions.ShouldNotBeNull();
        user.WorkoutSessions.ShouldBeEmpty();
        user.PersonalRecords.ShouldNotBeNull();
        user.PersonalRecords.ShouldBeEmpty();
    }

    [Fact]
    public void User_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var email = "john.doe@example.com";
        var passwordHash = "hashedPassword123";
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = DateTime.UtcNow.AddYears(-25);
        var weight = 75.5m;
        var height = 180.0m;
        var fitnessLevel = FitnessLevel.Intermediate;
        var role = UserRole.Admin;
        var updatedAt = DateTime.UtcNow;
        var lastLoginAt = DateTime.UtcNow.AddHours(-1);

        // Act
        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Weight = weight,
            Height = height,
            FitnessLevel = fitnessLevel,
            Role = role,
            IsActive = false,
            UpdatedAt = updatedAt,
            LastLoginAt = lastLoginAt
        };

        // Assert
        user.Email.ShouldBe(email);
        user.PasswordHash.ShouldBe(passwordHash);
        user.FirstName.ShouldBe(firstName);
        user.LastName.ShouldBe(lastName);
        user.DateOfBirth.ShouldBe(dateOfBirth);
        user.Weight.ShouldBe(weight);
        user.Height.ShouldBe(height);
        user.FitnessLevel.ShouldBe(fitnessLevel);
        user.Role.ShouldBe(role);
        user.IsActive.ShouldBeFalse();
        user.UpdatedAt.ShouldBe(updatedAt);
        user.LastLoginAt.ShouldBe(lastLoginAt);
    }

    [Theory]
    [InlineData(FitnessLevel.Unknown)]
    [InlineData(FitnessLevel.Beginner)]
    [InlineData(FitnessLevel.Intermediate)]
    [InlineData(FitnessLevel.Advanced)]
    public void User_ShouldAcceptValidFitnessLevels(FitnessLevel fitnessLevel)
    {
        // Act
        var user = new User { FitnessLevel = fitnessLevel };

        // Assert
        user.FitnessLevel.ShouldBe(fitnessLevel);
    }

    [Theory]
    [InlineData(UserRole.Admin)]
    [InlineData(UserRole.User)]
    public void User_ShouldAcceptValidUserRoles(UserRole role)
    {
        // Act
        var user = new User { Role = role };

        // Assert
        user.Role.ShouldBe(role);
    }

    [Fact]
    public void User_ShouldAllowCollectionManipulation()
    {
        // Arrange
        var user = new User();
        var userTrainingPlan = new UserTrainingPlan();
        var workoutSession = new WorkoutSession();
        var personalRecord = new PersonalRecord();

        // Act
        user.UserTrainingPlans.Add(userTrainingPlan);
        user.WorkoutSessions.Add(workoutSession);
        user.PersonalRecords.Add(personalRecord);

        // Assert
        user.UserTrainingPlans.ShouldContain(userTrainingPlan);
        user.WorkoutSessions.ShouldContain(workoutSession);
        user.PersonalRecords.ShouldContain(personalRecord);
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name+tag@domain.co.uk")]
    [InlineData("firstname.lastname@company.org")]
    public void User_ShouldAcceptValidEmailFormats(string email)
    {
        // Act
        var user = new User { Email = email };

        // Assert
        user.Email.ShouldBe(email);
    }
}
