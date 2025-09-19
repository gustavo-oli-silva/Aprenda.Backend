using System;

namespace Aprenda.Backend.Models;

public class Classroom
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

}
