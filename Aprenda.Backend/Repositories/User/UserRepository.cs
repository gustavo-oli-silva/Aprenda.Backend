using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<Models.User> GetByIdAsync(long id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task AddAsync(Models.User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Models.User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Models.User> GetByEmailAsync(string email)
    {
       return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
