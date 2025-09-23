using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympo.Domain.Entities.Exercises;

namespace Olympo.Infrastructure.Persistence.Exercises;

internal class ExerciseConfiguration : EntityConfigurationBase<Exercise>
{
    public override void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.Description)
            .HasMaxLength(1000);
        
        builder.Property(e => e.Instructions)
            .HasMaxLength(2000);

        builder.Property(e => e.Category)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.Difficulty)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.UpdatedAt);
        
        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.HasIndex(e => e.Category);
        builder.HasIndex(e => e.Difficulty);
        builder.HasIndex(e => e.IsActive);
        
        // Equipment relationship
        builder.HasOne(e => e.Equipment)
            .WithMany(eq => eq.Exercises)
            .HasForeignKey(e => e.EquipmentId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(e => e.MuscleGroups)
            .WithMany(mg => mg.Exercises)
            .UsingEntity(j => j.ToTable("ExerciseMuscleGroups"));
        
        builder.HasMany(e => e.Workouts)
            .WithOne(w => w.Exercise)
            .HasForeignKey(w => w.ExerciseId);
        
        builder.HasMany(e => e.PersonalRecords)
            .WithOne(pr => pr.Exercise)
            .HasForeignKey(pr => pr.ExerciseId);
        
        base.Configure(builder);
    }
}
