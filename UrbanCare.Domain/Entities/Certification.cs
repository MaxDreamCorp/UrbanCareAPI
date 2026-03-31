namespace UrbanCare.Domain.Entities;

public partial class Certification
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Number { get; set; } = null!;

    public DateOnly IssueDate { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public string IssuingOrganization { get; set; } = null!;

    public string ScanFilePath { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
