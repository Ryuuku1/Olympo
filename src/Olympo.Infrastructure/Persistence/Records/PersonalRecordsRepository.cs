using Microsoft.EntityFrameworkCore;
using Olympo.Domain.Entities.Records;

namespace Olympo.Infrastructure.Persistence.Records;

public class PersonalRecordsRepository : IPersonalRecordRepository
{
    private readonly DatabaseContext _context;
    
    public PersonalRecordsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PersonalRecord?> GetAsync(Guid userId, Guid exerciseId)
    {
        return await _context.PersonalRecords
            .FirstOrDefaultAsync(pr => pr.UserId == userId && pr.ExerciseId == exerciseId);
    }

    public async Task AddAsync(PersonalRecord personalRecord)
    {
        await _context.PersonalRecords.AddAsync(personalRecord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PersonalRecord personalRecord)
    {
        _context.PersonalRecords.Update(personalRecord);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId, Guid exerciseId)
    {
        var record = _context.PersonalRecords.FirstOrDefault(pr => pr.UserId == userId && pr.ExerciseId == exerciseId);
        if (record != null)
        {
            _context.PersonalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}