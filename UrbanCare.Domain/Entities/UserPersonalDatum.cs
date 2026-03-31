namespace UrbanCare.Domain.Entities;

public partial class UserPersonalDatum
{
    public int Id { get; private set; }

    public int PasportDataId { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public string Snils { get; private set; } = null!;

    public string Inn { get; private set; } = null!;

    public virtual PassportDatum PasportData { get; private set; } = null!;

    public virtual ICollection<User> Users { get; private set; } = new List<User>();


    private UserPersonalDatum() { }

    public static UserPersonalDatum Create(int id, DateOnly dateOfBirth, string snils, string inn, PassportDatum pasportData)
    {
        return new UserPersonalDatum
        {
            Id = id,
            DateOfBirth = dateOfBirth,
            Snils = snils ?? throw new ArgumentNullException(nameof(snils)),
            Inn = inn ?? throw new ArgumentNullException(nameof(inn)),
            PasportData = pasportData ?? throw new ArgumentNullException(nameof(pasportData)),
        };
    }
}
