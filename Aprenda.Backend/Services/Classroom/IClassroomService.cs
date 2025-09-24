using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Classroom;

public interface IClassroomService
{

    Task<IEnumerable<Dtos.Classroom.ClassroomDto>> GetAllClassroomsAsync();
    Task<Dtos.Classroom.ClassroomDto> GetClassroomByIdAsync(long id);
    Task<Dtos.Classroom.ClassroomDto> CreateClassroomAsync(Dtos.Classroom.CreateClassroomDto classroom);
    Task UpdateClassroomAsync(long id, Dtos.Classroom.CreateClassroomDto classroom);
    Task DeleteClassroomAsync(long id);
}
