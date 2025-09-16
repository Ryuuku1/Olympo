using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Infrastructure.Data.Configurations;

internal class TrainingPlanConfiguration : EntityConfigurationBase<TrainingPlan>
{
    public override void Configure(EntityTypeBuilder<TrainingPlan> builder)
    {
        builder.Property(tp => tp.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(tp => tp.Description)
            .HasMaxLength(1000);
        
        builder.Property(tp => tp.DurationWeeks)
            .IsRequired();
        
        builder.HasMany(tp => tp.WeeklyPlans)
            .WithOne(pw => pw.TrainingPlan)
            .HasForeignKey(pw => pw.TrainingPlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(tp => tp.UserTrainingPlans)
            .WithOne(utp => utp.TrainingPlan)
            .HasForeignKey(utp => utp.TrainingPlanId);
        
        base.Configure(builder);
    }
}
