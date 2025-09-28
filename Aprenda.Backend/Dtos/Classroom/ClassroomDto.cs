using Aprenda.Backend.Dtos.User;

namespace Aprenda.Backend.Dtos.Classroom;

public record ClassroomDto(
    long Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    IEnumerable<UserDto> Users
);

