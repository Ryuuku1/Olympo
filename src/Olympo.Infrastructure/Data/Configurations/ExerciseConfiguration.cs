using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.Exercises;

namespace Olympo.Infrastructure.Data.Configurations;

internal class ExerciseConfiguration : EntityConfigurationBase<Exercise>
{
    public override void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.Description)
            .HasMaxLength(1000);
        
        builder.Property(e => e.MuscleGroups)
            .HasMaxLength(500);
        
        builder.Property(e => e.Equipment)
            .HasMaxLength(200);
        
        builder.Property(e => e.Instructions)
            .HasMaxLength(2000);
        
        builder.HasIndex(e => e.Name)
            .IsUnique();
        
        builder.HasMany(e => e.Workouts)
            .WithOne(w => w.Exercise)
            .HasForeignKey(w => w.ExerciseId);
        
        builder.HasMany(e => e.PersonalRecords)
            .WithOne(pr => pr.Exercise)
            .HasForeignKey(pr => pr.ExerciseId);
        
        base.Configure(builder);
    }
}
