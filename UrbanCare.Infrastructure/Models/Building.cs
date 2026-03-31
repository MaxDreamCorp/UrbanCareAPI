using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Building
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int RegionId { get; set; }

    public int BuildingTypeId { get; set; }

    public short YearBuilt { get; set; }

    public int FloorCount { get; set; }

    public int WallMaterialId { get; set; }

    public int FloorMaterialId { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual BuildingType BuildingType { get; set; } = null!;

    public virtual FloorMaterial FloorMaterial { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Region Region { get; set; } = null!;

    public virtual WallMaterial WallMaterial { get; set; } = null!;
}
