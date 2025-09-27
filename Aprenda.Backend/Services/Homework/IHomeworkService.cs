using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Homework;

public interface IHomeworkService
{

    Task<IEnumerable<Dtos.Homework.HomeworkDto>> GetAllHomeworksByClassroomIdAsync(long classroomId);
    Task<Dtos.Homework.HomeworkDto> GetHomeworkByIdAsync(long id);
    Task<Dtos.Homework.HomeworkDto> CreateHomeworkAsync(long userId, long classroomId, Dtos.Homework.CreateHomeworkDto Homework);
    Task UpdateHomeworkAsync(long userId, long idHomework, Dtos.Homework.CreateHomeworkDto Homework);
    Task DeleteHomeworkAsync(long userId, long idHomework);

}
