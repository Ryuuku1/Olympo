using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.TrainingPlans;

namespace Olympo.Infrastructure.Persistence.TrainingPlans;

internal class UserTrainingPlanConfiguration : EntityConfigurationBase<UserTrainingPlan>
{
    public override void Configure(EntityTypeBuilder<UserTrainingPlan> builder)
    {
        builder.Property(utp => utp.StartDate)
            .IsRequired();

        builder.Property(utp => utp.EndDate);
        
        builder.Property(utp => utp.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(utp => utp.AssignedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
        
        builder.HasOne(utp => utp.User)
            .WithMany(u => u.UserTrainingPlans)
            .HasForeignKey(utp => utp.UserId);
        
        builder.HasOne(utp => utp.TrainingPlan)
            .WithMany(tp => tp.UserTrainingPlans)
            .HasForeignKey(utp => utp.TrainingPlanId);
        
        builder.HasIndex(utp => new { utp.UserId, utp.IsActive });
        builder.HasIndex(utp => utp.StartDate);
        
        base.Configure(builder);   
    }
}