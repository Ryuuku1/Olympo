using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Users;

namespace Olympo.Infrastructure.Data.Configurations;

internal class UserConfiguration : EntityConfigurationBase<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(512);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Weight)
            .HasPrecision(5, 2);
        
        builder.Property(u => u.Height)
            .HasPrecision(5, 2);
        
        builder.Property(u => u.FitnessLevel)
            .HasConversion<string>();
        
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.HasMany(u => u.UserTrainingPlans)
            .WithOne(utp => utp.User)
            .HasForeignKey(utp => utp.UserId);
        
        builder.HasMany(u => u.WorkoutSessions)
            .WithOne(ws => ws.User)
            .HasForeignKey(ws => ws.UserId);
        
        builder.HasMany(u => u.PersonalRecords)
            .WithOne(pr => pr.User)
            .HasForeignKey(pr => pr.UserId);
        
        base.Configure(builder);
    }
}
