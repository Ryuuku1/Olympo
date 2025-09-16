namespace Olympo.Domain.Entities.Records;

public interface IPersonalRecordRepository
{
    Task<PersonalRecord?> GetAsync(Guid userId, Guid exerciseId);
    Task AddAsync(PersonalRecord personalRecord);
    Task UpdateAsync(PersonalRecord personalRecord);
    Task DeleteAsync(Guid userId, Guid exerciseId);
}