namespace Aprenda.Backend.Dtos.Archive;

public record ArchiveDto(
    long Id,
    string OriginalName,
    string ContentType,
    long SizeInBytes,
    DateTime UploadedAt,
    string DownloadUrl 
);
        
