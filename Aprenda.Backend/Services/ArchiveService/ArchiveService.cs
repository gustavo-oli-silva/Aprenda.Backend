using System;
using Aprenda.Backend.Dtos.Archive;
using Aprenda.Backend.Repositories.Archive;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.ArchiveService;

public class ArchiveService : IArchiveService
{
    private readonly IArchiveRepository _archiveRepository;
    private readonly string _uploadsFolderPath;

    public ArchiveService(IArchiveRepository archiveRepository, IConfiguration configuration, IWebHostEnvironment env)
    {
        _archiveRepository = archiveRepository;
        string relativePath = configuration.GetValue<string>("StorageSettings:UploadsFolderPath");

        if (string.IsNullOrEmpty(relativePath))
        {
            throw new InvalidOperationException("Caminho para upload (UploadsFolderPath) não configurado.");
        }

        _uploadsFolderPath = Path.Combine(env.ContentRootPath, relativePath);
        if (!Directory.Exists(_uploadsFolderPath))
        {
            Directory.CreateDirectory(_uploadsFolderPath);
        }
    }


    public async Task<(byte[] content, string contentType, string originalName)> GetFileForDownloadAsync(string storedName)
    {

        var archive = await _archiveRepository.GetByStoredNameAsync(storedName);

        if (archive == null)
        {
            throw new FileNotFoundException("Arquivo não encontrado no sistema.");
        }
        var filePath = Path.Combine(_uploadsFolderPath, archive.StoredName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Registro do arquivo encontrado, mas o arquivo físico está ausente.");
        }

        var content = await File.ReadAllBytesAsync(filePath);
        return (content, archive.ContentType, archive.OriginalName);
    }

    public async Task<ArchiveDto> UploadFileAsync(IFormFile file)
    {
        ValidateUpload(file);

        var storedName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        var filePath = Path.Combine(_uploadsFolderPath, storedName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        catch (Exception ex)
        {
            throw new IOException($"Falha ao salvar o arquivo fisicamente: {ex.Message}", ex);
        }


        var archiveEntity = new Archive
        {
            OriginalName = file.FileName,
            StoredName = storedName,
            ContentType = file.ContentType,
            SizeInBytes = file.Length,
            UploadedAt = DateTime.UtcNow
        };

        var savedArchive = await _archiveRepository.AddAsync(archiveEntity);
        var downloadUrl = $"/api/Archive/download/{savedArchive.StoredName}";

        return new ArchiveDto(
            savedArchive.Id,
            savedArchive.OriginalName,
            savedArchive.ContentType,
            savedArchive.SizeInBytes,
            savedArchive.UploadedAt,
            downloadUrl
        );
    }

    private static void ValidateUpload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("O arquivo não pode ser nulo ou vazio.", nameof(file));
        }

        const long maxFileSize = 10 * 1024 * 1024;
        if (file.Length > maxFileSize)
        {
            throw new ArgumentException($"O tamanho do arquivo excede o limite de {maxFileSize / 1024 / 1024} MB.");
        }
    }
}