
namespace UrbanCare.Domain.Entities;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CommonAddress { get; set; } = null!;

    public int ManagementCompanyId { get; set; }

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    public virtual ManagementCompany ManagementCompany { get; set; } = null!;

    private Region() { }

    public static Region Create(int id,
                  string name,
                  string commonAddress,
                  ManagementCompany managementCompany)
    {
        return new Region
        {
            Id = id,
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            CommonAddress = commonAddress ?? throw new ArgumentNullException(nameof(commonAddress)),
            ManagementCompany = managementCompany ?? throw new ArgumentNullException(nameof(managementCompany))
        };
    }
}
