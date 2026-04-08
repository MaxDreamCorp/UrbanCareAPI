using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Resident
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ApartmentId { get; set; }

    public DateOnly MovingIntoDate { get; set; }

    public DateOnly? MovingOutDate { get; set; }

    public sbyte IsLiving { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
