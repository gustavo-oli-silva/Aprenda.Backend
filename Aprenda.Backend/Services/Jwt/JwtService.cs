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
using Microsoft.IdentityModel.Tokens;
namespace Aprenda.Backend.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateToken(Models.User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Claims são as informações que você quer guardar no token
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // 'Subject' (o ID do usuário)
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("role", user.Profile.ToString()) // O perfil (Professor, Aluno)
    };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2), // Duração do token
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
