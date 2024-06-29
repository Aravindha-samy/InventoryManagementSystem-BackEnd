using InventoryManagementSystem.Models;
using System;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.InventoryException;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace InventoryManagementSystem.Repository
{
    public class ProductRepository : IProductRepository
    {
        //Variable to hold InventoryManagementContext instance
        private readonly InventoryManagementSystemContext _context;

        //Initialize the InventoryManagementContext instance which received as an argument
        public ProductRepository(InventoryManagementSystemContext context)
        {
            _context = context;
        }
        //Initialize the InventoryManagementContext instances
        public ProductRepository()
        {
            _context = new InventoryManagementSystemContext();
        }     

        public int TotalProducts()
        {
            var count = _context.Products.Count();
            Console.WriteLine(count);
            return count;
        }
        public int Customer()
        {
            var count = _context.Customers.Count();
            Console.WriteLine(count);
            return count;
        }
        public decimal MonthProfit()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var totalProfitCurrentMonth = _context.Sales
                .Where(s => s.SalesDate.Month == currentMonth && s.SalesDate.Year == currentYear)
                .Join(
                    _context.Products,
                    s => s.ProductId,
                    p => p.ProductId,
                    (s, p) => s.QuantitySold * p.ProductCost)
                .Sum();
            return (decimal)totalProfitCurrentMonth;
        }
        public decimal YearProfit()
        {
            int currentYear = DateTime.Now.Year;

            var totalProfitCurrentYear = _context.Sales
                .Where(s => s.SalesDate.Year == currentYear)
                .Join(
                    _context.Products,
                    s => s.ProductId,
                    p => p.ProductId,
                    (s, p) => s.QuantitySold * p.ProductCost)
                .Sum();
            return (decimal)totalProfitCurrentYear;
        }
        public int LowStock()
        {
         

            var count = _context.Products.Count(product=>product.ProductQuantity<20);
            Console.WriteLine(count);
            return count;
        }
        public int SupplierCount()
        {
            var count = _context.Suppliers.Count();
            Console.WriteLine(count);
            return count;
        }

        public Product FetchProductById(int productId)
        {
            try
            {
                var product = _context.Products.Find(productId);
                if (productId < 0)
                {
                    throw new ArgumentException("Enter the Valid Product Id");
                }
                if (productId == 0)
                {
                    throw new InventoryManagementException("");
                }
                if (product == null)
                {
                    throw new InventoryNotFoundException($"The product {productId} is not found.Verify the Id");
                }               
                Log.Information("Product Fetched {product}", product);

                return product;
            }

            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Product.Please Contact Admin", e.Message);
            }
        }
        public bool InsertProduct(Product product)
        {
            bool flag = false;
            try
            {
                if (!IsBarcodeUnique(product.ProductBarCode))
                {
                    throw new InventoryManagementException("Barcode already exists. Please use a unique barcode.");
                }
                _context.Products.Add(product);
                _context.SaveChanges();
                flag = true;
            }
            catch(SqlException e)
            {
                throw new InventoryManagementException("Error When Adding Product.Please Contact Admin",e.Message);
            }
            return flag;
        }
        private bool IsBarcodeUnique(string barcode)
        {
            // Query the database to check if any product already has the same barcode
            return !_context.Products.Any(p => p.ProductBarCode == barcode);
        }

        public bool UpdateProduct(Product product)
        {
            bool flag = false;
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                flag = true;

            }
            catch(SqlException e)
            {
                throw new InventoryManagementException(e.Message);
            }
            return flag;
        }
        public bool DeleteProductById(int productId)
        {
            bool flag = false;
            try
            {
                var product = _context.Products.Find(productId);
                if (product != null)
                {
                    _context.Entry(product).State = EntityState.Deleted;
                    
                    _context.SaveChanges();
                    flag = true;
                }
               
            }
            catch(SqlException e)
            {
                throw new InventoryManagementException("Error When Deleting Product.Please Contact Admin", e.Message);
            }            
            return flag;
        }
        public IEnumerable<Product> FetchAllProducts()
        {
            try
            {

                IEnumerable<Product> products= from p in _context.Products
                                               select p;
                return products;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record",e.Message); 
            }
        }
        public IEnumerable<Product> FetchAllProductUsingJoin()
        {
            try
            {

                IEnumerable<Product> products = _context.Products.Include(p => p.Supplier);      
                return products;

            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }
        }
        public IEnumerable<Product> FetchProductsUsingFilter()
        {
            try
            {
                IEnumerable<Product> products = (from p in _context.Products
                                                 where  p.ProductQuantity < 50
                                                 orderby p.ProductQuantity ascending
                                                 select p 
                                               );
                return products;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }

        }
        public IEnumerable<Product> FetchProductUsingAssociation(string productName, int quantity)
        {
            try
            {
                IEnumerable<Product> products = (from p in _context.Products
                            join s in _context.Sales
                            on p.ProductId equals s.ProductId
                            where p.ProductName == productName || s.QuantitySold >= quantity
                                                 orderby productName
                                                 select new Product
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                ProductCategory = p.ProductCategory,
                                ProductQuantity = p.ProductQuantity,

                                Sales = new List<Sale>
                         {
                            new Sale
                            {
                            SalesId=s.SalesId,
                            SalesDate=s.SalesDate,
                            QuantitySold=s.QuantitySold,
                            CustomerId=s.CustomerId

                            }
                         }
                            });
                return products;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }

        }
        public Supplier FetchSupplierById(int supplierId)
        {
            try
            {
                var supplier = _context.Suppliers.Find(supplierId);
                if (supplierId < 0)
                {
                    throw new ArgumentException("Enter the Valid supplier Id");
                }
                if (supplierId == 0)
                {
                    throw new InventoryManagementException("");
                }
                if (supplier == null)
                {
                    throw new InventoryNotFoundException($"The supplier {supplierId} is not found.Verify the Id");
                }
                Log.Information("Product Fetched {supplier}", supplier);

                return supplier;
            }

            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Product.Please Contact Admin", e.Message);
            }
        }
        public IEnumerable<Supplier> FetchAllSuppliers()
        {
            try
            {

                IEnumerable<Supplier> suppliers = from s in _context.Suppliers
                                                  select s;
                return suppliers;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }
        }
        public bool InsertUser(User user)
        {
            bool flag = false;
            try
            {
                // Check if email already exists
                if (IsEmailIdExists(user.Email))
                {
                    throw new InventoryManagementException("Email already exists. Please use a unique email.");
                }

                // Check if username already exists
                if (IsUserNameExists(user.Username))
                {
                    throw new InventoryManagementException("Username already exists. Please use a unique username.");
                }
                user.PasswordHash = HashPassword(user.PasswordHash);
                _context.Users.Add(user);
                _context.SaveChanges();
                flag = true;
            }
            catch (DbUpdateException e)
            {
                // Log the error for debugging purposes
                Log.Information($"Error when adding user: {e.InnerException?.Message}");
                throw new InventoryManagementException("Error when adding user. Please contact the admin.");
            }
            return flag;
        }

        private bool IsEmailIdExists(string email)
        {
            // Check if the email exists in the database
            return _context.Users.Any(u => u.Email == email);
        }

        private bool IsUserNameExists(string username)
        {
            // Check if the username exists in the database
            return _context.Users.Any(u => u.Username == username);
        }
        public bool VerifyPassword(string password, string storedPasswordHash)
        {
            // Convert the stored password hash from Base64 to byte array
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);

            // Get the salt from the first 16 bytes of the hashBytes
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute the hash of the entered password using the same salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false; // Passwords don't match
                }
            }
            return true; // Passwords match
        }

        private string HashPassword(string password)
        {
            // Generate a salt (unique value) for each user
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Create a new instance of the Rfc2898DeriveBytes class
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            // Get the hash value
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and hash into a single byte array
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert the combined bytes to a string and return it
            return Convert.ToBase64String(hashBytes);
        }
        public User Login(string username, string password)
        {
            try
            {
                // Assuming _context represents your database context
                var user = _context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    // Verify the password
                    if (VerifyPassword(password, user.PasswordHash))
                    {
                        return user; // Return the authenticated user
                    }
                }

                return null; // User authentication failed
            }
            catch (Exception ex)
            {
                // Handle exceptions according to your application's error handling strategy
                throw new InventoryManagementException("Error occurred during login.", ex.Message);
            }
        }

        //public User Login(string username, string password)
        //{

        //    try
        //    {
        //        // Assuming _context represents your database context
        //        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);

        //        if (user != null)
        //        {
        //            return user; // Return the authenticated user
        //        }
        //        else
        //        {
        //            return null; // User authentication failed
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions according to your application's error handling strategy
        //        throw new InventoryManagementException("Error occurred during login.", ex.Message);
        //    }

        //}
        public IEnumerable<User> FetchAllUsers()
        {
            try
            {

                IEnumerable<User> users = from u in _context.Users
                                                select u;
                return users;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }
        }
        public IEnumerable<Product> Profitlist()
        {
            try
            {


                var query = (from p in _context.Products
                             join s in _context.Sales on p.ProductId equals s.ProductId
                             orderby s.SalesDate descending
                             //where s.SalesDate.Year == DateTime.Now.Year
                             select new
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 ProductCategory = p.ProductCategory,
                                 ProductQuantity = p.ProductQuantity,
                                 SalesId = s.SalesId,
                                 QuantitySold = s.QuantitySold,
                                 CustomerId = s.CustomerId,
                                 SalesDate = s.SalesDate,
                                 Profit = s.QuantitySold * p.ProductCost,

                             })
                    .ToList() // Execute database query to retrieve data
                   
                    .GroupBy(x => new { x.ProductId, x.ProductName, x.ProductCategory, x.ProductQuantity })
                     
                    .Select(g => new Product
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName,
                        ProductCategory = g.Key.ProductCategory,
                        ProductQuantity = g.Key.ProductQuantity,
                        Sales = g.Select(x => new Sale
                        {
                            SalesId = x.SalesId,
                            QuantitySold = x.QuantitySold,
                            CustomerId = x.CustomerId,
                            Profit = x.Profit,
                            SalesDate = x.SalesDate

                        })

                        .ToArray(),
                        Total_Profit_For_Product = g.Sum(x => x.Profit),
                        Product_Count = g.Count()
                    })
                 .ToArray();

                return query;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }
        }
        public IEnumerable<Product> FilterByProduct(string productName)
        {
            try
            {

                string searchTermLower = productName.ToLower();

                var filteredProducts = _context.Products
                    .Where(p => p.ProductName.ToLower().StartsWith(searchTermLower))
                    .ToArray();


                return filteredProducts;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }

        }
        public IEnumerable<Product> FilterByCategory(string category)
        {
            try
            {
                string searchTermLower = category.ToLower();

                var filteredProducts = _context.Products
                    .Where(p => p.ProductCategory.ToLower().StartsWith(searchTermLower))
                 .ToArray();

                return filteredProducts;
            }
            catch (SqlException e)
            {
                throw new InventoryManagementException("Error When Fetching Record", e.Message);
            }

        }

    }
}


