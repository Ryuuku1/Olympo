using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.Records;

namespace Olympo.Infrastructure.Data.Configurations;

internal class PersonalRecordConfiguration : EntityConfigurationBase<PersonalRecord>
{
    public override void Configure(EntityTypeBuilder<PersonalRecord> builder)
    {
        builder.Property(pr => pr.Load)
            .HasPrecision(8, 2)
            .IsRequired();
        
        builder.Property(pr => pr.Repetitions)
            .IsRequired();
        
        builder.Property(pr => pr.AchievedAt)
            .IsRequired();
        
        builder.HasOne(pr => pr.User)
            .WithMany(u => u.PersonalRecords)
            .HasForeignKey(pr => pr.UserId);
        
        builder.HasOne(pr => pr.Exercise)
            .WithMany(e => e.PersonalRecords)
            .HasForeignKey(pr => pr.ExerciseId);
        
        builder.HasOne(pr => pr.WorkoutSession)
            .WithMany()
            .HasForeignKey(pr => pr.WorkoutSessionId);
        
        builder.HasIndex(pr => new { pr.UserId, pr.ExerciseId })
            .IsUnique();
        
        base.Configure(builder);  
    }
}