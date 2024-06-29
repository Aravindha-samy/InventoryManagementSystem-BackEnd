using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierUserName { get; set; } = null!;

    public string SupplierFistName { get; set; } = null!;

    public string? SupplierLastName { get; set; }

    public string ContactNumber { get; set; } = null!;

    public string SupplierEmailId { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
