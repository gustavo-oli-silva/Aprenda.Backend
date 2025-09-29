using System;
using Aprenda.Backend.Dtos.Archive;


namespace Aprenda.Backend.Mappers.Archive;

public static class ArchiveMapper
{
    public static ArchiveDto ToDto(this Models.Archive Archive, IHttpContextAccessor httpContextAccessor) =>
        new ArchiveDto(
            Archive.Id,
            Archive.OriginalName,
            Archive.ContentType,
            Archive.SizeInBytes,
            Archive.UploadedAt,
            DownloadUrl : GenerateFullPath(Archive, httpContextAccessor)
        );

   
     private static string GenerateFullPath(Models.Archive archive, IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";
        var relativePath = $"/api/archives/download/{archive.StoredName}";
        return $"{baseUrl}{relativePath}";
    }
   
}
