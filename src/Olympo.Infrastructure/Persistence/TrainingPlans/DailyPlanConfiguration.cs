using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Infrastructure.Persistence.TrainingPlans;

internal class DailyPlanConfiguration : EntityConfigurationBase<DailyPlan>
{
    public override void Configure(EntityTypeBuilder<DailyPlan> builder)
    {
        builder.Property(pd => pd.DayNumber)
            .IsRequired();
        
        builder.Property(pd => pd.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(pd => pd.Notes)
            .HasMaxLength(1000);

        builder.Property(pd => pd.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(pd => pd.UpdatedAt);
        
        builder.HasOne(pd => pd.WeeklyPlan)
            .WithMany(pw => pw.DailyPlans)
            .HasForeignKey(pd => pd.WeeklyPlanId);
        
        builder.HasMany(pd => pd.Workouts)
            .WithOne(w => w.DailyPlan)
            .HasForeignKey(w => w.DailyPlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(pd => new { pd.WeeklyPlanId, pd.DayNumber })
            .IsUnique();
        
        base.Configure(builder);
    }
}