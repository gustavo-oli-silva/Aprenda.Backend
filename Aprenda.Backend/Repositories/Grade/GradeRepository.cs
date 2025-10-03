using System;
using Aprenda.Backend.Data;
using Aprenda.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Aprenda.Backend.Repositories.Grade;

public class GradeRepository : IGradeRepository
{
    private readonly AppDbContext _dbContext;

    public GradeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Models.Grade Grade)
    {
        await _dbContext.Grades.AddAsync(Grade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Models.Grade?> GetGradeBySubmissionId(int submissionId)
    {
        return await _dbContext.Grades.FirstOrDefaultAsync(g => g.SubmissionId == submissionId);
    }
}