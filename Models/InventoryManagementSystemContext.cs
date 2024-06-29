using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Models;

public partial class InventoryManagementSystemContext : DbContext
{
    public InventoryManagementSystemContext()
    {
    }

    public InventoryManagementSystemContext(DbContextOptions<InventoryManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=WINDOWS-L7564LO\\SQLEXPRESS;Initial Catalog=InventoryManagementSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__8CB2869955836887");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Contact_Number");
            entity.Property(e => e.CustomerEmailId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Customer_EmailId");
            entity.Property(e => e.CustomerFistName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Customer_FistName");
            entity.Property(e => e.CustomerLastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Customer_LastName");
            entity.Property(e => e.CustomerUserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Customer_UserName");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__DA6C7FC119AE7520");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate).HasColumnName("Payment_Date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Payment__Transac__5FB337D6");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__9834FBBAB67B49AE");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductBarCode, "UQ__Product__3F80244D44021FEC").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.ProductBarCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Product_BarCode");
            entity.Property(e => e.ProductCategory)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Product_Category");
            entity.Property(e => e.ProductCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Product_Cost");
            entity.Property(e => e.ProductDescribtion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Product_Describtion");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Product_Name");
            entity.Property(e => e.ProductQuantity).HasColumnName("Product_Quantity");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Product__Supplie__4E88ABD4");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__543E6D43BCBE8491");

            entity.ToTable("Purchase");

            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.PurchaseDate).HasColumnName("Purchase_Date");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Purchase__Produc__5629CD9C");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Purchase__Suppli__571DF1D5");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Purchase__Transa__5812160E");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK__Sales__32123EDA28205394");

            entity.Property(e => e.SalesId).HasColumnName("Sales_Id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.QuantitySold).HasColumnName("Quantity_Sold");
            entity.Property(e => e.SalesDate).HasColumnName("Sales_Date");
            entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Sales__Customer___5BE2A6F2");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Sales__Product_I__5AEE82B9");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Sales)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Sales__Transacti__5CD6CB2B");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__83918DB86DD498BA");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Contact_Number");
            entity.Property(e => e.SupplierEmailId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Supplier_EmailId");
            entity.Property(e => e.SupplierFistName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Supplier_FistName");
            entity.Property(e => e.SupplierLastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Supplier_LastName");
            entity.Property(e => e.SupplierUserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Supplier_UserName");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__9A8D5605CD179EE0");

            entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");
            entity.Property(e => e.TransactionDate).HasColumnName("Transaction_Date");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C01A3F481");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E4B780D444").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534F232C2A1").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
