using System;
using Aprenda.Backend.Models;

namespace Aprenda.Backend.Services.Jwt;

public interface IHashService
{
   string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
