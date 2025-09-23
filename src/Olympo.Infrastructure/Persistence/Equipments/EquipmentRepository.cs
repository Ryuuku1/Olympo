using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities.Equipments;

namespace Olympo.Infrastructure.Persistence.Equipments;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly DatabaseContext _context;

    public EquipmentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Equipments.Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Equipment
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Domain.Entities.Equipments.Equipment?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Equipment
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(e => e.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.Equipments.Equipment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Equipment
            .Include(e => e.Exercises)
            .ToListAsync(cancellationToken);
    }

    public void Add(Domain.Entities.Equipments.Equipment equipment)
    {
        _context.Equipment.Add(equipment);
    }

    public void Update(Domain.Entities.Equipments.Equipment equipment)
    {
        _context.Equipment.Update(equipment);
    }

    public void Delete(Domain.Entities.Equipments.Equipment equipment)
    {
        _context.Equipment.Remove(equipment);
    }
}
