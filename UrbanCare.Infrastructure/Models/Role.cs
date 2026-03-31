using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Role1 { get; set; } = null!;

    public string AccessLevel { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
