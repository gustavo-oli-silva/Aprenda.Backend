namespace Aprenda.Backend.Dtos.Post;

public record CreatePostDto(
    string Title,
    string Content,
    bool IsFixed,
    List<long> AttachmentIds 
);
