using System;
using Aprenda.Backend.Models;

namespace Aprenda.Backend.Services.Jwt;

public interface IJwtService
{
    string GenerateToken(Models.User user);

}
