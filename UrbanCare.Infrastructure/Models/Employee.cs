using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ManagementCompanyId { get; set; }

    public int EmployeePositionId { get; set; }

    public int QualificationCategoryId { get; set; }

    public DateOnly EmploymentDate { get; set; }

    public int ExperienceYears { get; set; }

    public int Salary { get; set; }

    public int StatusId { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Certification> Certifications { get; set; } = new List<Certification>();

    public virtual EmployeePosition EmployeePosition { get; set; } = null!;

    public virtual ManagementCompany ManagementCompany { get; set; } = null!;

    public virtual ICollection<OrderExecutor> OrderExecutors { get; set; } = new List<OrderExecutor>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual QualificationCategory QualificationCategory { get; set; } = null!;

    public virtual EmployeeStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
