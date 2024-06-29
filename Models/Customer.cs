using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerUserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string CustomerFistName { get; set; } = null!;

    public string? CustomerLastName { get; set; }

    public string ContactNumber { get; set; } = null!;

    public string CustomerEmailId { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
