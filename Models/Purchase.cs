using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    public int? TransactionId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
