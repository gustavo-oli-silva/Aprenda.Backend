using Aprenda.Backend.Models;

namespace Aprenda.Backend.Repositories.Submission;

public interface ISubmissionRepository
{
    Task<IEnumerable<Models.Submission>> GetAllAsync();
    Task<Models.Submission> GetByIdAsync(long id);

    Task<IEnumerable<Models.Submission>> GetAllSubmissionsByHomeworkIdAsync(long idHomework);

    Task AddAsync(Models.Submission Submission);
    Task UpdateAsync(Models.Submission Submission);
    Task DeleteAsync(long id);
}
