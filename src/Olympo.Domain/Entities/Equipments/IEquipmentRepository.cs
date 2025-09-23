namespace Olympo.Domain.Entities.Equipments;

public interface IEquipmentRepository
{
    Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Equipment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Equipment?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    void Add(Equipment equipment);
    void Update(Equipment equipment);
    void Delete(Equipment equipment);
}
