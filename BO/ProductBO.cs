using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository;
using InventoryManagementSystem.InventoryException;
using Serilog;
namespace InventoryManagementSystem.BO
{
    public class ProductBO
    {
        
        private readonly IProductRepository _productRepos;
        public ProductBO(IProductRepository productRepos)
        {             
                _productRepos = productRepos;
        }
       public int TotalProducts()
        {
            return _productRepos.TotalProducts();
        }
        public int LowStock()
        {
            return _productRepos.LowStock();
        }
        public int Customer()
        {
            return _productRepos.Customer();
        }
        public decimal MonthProfit()
        {
            return _productRepos.MonthProfit();
        }
        public decimal YearProfit()
        {
            return _productRepos.YearProfit();
        }
        public int SupplierCount()
        {
            return _productRepos.SupplierCount();
        }
        public bool InsertProduct(Product product)
        {
            Log.Information("Product Adding {product}", product);           
            bool flag = _productRepos.InsertProduct(product);
            Log.Information("Product Added Successfully ", flag);
            return flag;
        }
       
        public Product FetchProductById(int id)
        {
            
                Log.Information("Product Fetching {productid}", id);
                var product = _productRepos.FetchProductById(id);
                Log.Information("Product Fetced {product}", product);
                return product;
           
        }
        
        public bool UpdateProduct(Product product)
        {
            
                Log.Information("Updating the Product {product}", product);
                bool flag = _productRepos.UpdateProduct(product);
                Log.Information("Product Updated Status{flag}",flag);
                return flag;
        }
        public bool DeleteProduct(int id)
        {
            Log.Information("Delete the Product {id}", id);
            bool flag = _productRepos.DeleteProductById(id);
            Log.Information("Deleting the Product Status{flag}", flag);
            return flag;
        }
        public IEnumerable<Product> FetchAllProducts()
        {
            Log.Information("Product Fetching All");
            var list = _productRepos.FetchAllProducts();
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;
            
        }
        
        public IEnumerable<Product> FetchAllProductUsingJoin()
        {
            Log.Information("Product Fetching All Using Join");
            var list = _productRepos.FetchAllProductUsingJoin();
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;
        }

        public IEnumerable<Product> FetchProductsUsingFilter()
        {
            Log.Information("Product Fetching All Using Filter");
            var list =_productRepos.FetchProductsUsingFilter();
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;
        }
        public IEnumerable<Product> FetchProductUsingAssociation(string productName, int quantity)
        {
            Log.Information("Product Fetching All Using Association");
            var list = _productRepos.FetchProductUsingAssociation(productName, quantity);
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;
        }
        public Supplier FetchSupplierById(int id)
        {

            Log.Information("Product Fetching {productid}", id);
            var supplier = _productRepos.FetchSupplierById(id);
            Log.Information("Product Fetced {product}", supplier);
            return supplier;

        }
        public IEnumerable<Supplier> FetchAllSuppliers()
        {
            Log.Information("Product Fetching All");
            var list = _productRepos.FetchAllSuppliers();
            Log.Information("Product {Supplierlist} fetched SuccessFully ", list.ToString());
            return list;

        }
         public bool InsertUser(User user)
        {
            Log.Information("User Adding {product}", user);           
            bool flag = _productRepos.InsertUser(user);
            Log.Information("user Added Successfully ", flag);
            return flag;
        }
        public User Login(string Username, string PasswordHash)
        {
            Log.Information("Product Fetching All Using Association");
            var user = _productRepos.Login(Username, PasswordHash);            
            return user;
        }
        public IEnumerable<User> FetchAllUsers()
        {
            Log.Information("Product Fetching All");
            var list = _productRepos.FetchAllUsers();
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;

        }
        public IEnumerable<Product> ProfitList()
        {
            Log.Information("Product Fetching All");
            var list = _productRepos.Profitlist();
            //Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;

        }
        public IEnumerable<Product> FilterByProduct(string productName)
        {
            Log.Information("Product Fetching All Using Association");
            var list = _productRepos.FilterByProduct(productName);
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;
        }
        public IEnumerable<Product> FilterByCategory(string productcategory)
        {
            Log.Information("Product Fetching All Using Association");
            var list = _productRepos.FilterByCategory(productcategory);
            Log.Information("Product {Productlist} fetched SuccessFully ", list.ToString());
            return list;
        }

    }
}
