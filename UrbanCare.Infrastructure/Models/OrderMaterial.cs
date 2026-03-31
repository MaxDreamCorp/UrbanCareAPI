using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class OrderMaterial
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int MaterialId { get; set; }

    public int Quantity { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
