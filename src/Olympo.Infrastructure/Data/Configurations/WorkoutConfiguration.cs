using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Infrastructure.Data.Configurations;

internal class WorkoutConfiguration : EntityConfigurationBase<Workout>
{
    public override void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.Property(w => w.Sets)
            .IsRequired();
        
        builder.Property(w => w.Reps)
            .IsRequired();
        
        builder.Property(w => w.Load)
            .HasPrecision(8, 2);
        
        builder.Property(w => w.Notes)
            .HasMaxLength(500);
        
        builder.Property(w => w.Guide)
            .HasMaxLength(1000);

        builder.Property(w => w.Order);
        
        builder.HasOne(w => w.DailyPlan)
            .WithMany(pd => pd.Workouts)
            .HasForeignKey(w => w.DailyPlanId);
        
        builder.HasOne(w => w.Exercise)
            .WithMany(e => e.Workouts)
            .HasForeignKey(w => w.ExerciseId);
        
        builder.HasMany(w => w.WorkoutSessions)
            .WithOne(ws => ws.Workout)
            .HasForeignKey(ws => ws.WorkoutId);
        
        base.Configure(builder);
    }
}
