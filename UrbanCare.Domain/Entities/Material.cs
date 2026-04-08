namespace UrbanCare.Domain.Entities;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal Price { get; set; }

    public int AmountAtStorage { get; set; }

    public int StorageId { get; set; }

    public virtual ICollection<OrderMaterial> OrderMaterials { get; set; } = new List<OrderMaterial>();

    public virtual Storage Storage { get; set; } = null!;
}
