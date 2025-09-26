using Aprenda.Backend.Dtos.Archive;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Aprenda.Backend.Services;

public interface IArchiveService
{
    
    Task<ArchiveDto> UploadFileAsync(IFormFile file);

  
    Task<(byte[] content, string contentType, string originalName)> GetFileForDownloadAsync(string storedName);
}