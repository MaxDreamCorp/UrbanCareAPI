namespace UrbanCare.Domain.Entities;

public partial class UserSession
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public byte[] SessionToken { get; set; } = null!;

    public string DeviceInfo { get; set; } = null!;

    public byte[] IpAddress { get; set; } = null!;

    public DateTime LoginAt { get; set; }

    public DateTime LastActivityAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public int SessionStatusId { get; set; }

    public DateTime? LogoutAt { get; set; }

    public virtual SessionStatus SessionStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
