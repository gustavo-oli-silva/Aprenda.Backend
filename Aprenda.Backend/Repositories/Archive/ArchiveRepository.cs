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

    public async Task AddAsync(Models.Archive Archive)
    {
        await _dbContext.Archives.AddAsync(Archive);
        await _dbContext.SaveChangesAsync();
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

}
