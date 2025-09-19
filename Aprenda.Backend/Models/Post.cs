using System;

namespace Aprenda.Backend.Models;

public class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool IsFixed { get; set; }

}
