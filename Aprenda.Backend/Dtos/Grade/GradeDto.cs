using Aprenda.Backend.Dtos.Archive;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Models;

namespace Aprenda.Backend.Dtos.Grade;

public record GradeDto(
    long Id,
    double Value,
    string Feedback,
    DateTime GradedAt
);

