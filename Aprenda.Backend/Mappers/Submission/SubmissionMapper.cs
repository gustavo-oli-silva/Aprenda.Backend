using System;
using Aprenda.Backend.Dtos.Submission;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Mappers.Archive; // Add this using
using Microsoft.AspNetCore.Http;       // Add this using
using System.Linq;
using Aprenda.Backend.Mappers.Grade;

namespace Aprenda.Backend.Mappers.Submission;

public static class SubmissionMapper
{
    public static SubmissionDto ToDto(this Models.Submission submission, IHttpContextAccessor httpContextAccessor) =>
        new SubmissionDto(
            submission.Id,
            submission.User.ToDto(),
            submission.HomeworkId,
            submission.Status,
            submission.Archives.Select(a => a.ToDto(httpContextAccessor)) ,
            submission.SubmittedAt,
            submission.Grade?.ToDto()
        );


    public static Models.Submission ToDomain(this CreateSubmissionDto createDto) =>
        new Models.Submission
        {
            SubmittedAt = DateTime.UtcNow,
            Status = Models.ESubmissionStatus.Submitted,
        };
}
