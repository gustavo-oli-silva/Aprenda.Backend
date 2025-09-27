using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aprenda.Backend.Dtos.Classroom;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.Jwt;
using Microsoft.IdentityModel.Tokens;
namespace Aprenda.Backend.Services.Hash;
using BCryptNet = BCrypt.Net.BCrypt;
public class HashService : IHashService
{
    private readonly IConfiguration _config;

    public HashService(IConfiguration config)
    {
        _config = config;
    }

    public string HashPassword(string password)
    {
          return BCryptNet.HashPassword(password);  
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
          return BCryptNet.Verify(password, hashedPassword);
    }

}
