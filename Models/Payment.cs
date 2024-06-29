using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public int? TransactionId { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
