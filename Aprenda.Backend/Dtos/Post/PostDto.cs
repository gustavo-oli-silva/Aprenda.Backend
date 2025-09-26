using Aprenda.Backend.Dtos.User;

namespace Aprenda.Backend.Dtos.Post;

public record PostDto(
    long Id,
    string Title,
    string Content,
    bool IsFixed,
    UserDto User,
    long ClassroomId
);

