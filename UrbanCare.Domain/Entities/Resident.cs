using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanCare.Domain.Entities;

public partial class Resident
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ApartmentId { get; set; }

    public DateOnly MovingIntoDate { get; set; }

    public DateOnly? MovingOutDate { get; set; }

    public sbyte IsLiving { get; set; }

    [NotMapped]
    public bool IsLivingBool
    {
        get => IsLiving == 1;
        set
        {
            IsLiving = (sbyte)(value ? 1 : 0);
        }
    }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;

    private Resident() { }

    public static Resident Create(int id,
                                    User user,
                                    Apartment apartment,
                                    DateOnly movingIntoDate,
                                    DateOnly? movingOutDate,
                                    bool isLiving)
    {
        return new Resident
        {
            Id = id,
            User = user,
            Apartment = apartment,
            MovingIntoDate = movingIntoDate,
            MovingOutDate = movingOutDate,
            IsLivingBool = isLiving,
        };
    }
}
