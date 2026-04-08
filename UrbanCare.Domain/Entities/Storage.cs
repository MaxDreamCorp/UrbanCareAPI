namespace UrbanCare.Domain.Entities;

public partial class Storage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ManagementCompanyId { get; set; }

    public virtual ManagementCompany ManagementCompany { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
