namespace UrbanCare.Domain.Entities;

public partial class WallMaterial
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
}
