using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class User
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public int RoleId { get; set; }

    public int UserPersonalDataId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();

    public virtual Role Role { get; set; } = null!;

    public virtual UserPersonalDatum UserPersonalData { get; set; } = null!;

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
