namespace UrbanCare.Domain.Entities;

public partial class PassportDatum
{
    public int Id { get; private set; }

    public string Seria { get; private set; } = null!;

    public string Number { get; private set; } = null!;

    public string Department { get; private set; } = null!;

    public string DepartmentCode { get; private set; } = null!;

    public virtual ICollection<UserPersonalDatum> UserPersonalData { get; private set; } = new List<UserPersonalDatum>();


    private PassportDatum() { }

    public static PassportDatum Create(int id, string seria, string number, string department, string departmentCode)
    {
        return new PassportDatum
        {
            Id = id,
            Seria = seria ?? throw new ArgumentNullException(nameof(seria)),
            Number = number ?? throw new ArgumentNullException(nameof(number)),
            Department = department ?? throw new ArgumentNullException(nameof(department)),
            DepartmentCode = departmentCode ?? throw new ArgumentNullException(nameof(departmentCode))
        };
    }
}
