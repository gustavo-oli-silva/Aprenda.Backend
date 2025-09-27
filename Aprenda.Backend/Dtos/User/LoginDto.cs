using Aprenda.Backend.Models;

namespace Aprenda.Backend.Dtos.User;

public record LoginDto(
    string Email,
    string Password
);
