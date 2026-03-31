using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderMaterial> OrderMaterials { get; set; } = new List<OrderMaterial>();
}
