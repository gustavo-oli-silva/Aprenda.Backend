using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.Grade;

public interface IGradeRepository
{
    Task<Models.Grade?> GetGradeBySubmissionId(int submissionId);
    Task AddAsync(Models.Grade Grade);
}
