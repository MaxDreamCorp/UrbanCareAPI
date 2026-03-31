using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Priority
{
    public int Id { get; set; }

    public string Priority1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
