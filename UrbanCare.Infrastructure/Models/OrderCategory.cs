using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class OrderCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public int TypeId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual OrderType Type { get; set; } = null!;
}
