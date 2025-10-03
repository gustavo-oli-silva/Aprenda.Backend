using System;
using Aprenda.Backend.Dtos.Grade;
using Aprenda.Backend.Dtos.Submission;
using Aprenda.Backend.Mappers.Archive;
using Aprenda.Backend.Mappers.User;

namespace Aprenda.Backend.Mappers.Grade;

public static class GradeMapper
{
    public static GradeDto ToDto(this Models.Grade Grade) =>
        new GradeDto(
            Grade.Id,
            Grade.Value,
            Grade.Feedback,
            Grade.GradedAt
        );


    public static Models.Grade ToDomain(this CreateGradeDto createDto) =>
        new Models.Grade
        {
            Value = createDto.Value,
            Feedback = createDto.Feedback,
            GradedAt = DateTime.UtcNow
        };
}
