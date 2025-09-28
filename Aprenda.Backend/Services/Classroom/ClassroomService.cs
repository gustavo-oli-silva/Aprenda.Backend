using System;
using System.Security.Cryptography;
using System.Text;
using Aprenda.Backend.Dtos.Classroom;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.Classroom;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Classroom;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.User;
namespace Aprenda.Backend.Services.Classroom;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IUserRepository _userRepository;

    private const string CharacterSet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public ClassroomService(IClassroomRepository classroomRepository, IUserRepository userRepository)
    {
        _classroomRepository = classroomRepository;
        _userRepository = userRepository;
    }

    public async Task AssignUserToClassroom(long classroomId, long userId)
    {
        var classroom = await _classroomRepository.GetByIdAsync(classroomId) ?? throw new KeyNotFoundException("Classroom not found");
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new KeyNotFoundException("User not found");

        if (classroom.Users.Any(u => u.Id == userId))
        {
            throw new InvalidOperationException("User is already assigned to this classroom");
        }

        await _classroomRepository.AssignUserToClassroom(classroom, user);
    }

    public async Task<ClassroomDto> CreateClassroomAsync(long userId, CreateClassroomDto classroom)
    {
        var classroomEntity = ClassroomMapper.ToDomain(classroom);
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new KeyNotFoundException("User not found");
        if (user.Profile != EProfile.Professor)
        {
            throw new InvalidOperationException("Only professors can create classrooms");
        }

        string newCode = await ValidateInviteCode();

        classroomEntity.InviteCode = newCode;
        await _classroomRepository.AddAsync(classroomEntity);
        await AssignUserToClassroom(classroomEntity.Id, userId);
        return classroomEntity.ToDto();
    }

    private async Task<string> ValidateInviteCode()
    {
        string newCode;
        bool codeExists;

        do
        {
            newCode = await GenerateInviteCode(6);
            codeExists = await _classroomRepository.InviteCodeExistsAsync(newCode);
        } while (codeExists);
        return newCode;
    }

    public async Task DeleteClassroomAsync(long id)
    {
        await _classroomRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ClassroomDto>> GetAllClassroomsAsync()
    {
        var classrooms = await _classroomRepository.GetAllAsync();
        return classrooms.Select(c => c.ToDto());
    }

    public async Task<ClassroomDto> GetClassroomByIdAsync(long id)
    {
        var classroom = await _classroomRepository.GetByIdAsync(id);
        return classroom?.ToDto();
    }



    public async Task UpdateClassroomAsync(long id, CreateClassroomDto classroom)
    {
        var classroomEntity = await _classroomRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Classroom not found");

        classroomEntity.Name = classroom.Name;
        classroomEntity.Description = classroom.Description;

        await _classroomRepository.UpdateAsync(classroomEntity);
    }

    Task<IEnumerable<UserDto?>> IClassroomService.GetClassroomWithStudentsAsync(long id)
    {
        var classroom = _classroomRepository.GetClassroomWithUsersAsync(id) ?? throw new KeyNotFoundException("Classroom not found");
        if (classroom == null)
        {
            throw new KeyNotFoundException("Classroom not found");
        }

        var students = classroom.Result.Users.Where(u => u.Profile == EProfile.Student).Select(u => u.ToDto());
        return Task.FromResult<IEnumerable<UserDto?>>(students);
    }

    public Task<IEnumerable<UserDto?>> GetClassroomWithProfessorsAsync(long id)
    {
        var classroom = _classroomRepository.GetClassroomWithUsersAsync(id) ?? throw new KeyNotFoundException("Classroom not found");
        if (classroom == null)
        {
            throw new KeyNotFoundException("Classroom not found");
        }

        var professors = classroom.Result.Users.Where(u => u.Profile == EProfile.Professor).Select(u => UserMapper.ToDto(u));
        return Task.FromResult<IEnumerable<UserDto?>>(professors);
    }

    public async Task<string> GenerateInviteCode(int length = 6)
    {
        var result = new StringBuilder(length);
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (result.Length < length)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);

                result.Append(CharacterSet[(int)(num % (uint)CharacterSet.Length)]);
            }
        }

        return result.ToString();
    }

    public async Task JoinClassroom(string inviteCode, long userId)
    {
        var classroom = await _classroomRepository.GetClassroomByInviteCodeAsync(inviteCode) ?? throw new KeyNotFoundException("Classroom not found");
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new KeyNotFoundException("User not found");

        if (classroom.Users.Any(u => u.Id == userId))
        {
            throw new InvalidOperationException("User is already assigned to this classroom");
        }

         await _classroomRepository.AssignUserToClassroom(classroom, user);
    }
}
