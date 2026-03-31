using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class BuildingType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
}
