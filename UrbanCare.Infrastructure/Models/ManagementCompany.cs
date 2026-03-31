using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class ManagementCompany
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
