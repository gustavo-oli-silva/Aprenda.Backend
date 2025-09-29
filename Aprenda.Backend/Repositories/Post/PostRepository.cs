using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.Post;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _dbContext;

    public PostRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Post>> GetAllAsync()
    {
        return await _dbContext.Posts.ToListAsync();
    }

    public async Task<Models.Post> GetByIdAsync(long id)
    {
        return await _dbContext.Posts.FindAsync(id);
    }

    public async Task AddAsync(Models.Post Post)
    {
        await _dbContext.Posts.AddAsync(Post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Models.Post Post)
    {
        _dbContext.Posts.Update(Post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var Post = await GetByIdAsync(id);
        if (Post != null)
        {
            _dbContext.Posts.Remove(Post);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Models.Post>> GetPostsByClassroomIdAsync(long idClassroom)
    {
        return await _dbContext.Posts.Include(p => p.User).Include(p => p.Archives).Where(p => p.ClassroomId == idClassroom).ToListAsync();
    }
}
