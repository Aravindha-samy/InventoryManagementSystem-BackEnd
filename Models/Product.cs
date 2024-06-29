using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models;

public partial class Product
{
    internal decimal? Total_Profit_For_Product;
   
   


    [Display(Name = "Product Id")]
    public int ProductId { get; set; }

    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "Please Enter the Product Name")]
    [MinLength(3, ErrorMessage = "Product Name Should have minimum 3 Characters")]
    [MaxLength(15, ErrorMessage = "Product Name must not Exceed 15 Characters")]
    public string ProductName { get; set; } = null!;

    [Required(ErrorMessage = "Product Description is Mandatory")]
    [MinLength(10, ErrorMessage = "Product Description Should have minimum 10 Characters")]
    [MaxLength(40, ErrorMessage = "Product Description must not Exceed 40 Characters")]
    [Display(Name = "Product Description")]
    public string? ProductDescribtion { get; set; }
    [Required(ErrorMessage = "Product Quantity is Mandatory")]
    [Display(Name = "Product Quantity")]
    public int ProductQuantity { get; set; }

    [Required(ErrorMessage = "Product Cost is Mandatory")]
    [Display(Name = "Product Cost")]
    public decimal ProductCost { get; set; }

    [Required(ErrorMessage = "Product Barcode is Mandatory")]
    [Display(Name = "Product Barcode")]
    //[BarcodeAttribute]
    public string ProductBarCode { get; set; } = null!;
    [Required(ErrorMessage = "Product Category is Mandatory")]
    [Display(Name = "Product Category")]
    public string? ProductCategory { get; set; }
    [Required(ErrorMessage = "Product Supplier Id is Mandatory")]
    [Display(Name = "Supplier Id")]
    public int? SupplierId { get; set; }


    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Supplier? Supplier { get; set; }
    

internal int Product_Count;
    internal decimal? TotalProfitForProduct;
    internal int ProductCount;
}
