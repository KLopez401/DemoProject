﻿using System;
using System.Collections.Generic;

namespace DemoProject.Domain.Model;
/// <summary>
/// category model/entity
/// </summary>
public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}