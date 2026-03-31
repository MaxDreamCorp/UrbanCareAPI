using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class EmployeePosition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
