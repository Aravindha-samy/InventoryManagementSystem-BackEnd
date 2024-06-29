using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.Repository
{
    public interface IProductRepository
    {
        int TotalProducts();
        int LowStock();
        int SupplierCount();
        int Customer();
        decimal MonthProfit();
        decimal YearProfit();
        bool InsertProduct(Product product);
        bool UpdateProduct(Product product);
        Product FetchProductById(int productId);
        IEnumerable<Product> FetchAllProducts();        
        bool DeleteProductById(int productId);
        IEnumerable<Product> FetchAllProductUsingJoin();
        public IEnumerable<Product> FetchProductsUsingFilter();
        public IEnumerable<Product> FetchProductUsingAssociation(string productName, int quantity);
        Supplier FetchSupplierById(int supplierId);
        IEnumerable<Supplier> FetchAllSuppliers();

        bool InsertUser(User user);
        public User Login(string UserName, string PasswordHash);
        IEnumerable<User> FetchAllUsers();

        IEnumerable<Product> Profitlist();
        IEnumerable<Product> FilterByProduct(string productName);
        IEnumerable<Product> FilterByCategory(string productcategory);
    }
}
