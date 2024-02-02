using System;
using System.Collections.Generic;

namespace DemoProject.Domain.Model;
/// <summary>
/// Book model or entity
/// </summary>
public partial class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? PublishDateUtc { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
