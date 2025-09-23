using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Infrastructure.Persistence.TrainingPlans;

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

        builder.Property(tp => tp.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(tp => tp.UpdatedAt);
        
        builder.HasMany(tp => tp.WeeklyPlans)
            .WithOne(pw => pw.TrainingPlan)
            .HasForeignKey(pw => pw.TrainingPlanId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(tp => tp.UserTrainingPlans)
            .WithOne(utp => utp.TrainingPlan)
            .HasForeignKey(utp => utp.TrainingPlanId);

        builder.HasIndex(tp => tp.Name);
        
        base.Configure(builder);
    }
}
