using System;
using Aprenda.Backend.Dtos.Submission;
using Aprenda.Backend.Mappers.User;

namespace Aprenda.Backend.Mappers.Submission;

public static class SubmissionMapper
{
    public static SubmissionDto ToDto(this Models.Submission Submission) =>
        new SubmissionDto(
            Submission.Id,
            UserMapper.ToDto(Submission.User),
            Submission.HomeworkId,
            Submission.Status
        );


    public static Models.Submission ToDomain(this CreateSubmissionDto createDto) =>
        new Models.Submission
        {
            SubmittedAt = DateTime.UtcNow,
            Status = Models.ESubmissionStatus.Submitted,
        };
}
