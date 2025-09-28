    using Aprenda.Backend.Models;

namespace Aprenda.Backend.Dtos.User;

public record CreateUserDto(
    string Name,
    string Email,
    string Password,
    EProfile Profile,
    long? AvatarId
);
