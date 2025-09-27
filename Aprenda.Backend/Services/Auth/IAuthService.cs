using System;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Models;

namespace Aprenda.Backend.Services.Jwt;

public interface IAuthService
{
    Task<string> Authenticate(LoginDto loginDto);
}
