using System;

namespace Aprenda.Backend.Models;

public class Archive
{
    public long Id { get; set; }
    public string OriginalName { get; set; }
    public string StoredName { get; set; }

    public string ContentType { get; set; }

     public long SizeInBytes { get; set; }
    public DateTime UploadedAt { get; set; }


    
}
