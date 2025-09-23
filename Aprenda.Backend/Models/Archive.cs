using System;

namespace Aprenda.Backend.Models;

public class Archive
{
    public long Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }


    
}
