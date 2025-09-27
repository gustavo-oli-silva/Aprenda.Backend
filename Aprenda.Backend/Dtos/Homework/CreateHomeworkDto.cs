namespace Aprenda.Backend.Dtos.Homework;

public record CreateHomeworkDto(
    string Title,
    string Content,
    bool IsFixed,
    List<long> AttachmentIds,
    DateTime DueDate
);
