using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public DateOnly? TransactionDate { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
