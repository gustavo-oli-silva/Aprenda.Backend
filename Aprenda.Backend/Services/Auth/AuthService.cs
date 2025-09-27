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
namespace Aprenda.Backend.Services.Auth;
using BCryptNet = BCrypt.Net.BCrypt;
public class AuthService : IAuthService
{


    private readonly IUserRepository _userService;

    private readonly IHashService _hashService;

    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userService, IHashService hashService, IJwtService jwtService)
    {

        _userService = userService;
        _hashService = hashService;
        _jwtService = jwtService;
    }

    public async Task<string> Authenticate(LoginDto loginDto)
    {
        var user = await _userService.GetByEmailAsync(loginDto.Email);

        if(user == null || !_hashService.VerifyPassword(loginDto.Password, user.Password))
        {
            return null;
        }

        return _jwtService.GenerateToken(user);
    }


}
