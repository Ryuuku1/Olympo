using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Infrastructure.Persistence.Workouts;

internal class WorkoutSessionConfiguration : EntityConfigurationBase<WorkoutSession>
{
    public override void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.Property(ws => ws.StartDateTime)
            .IsRequired();

        builder.Property(ws => ws.EndDateTime);

        builder.Property(ws => ws.Duration);
        
        builder.Property(ws => ws.Notes)
            .HasMaxLength(1000);
        
        builder.Property(ws => ws.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(ws => ws.ActualSets);
        
        builder.Property(ws => ws.ActualReps);
        
        builder.Property(ws => ws.ActualWeight)
            .HasPrecision(8, 2);

        builder.Property(ws => ws.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ws => ws.UpdatedAt);
        
        builder.HasOne(ws => ws.User)
            .WithMany(u => u.WorkoutSessions)
            .HasForeignKey(ws => ws.UserId);
        
        builder.HasOne(ws => ws.Workout)
            .WithMany(w => w.WorkoutSessions)
            .HasForeignKey(ws => ws.WorkoutId);

        builder.HasMany(ws => ws.PersonalRecords)
            .WithOne(pr => pr.WorkoutSession)
            .HasForeignKey(pr => pr.WorkoutSessionId);

        builder.HasIndex(ws => new { ws.UserId, ws.StartDateTime });
        builder.HasIndex(ws => ws.Status);
        
        base.Configure(builder);   
    }
}