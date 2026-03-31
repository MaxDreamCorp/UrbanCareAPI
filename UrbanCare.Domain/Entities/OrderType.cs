namespace UrbanCare.Domain.Entities;

public partial class OrderType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<OrderCategory> OrderCategories { get; set; } = new List<OrderCategory>();
}
