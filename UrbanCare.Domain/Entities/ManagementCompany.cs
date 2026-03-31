namespace UrbanCare.Domain.Entities;

public partial class ManagementCompany
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    private ManagementCompany() { }

    public static ManagementCompany Create(int id,
        string name,
        string address)
    {
        return new()
        {
            Id = id,
            Name = name,
            Address = address
        };
    }
}
