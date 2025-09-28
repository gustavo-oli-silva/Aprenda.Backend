using System;
using Aprenda.Backend.Models;
using Aprenda.Backend.Dtos.User;
namespace Aprenda.Backend.Services.User;

public interface IUserService
{

    Task<IEnumerable<Dtos.User.UserDto>> GetAllUsersAsync();
    Task<Dtos.User.UserDto> GetUserByIdAsync(long id);
    Task<Dtos.User.UserDto> GetUserByEmailAsync(string email);
    Task<Dtos.User.UserDto> CreateUserAsync(Dtos.User.CreateUserDto user);
    Task UpdateUserAsync(long id, Dtos.User.CreateUserDto user);
    Task DeleteUserAsync(long id);

    Task<IEnumerable<Dtos.Classroom.ClassroomDto>> GetClassroomsByUserIdAsync(long userId);
}
