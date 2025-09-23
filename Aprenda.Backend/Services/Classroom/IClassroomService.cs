using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Classroom;

public interface IClassroomService
{

    Task<IEnumerable<Dtos.Classroom.ClassroomDto>> GetAllClassroomsAsync();
    Task<Dtos.Classroom.ClassroomDto> GetClassroomByIdAsync(long id);
    Task<Dtos.Classroom.ClassroomDto> CreateClassroomAsync(Dtos.Classroom.CreateClassroomDto classroom);
    Task<Dtos.Classroom.ClassroomDto> UpdateClassroomAsync(long id, Dtos.Classroom.CreateClassroomDto classroom);
    Task<bool> DeleteClassroomAsync(long id);
}
