using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Olympo.Infrastructure.Persistence.Equipments;

internal class EquipmentConfiguration : EntityConfigurationBase<Domain.Entities.Equipments.Equipment>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.Equipments.Equipment> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Description)
            .HasMaxLength(500);
        
        base.Configure(builder);
    }
}
