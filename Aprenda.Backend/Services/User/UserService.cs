using System;
using Aprenda.Backend.Dtos.Classroom;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.Jwt;
namespace Aprenda.Backend.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _UserRepository;

    private readonly IArchiveRepository _ArchiveRepository;

    private readonly IHashService _hashService;

    public UserService(IUserRepository UserRepository, IArchiveRepository ArchiveRepository, IHashService hashService)
    {
        _UserRepository = UserRepository;
        _ArchiveRepository = ArchiveRepository;
        _hashService = hashService;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto User)
    {
        var UserEntity = User.ToDomain();
        var avatar = User.AvatarId != null ? await _ArchiveRepository.GetByIdAsync(User.AvatarId.Value) : null;
        if (User.AvatarId != null && avatar == null)
        {
            throw new KeyNotFoundException("Avatar not found");
        }
        UserEntity.Avatar = avatar;
        UserEntity.Password =       _hashService.HashPassword(UserEntity.Password);
        await _UserRepository.AddAsync(UserEntity);
        return UserEntity.ToDto();
    }

    public async Task DeleteUserAsync(long id)
    {
        await _UserRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var Users = await _UserRepository.GetAllAsync();
        return Users.Select(c => c.ToDto());
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        return (await _UserRepository.GetByEmailAsync(email))?.ToDto();
    }

    public async Task<UserDto> GetUserByIdAsync(long id)
    {
        var User = await _UserRepository.GetByIdAsync(id);
        return User?.ToDto();  
    }

    public async Task UpdateUserAsync(long id, CreateUserDto User)
    {
        var UserEntity = await _UserRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found");

        UserEntity.Name = User.Name;
        UserEntity.Email = User.Email;
        UserEntity.Password = User.Password;
        UserEntity.Profile = User.Profile;
        

        await _UserRepository.UpdateAsync(UserEntity);
    }
}
