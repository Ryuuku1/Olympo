using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.Records;

namespace Olympo.Infrastructure.Persistence.Records;

internal class PersonalRecordConfiguration : EntityConfigurationBase<PersonalRecord>
{
    public override void Configure(EntityTypeBuilder<PersonalRecord> builder)
    {
        builder.Property(pr => pr.Weight)
            .HasPrecision(8, 2)
            .IsRequired();
        
        builder.Property(pr => pr.Repetitions)
            .IsRequired();
        
        builder.Property(pr => pr.AchievedAt)
            .IsRequired();

        builder.Property(pr => pr.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(pr => pr.UpdatedAt);
        
        builder.HasOne(pr => pr.User)
            .WithMany(u => u.PersonalRecords)
            .HasForeignKey(pr => pr.UserId);
        
        builder.HasOne(pr => pr.Exercise)
            .WithMany(e => e.PersonalRecords)
            .HasForeignKey(pr => pr.ExerciseId);
        
        builder.HasOne(pr => pr.WorkoutSession)
            .WithMany(ws => ws.PersonalRecords)
            .HasForeignKey(pr => pr.WorkoutSessionId);
        
        builder.HasIndex(pr => new { pr.UserId, pr.ExerciseId });
        
        base.Configure(builder);  
    }
}