using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.Archive;

public class ArchiveRepository : IArchiveRepository
{
    private readonly AppDbContext _dbContext;

    public ArchiveRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Archive>> GetAllAsync()
    {
        return await _dbContext.Archives.ToListAsync();
    }

    public async Task<Models.Archive> GetByIdAsync(long id)
    {
        return await _dbContext.Archives.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Models.Archive> AddAsync(Models.Archive Archive)
    {
        await _dbContext.Archives.AddAsync(Archive);
        await _dbContext.SaveChangesAsync();
        return Archive;
    }

    public async Task UpdateAsync(Models.Archive Archive)
    {
        _dbContext.Archives.Update(Archive);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var Archive = await GetByIdAsync(id);
        if (Archive != null)
        {
            _dbContext.Archives.Remove(Archive);
            await _dbContext.SaveChangesAsync();
        }
    }

    public Task<Models.Archive> GetByStoredNameAsync(string storedName)
    {
        return _dbContext.Archives.FirstOrDefaultAsync(a => a.StoredName == storedName);
    }

    public async Task<IEnumerable<Models.Archive>> GetByIdsAsync(IEnumerable<long> ids)
    {
        return await _dbContext.Archives
        .Where(a => ids.Contains(a.Id))
        .ToListAsync();
    }
}
