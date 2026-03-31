
namespace UrbanCare.Domain.Entities;

public partial class Building
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int RegionId { get; set; }

    public int BuildingTypeId { get; set; }

    public short YearBuilt { get; set; }

    public int FloorCount { get; set; }

    public int WallMaterialId { get; set; }

    public int FloorMaterialId { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual BuildingType BuildingType { get; set; } = null!;

    public virtual FloorMaterial FloorMaterial { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Region Region { get; set; } = null!;

    public virtual WallMaterial WallMaterial { get; set; } = null!;

    private Building() { }

    public static Building Create(int id,
                                  string number,
                                  string address,
                                  Region region,
                                  BuildingType buildingType,
                                  short yearBuilt,
                                  int floorCount,
                                  FloorMaterial floorMaterial,
                                  WallMaterial wallMaterial)
    {
        return new Building
        {
            Id = id,
            Number = number ?? throw new ArgumentNullException(nameof(number)),
            Address = address ?? throw new ArgumentNullException(nameof(address)),
            Region = region ?? throw new ArgumentNullException(nameof(region)),
            BuildingType = buildingType ?? throw new ArgumentNullException(nameof(buildingType)),
            YearBuilt = yearBuilt,
            FloorCount = floorCount,
            FloorMaterial = floorMaterial ?? throw new ArgumentNullException(nameof(floorMaterial)),
            WallMaterial = wallMaterial ?? throw new ArgumentNullException(nameof(wallMaterial))
        };
    }
}
