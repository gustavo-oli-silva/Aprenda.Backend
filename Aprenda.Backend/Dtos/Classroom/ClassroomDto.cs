namespace Aprenda.Backend.Dtos.Classroom;

public record ClassroomDto(
    long Id,
    string Name,
    string Description,
    DateTime CreatedAt
);

