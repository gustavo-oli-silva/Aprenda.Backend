using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.Homework;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly AppDbContext _dbContext;

    public HomeworkRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Homework>> GetAllAsync()
    {
        return await _dbContext.Homeworks.ToListAsync();
    }

    public async Task<Models.Homework> GetByIdAsync(long id)
    {
        return await _dbContext.Homeworks.FindAsync(id);
    }

    public async Task AddAsync(Models.Homework Homework)
    {
        await _dbContext.Homeworks.AddAsync(Homework);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Models.Homework Homework)
    {
        _dbContext.Homeworks.Update(Homework);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var Homework = await GetByIdAsync(id);
        if (Homework != null)
        {
            _dbContext.Homeworks.Remove(Homework);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Models.Homework>> GetHomeworksByClassroomIdAsync(long idClassroom)
    {
        return await _dbContext.Homeworks.Include(p => p.User).Where(p => p.ClassroomId == idClassroom).ToListAsync();
    }
}
