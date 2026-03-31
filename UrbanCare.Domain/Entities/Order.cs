namespace UrbanCare.Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int ResidentId { get; set; }

    public int OrderCategoryId { get; set; }

    public int BuildingId { get; set; }

    public int? ApartmentId { get; set; }

    public string Description { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    public int? DispatcherId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ChangedAt { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Apartment? Apartment { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual Employee? Dispatcher { get; set; }

    public virtual OrderCategory OrderCategory { get; set; } = null!;

    public virtual ICollection<OrderExecutor> OrderExecutors { get; set; } = new List<OrderExecutor>();

    public virtual ICollection<OrderMaterial> OrderMaterials { get; set; } = new List<OrderMaterial>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Priority Priority { get; set; } = null!;

    public virtual Resident Resident { get; set; } = null!;

    public virtual OrderStatus Status { get; set; } = null!;

    private Order() { }

    public static Order Create(int id,
                Resident resident,
                 string description,
                 OrderCategory orderCategory,
                 Building building,
                 Apartment? apartment,
                 Priority priority,
                 string contactPhone,
                 string contactEmail,
                 OrderStatus status)
    {
        return new Order
        {
            Id = id,
            Resident = resident,
            Description = description ?? throw new ArgumentNullException(nameof(description)),
            OrderCategory = orderCategory ?? throw new ArgumentNullException(nameof(orderCategory)),
            Building = building ?? throw new ArgumentNullException(nameof(building)),
            Apartment = apartment,
            Priority = priority ?? throw new ArgumentNullException(nameof(priority)),
            ContactPhone = contactPhone ?? throw new ArgumentNullException(nameof(contactPhone)),
            ContactEmail = contactEmail ?? throw new ArgumentNullException(nameof(contactEmail)),
            Status = status ?? throw new ArgumentNullException(nameof(status)),
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Order CreateForUpdate(int id,
                Resident resident,
                 string description,
                 OrderCategory orderCategory,
                 Building building,
                 Apartment? apartment,
                 Priority priority,
                 string contactPhone,
                 string contactEmail,
                 OrderStatus status,
                 DateTime createdAt)
    {
        return new Order
        {
            Id = id,
            Resident = resident,
            Description = description ?? throw new ArgumentNullException(nameof(description)),
            OrderCategory = orderCategory ?? throw new ArgumentNullException(nameof(orderCategory)),
            Building = building ?? throw new ArgumentNullException(nameof(building)),
            Apartment = apartment,
            Priority = priority ?? throw new ArgumentNullException(nameof(priority)),
            ContactPhone = contactPhone ?? throw new ArgumentNullException(nameof(contactPhone)),
            ContactEmail = contactEmail ?? throw new ArgumentNullException(nameof(contactEmail)),
            Status = status ?? throw new ArgumentNullException(nameof(status)),
            CreatedAt = createdAt,
            ChangedAt = DateTime.UtcNow
        };
    }
}
