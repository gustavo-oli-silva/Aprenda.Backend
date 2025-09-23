using System;
using Aprenda.Backend.Dtos.Classroom;

namespace Aprenda.Backend.Mappers.Classroom;

public static class ClassroomMapper
{
    public static ClassroomDto ToDto(this Models.Classroom classroom) =>
        new ClassroomDto(
            classroom.Id,
            classroom.Name,
            classroom.Description,
            classroom.CreatedAt
        );


    public static Models.Classroom ToDomain(this CreateClassroomDto createDto) =>
        new Models.Classroom
        {
            Name = createDto.Name,
            Description = createDto.Description,
            CreatedAt = DateTime.UtcNow
        };
}
