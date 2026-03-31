using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Apartment
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int BuildingId { get; set; }

    public int? Entrance { get; set; }

    public int Floor { get; set; }

    public int RoomsCount { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();
}
