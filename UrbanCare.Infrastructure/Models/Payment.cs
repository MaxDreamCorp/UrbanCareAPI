using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public int PaymentMethodId { get; set; }

    public byte[] PaymentCode { get; set; } = null!;

    public DateTime PaidAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
