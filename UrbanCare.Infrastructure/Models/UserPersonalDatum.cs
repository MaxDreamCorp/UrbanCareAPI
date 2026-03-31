using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class UserPersonalDatum
{
    public int Id { get; set; }

    public int PasportDataId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Snils { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public virtual PassportDatum PasportData { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
