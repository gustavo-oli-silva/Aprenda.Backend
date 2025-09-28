using System;
using Aprenda.Backend.Dtos.Classroom;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Models;

namespace Aprenda.Backend.Mappers.User;

public static class UserMapper
{
    public static UserDto ToDto(this Models.User user) =>
        new UserDto(
            user.Id,
            user.Name,
            user.Email,
            (EProfile) Enum.Parse(typeof(EProfile), user.Profile.ToString()),
            user.AvatarId,
            user.CreatedAt
        );


#pragma warning disable CS8601 // Possible null reference assignment.
    public static Models.User ToDomain(this CreateUserDto createDto) =>
        new Models.User
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Password = createDto.Password,
            Profile = createDto.Profile,
            CreatedAt = DateTime.UtcNow,
            AvatarId = createDto.AvatarId
        };
#pragma warning restore CS8601 // Possible null reference assignment.


    public static Models.Classroom ToDomain(this CreateClassroomDto createDto) =>
        new Models.Classroom
        {
            Name = createDto.Name,
            Description = createDto.Description,
            CreatedAt = DateTime.UtcNow
        };

    }
