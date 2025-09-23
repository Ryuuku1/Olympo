using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.MuscleGroups;

namespace Olympo.Infrastructure.Persistence.MuscleGroups;

internal class MuscleGroupConfiguration : EntityConfigurationBase<MuscleGroup>
{
    public override void Configure(EntityTypeBuilder<MuscleGroup> builder)
    {
        builder.Property(mg => mg.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(mg => mg.Description)
            .HasMaxLength(500);
        
        base.Configure(builder);
    }
}
