namespace UrbanCare.Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Role1 { get; set; } = null!;

    public string AccessLevel { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
