using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Infrastructure.Data.Configurations;

internal class WeeklyPlanConfiguration : EntityConfigurationBase<WeeklyPlan>
{
    public override void Configure(EntityTypeBuilder<WeeklyPlan> builder)
    {
        builder.HasKey(pw => pw.Id);
        
        builder.Property(pw => pw.WeekNumber)
            .IsRequired();
        
        builder.Property(pw => pw.Notes)
            .HasMaxLength(1000);
        
        builder.HasOne(pw => pw.TrainingPlan)
            .WithMany(tp => tp.WeeklyPlans)
            .HasForeignKey(pw => pw.TrainingPlanId);
        
        builder.HasMany(pw => pw.DailyPlans)
            .WithOne(pd => pd.WeeklyPlan)
            .HasForeignKey(pd => pd.WeeklyPlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(pw => new { pw.TrainingPlanId, pw.WeekNumber })
            .IsUnique();
        
        base.Configure(builder);
    }
}