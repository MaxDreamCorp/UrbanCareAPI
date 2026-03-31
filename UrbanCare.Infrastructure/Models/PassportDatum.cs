using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class PassportDatum
{
    public int Id { get; set; }

    public string Seria { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public virtual ICollection<UserPersonalDatum> UserPersonalData { get; set; } = new List<UserPersonalDatum>();
}
