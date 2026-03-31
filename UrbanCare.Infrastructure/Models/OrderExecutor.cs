using System;
using System.Collections.Generic;

namespace UrbanCare.Infrastructure.Models;

public partial class OrderExecutor
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ExecutorId { get; set; }

    public virtual Employee Executor { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
