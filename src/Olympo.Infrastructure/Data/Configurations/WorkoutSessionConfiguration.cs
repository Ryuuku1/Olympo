using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Infrastructure.Data.Configurations;

internal class WorkoutSessionConfiguration : EntityConfigurationBase<WorkoutSession>
{
    public override void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        
        builder.Property(ws => ws.StartDateTime)
            .IsRequired();
        
        builder.Property(ws => ws.Notes)
            .HasMaxLength(1000);
        
        builder.Property(ws => ws.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.HasOne(ws => ws.User)
            .WithMany(u => u.WorkoutSessions)
            .HasForeignKey(ws => ws.UserId);
        
        builder.HasOne(ws => ws.Workout)
            .WithMany(w => w.WorkoutSessions)
            .HasForeignKey(ws => ws.WorkoutId);
        
        base.Configure(builder);   
    }
}