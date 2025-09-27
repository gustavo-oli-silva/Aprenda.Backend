using Aprenda.Backend.Dtos.User;

namespace Aprenda.Backend.Dtos.Homework;

public record HomeworkDto(
    long Id,
    string Title,
    string Content,
    bool IsFixed,
    UserDto User,
    long ClassroomId,
    DateTime DueDate
);

