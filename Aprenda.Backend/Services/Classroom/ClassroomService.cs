using System;
using Aprenda.Backend.Dtos.Classroom;
using Aprenda.Backend.Mappers.Classroom;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Classroom;
namespace Aprenda.Backend.Services.Classroom;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;

    public ClassroomService(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<ClassroomDto> CreateClassroomAsync(CreateClassroomDto classroom)
    {
        var classroomEntity = classroom.ToDomain();
        await _classroomRepository.AddAsync(classroomEntity);
        return classroomEntity.ToDto();
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
}
