using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class QualificationCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public float MinExperienceYears { get; set; }

    public float SalaryCoefficient { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
