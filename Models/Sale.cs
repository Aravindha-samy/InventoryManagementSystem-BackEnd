using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models;

public partial class Sale
{
    internal int year;

    public int SalesId { get; set; }

    public int? QuantitySold { get; set; }

    public DateOnly SalesDate { get; set; }

    public int? ProductId { get; set; }

    public int? CustomerId { get; set; }

    public int? TransactionId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Transaction? Transaction { get; set; }
    public decimal? Profit { get; internal set; }
}
