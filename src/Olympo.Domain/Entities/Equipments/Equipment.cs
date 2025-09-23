using Olympo.Domain.Abstractions;
using Olympo.Domain.Entities.Exercises;

namespace Olympo.Domain.Entities.Equipments;

public class Equipment : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Exercise> Exercises { get; set; } = [];
}
