using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CommonAddress { get; set; } = null!;

    public int ManagementCompanyId { get; set; }

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    public virtual ManagementCompany ManagementCompany { get; set; } = null!;
}
