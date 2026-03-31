namespace UrbanCare.Domain.Entities;

public partial class User
{

    public int Id { get; private set; }

    public string Fullname { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Phone { get; private set; } = null!;

    public byte[] PasswordHash { get; private set; } = null!;

    public byte[] PasswordSalt { get; private set; } = null!;

    public int RoleId { get; private set; }

    public int UserPersonalDataId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public virtual ICollection<Employee> Employees { get; private set; } = new List<Employee>();

    public virtual ICollection<Resident> Residents { get; private set; } = new List<Resident>();

    public virtual Role Role { get; private set; } = null!;

    public virtual UserPersonalDatum UserPersonalData { get; private set; } = null!;

    public virtual ICollection<UserSession> UserSessions { get; private set; } = new List<UserSession>();



    private User() { }

    public static User Create(
        int id,
        string fullname,
        string email,
        string phone,
        byte[] passwordHash,
        byte[] passwordSalt,
        int roleId,
        UserPersonalDatum userPersonalData)
    {
        return new User
        {
            Id = id,
            Fullname = fullname ?? throw new ArgumentNullException(nameof(fullname)),
            Email = email ?? throw new ArgumentNullException(nameof(email)),
            Phone = phone ?? throw new ArgumentNullException(nameof(phone)),
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash)),
            PasswordSalt = passwordSalt ?? throw new ArgumentNullException(nameof(passwordSalt)),
            CreatedAt = DateTime.Now,
            RoleId = roleId,
            UserPersonalData = userPersonalData ?? throw new ArgumentNullException(nameof(userPersonalData)),
        };
    }

}
