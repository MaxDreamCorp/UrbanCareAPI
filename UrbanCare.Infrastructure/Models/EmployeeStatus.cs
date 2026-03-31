using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class EmployeeStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
