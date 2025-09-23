using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.Classroom;

public class ClassroomRepository : IClassroomRepository
{
    private readonly AppDbContext _dbContext;

    public ClassroomRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Classroom>> GetAllAsync()
    {
        return await _dbContext.Classrooms.ToListAsync();
    }

    public async Task<Models.Classroom> GetByIdAsync(long id)
    {
        return await _dbContext.Classrooms.FindAsync(id);
    }

    public async Task AddAsync(Models.Classroom classroom)
    {
        await _dbContext.Classrooms.AddAsync(classroom);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Models.Classroom classroom)
    {
        _dbContext.Classrooms.Update(classroom);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var classroom = await GetByIdAsync(id);
        if (classroom != null)
        {
            _dbContext.Classrooms.Remove(classroom);
            await _dbContext.SaveChangesAsync();
        }
    }   
}
