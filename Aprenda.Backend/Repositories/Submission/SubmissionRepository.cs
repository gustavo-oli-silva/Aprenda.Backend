using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.Submission;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly AppDbContext _dbContext;

    public SubmissionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Submission>> GetAllAsync()
    {
        return await _dbContext.Submissions.ToListAsync();
    }

    public async Task<Models.Submission> GetByIdAsync(long id)
    {
        return await _dbContext.Submissions.FindAsync(id);
    }

    public async Task AddAsync(Models.Submission Submission)
    {
        await _dbContext.Submissions.AddAsync(Submission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Models.Submission Submission)
    {
        _dbContext.Submissions.Update(Submission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var Submission = await GetByIdAsync(id);
        if (Submission != null)
        {
            _dbContext.Submissions.Remove(Submission);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Models.Submission>> GetAllSubmissionsByHomeworkIdAsync(long userId, long idHomework)
    {
        return await _dbContext.Submissions.Include(s => s.Archives).Include(s => s.User).Where(s => s.UserId == userId).Where(s => s.HomeworkId == idHomework).ToListAsync();
    }
    
    public async Task<IEnumerable<Models.Submission>> GetAllSubmissionsByHomeworkIdAsync(long idHomework)
    {
        return await _dbContext.Submissions.Include(s => s.Archives).Include(s => s.User).Where(s => s.HomeworkId == idHomework).ToListAsync();
    }
}