//var monthlyProfits = (from s in _context.Sales
//                      join p in _context.Products on s.ProductId equals p.ProductId
//                      where s.SalesDate.Year == DateTime.Now.Year // or DateTime.Today.Year
//                      group new { s, p } by new { s.SalesDate.Year, s.SalesDate.Month } into g
//                      select new
//                      {
//                          Year = g.Key.Year,
//                          Month = g.Key.Month,
//                          TotalProfit = g.Sum(x => x.s.QuantitySold * x.p.ProductCost)
//                      }).ToList();

//var query = (from s in _context.Sales
//             join p in _context.Products on s.ProductId equals p.ProductId
//             join mp in monthlyProfits on new { s.SalesDate.Year, s.SalesDate.Month } equals new { mp.Year, mp.Month }
//             where s.SalesDate.Year == DateTime.Now.Year // or DateTime.Today.Year
//             orderby s.SalesDate.Year, s.SalesDate.Month, p.ProductCategory
//             select new
//             {
//                 Month = s.SalesDate.Month,
//                 Year = s.SalesDate.Year,
//                 Product_Category = p.ProductCategory,
//                 Product_Count = 1, // Assuming each row represents one product sold
//                 Profit = s.QuantitySold * p.ProductCost,
//                 Total_Profit_Per_Month = mp.TotalProfit
//             }).ToList();

//return (IEnumerable<Sale>)query;