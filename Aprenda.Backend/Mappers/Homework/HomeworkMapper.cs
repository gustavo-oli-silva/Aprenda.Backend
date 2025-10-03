using System;
using Aprenda.Backend.Dtos.Homework;
using Aprenda.Backend.Mappers.Submission;
using Aprenda.Backend.Mappers.User;

namespace Aprenda.Backend.Mappers.Homework;

public static class HomeworkMapper
{
    public static HomeworkDto ToDto(this Models.Homework Homework, IHttpContextAccessor httpContextAccessor) =>
        new HomeworkDto(
            Homework.Id,
            Homework.Title,
            Homework.Content,
            Homework.IsFixed,
            UserMapper.ToDto(Homework.User),
            Homework.ClassroomId,
            Homework.DueDate,
            Homework.CreatedAt,
            Homework.Submissions.Select(s => s.ToDto(httpContextAccessor))
        );


    public static Models.Homework ToDomain(this CreateHomeworkDto createDto) =>
        new Models.Homework
        {
            Title = createDto.Title,
            Content = createDto.Content,
            IsFixed = createDto.IsFixed,
            DueDate = createDto.DueDate
        };
}
