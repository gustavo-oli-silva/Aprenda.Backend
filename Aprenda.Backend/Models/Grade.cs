using System;

namespace Aprenda.Backend.Models;

public class Grade
{
    public long Id { get; set; }
    public double Value { get; set; }
    public string Feedback { get; set; }
    public DateTime GradedAt { get; set; }
}
