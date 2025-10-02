using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Submission;

public interface ISubmissionService
{

    Task<IEnumerable<Dtos.Submission.SubmissionDto>> GetAllSubmissionsByHomeworkIdAsync(long userId,long homeworkId);
    Task<Dtos.Submission.SubmissionDto> GetSubmissionByIdAsync(long id);
    Task<Dtos.Submission.SubmissionDto> CreateSubmissionAsync(long userId, long homeworkId, Dtos.Submission.CreateSubmissionDto Submission);
    Task UpdateSubmissionAsync(long userId, long idSubmission, Dtos.Submission.CreateSubmissionDto Submission);
    Task DeleteSubmissionAsync(long userId, long idSubmission);

}
