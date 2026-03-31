namespace UrbanCare.Domain.Entities;

public partial class SessionStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
