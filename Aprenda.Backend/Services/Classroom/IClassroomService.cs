using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Classroom;

public interface IClassroomService
{

    Task<IEnumerable<Dtos.Classroom.ClassroomDto>> GetAllClassroomsAsync();
    Task<Dtos.Classroom.ClassroomDto> GetClassroomByIdAsync(long id);
    Task<Dtos.Classroom.ClassroomDto> CreateClassroomAsync(long userId, Dtos.Classroom.CreateClassroomDto classroom);
    Task UpdateClassroomAsync(long id, Dtos.Classroom.CreateClassroomDto classroom);
    Task DeleteClassroomAsync(long id);

    Task AssignUserToClassroom(long classroomId, long userId);

    Task JoinClassroom(string inviteCode, long userId);

    Task<IEnumerable<Dtos.User.UserDto>> GetClassroomWithStudentsAsync(long id);

    Task<IEnumerable<Dtos.User.UserDto>> GetClassroomWithProfessorsAsync(long id);

    Task<string> GenerateInviteCode(int length = 6);
}
