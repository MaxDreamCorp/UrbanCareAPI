
namespace UrbanCare.Domain.Entities;

public partial class Apartment
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int BuildingId { get; set; }

    public int? Entrance { get; set; }

    public int Floor { get; set; }

    public int RoomsCount { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();

    private Apartment() { }

    public static Apartment Create(int id,
                                   int number,
                                   Building building,
                                   int? entrance,
                                   int floor,
                                   int roomsCount)
    {
        return new Apartment
        {
            Id = id,
            Number = number,
            Building = building ?? throw new ArgumentNullException(nameof(building)),
            Entrance = entrance,
            Floor = floor,
            RoomsCount = roomsCount
        };
    }
}
