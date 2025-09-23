using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.Workouts;

namespace Olympo.Infrastructure.Persistence.Workouts;

internal class WorkoutConfiguration : EntityConfigurationBase<Workout>
{
    public override void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.Property(w => w.Sets)
            .IsRequired();
        
        builder.Property(w => w.Reps)
            .IsRequired();
        
        builder.Property(w => w.Weight)
            .HasPrecision(8, 2);
        
        builder.Property(w => w.Notes)
            .HasMaxLength(500);
        
        builder.Property(w => w.Guide)
            .HasMaxLength(1000);

        builder.Property(w => w.Order)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(w => w.RestTime);

        builder.Property(w => w.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(w => w.UpdatedAt);
        
        builder.HasOne(w => w.DailyPlan)
            .WithMany(pd => pd.Workouts)
            .HasForeignKey(w => w.DailyPlanId);
        
        builder.HasOne(w => w.Exercise)
            .WithMany(e => e.Workouts)
            .HasForeignKey(w => w.ExerciseId);
        
        builder.HasMany(w => w.WorkoutSessions)
            .WithOne(ws => ws.Workout)
            .HasForeignKey(ws => ws.WorkoutId);

        builder.HasIndex(w => new { w.DailyPlanId, w.Order });
        
        base.Configure(builder);
    }
}
