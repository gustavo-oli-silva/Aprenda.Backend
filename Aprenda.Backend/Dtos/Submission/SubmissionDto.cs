using Aprenda.Backend.Dtos.Archive;
using Aprenda.Backend.Dtos.Grade;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Models;

namespace Aprenda.Backend.Dtos.Submission;

public record SubmissionDto(
    long Id,
    UserDto User,
    long HomeworkId,
    ESubmissionStatus Status,
    IEnumerable<ArchiveDto> Archives,
    DateTime SubmittedAt,
    GradeDto? Grade
);

