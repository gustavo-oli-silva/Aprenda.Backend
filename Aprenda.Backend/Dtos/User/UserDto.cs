using Aprenda.Backend.Models;

namespace Aprenda.Backend.Dtos.User;

public record UserDto(
    long Id,
    string Name,
    string Email,
    EProfile Profile,
    string? AvatarUrl,  
    DateTime CreatedAt
);

