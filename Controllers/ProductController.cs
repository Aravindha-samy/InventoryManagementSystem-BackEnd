using InventoryManagementSystem.BO;
using InventoryManagementSystem.InventoryException;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace InventoryManagementSystem.Controllers
{


    public class ProductController : Controller
    {
        private readonly ProductBO _productBO;

        public ProductController(ProductBO productRepos)
        {
            _productBO = productRepos;
        }


        public ActionResult Index()
        {
            ViewBag.total = _productBO.TotalProducts();
            ViewBag.lowStock=_productBO.LowStock();
            ViewBag.supplierCount = _productBO.SupplierCount();
            return View();
        }

        //// GET: ProductController/Details/5
        //[Route("~/[controller]/Details")]
        //[Route("~/[controller]/Details/{id:PositiveConstraint}")]
        public ActionResult Details(int id)
        {
            Log.Information("Fetch By Id Method Invoked");
            try
            {
                var product = _productBO.FetchProductById(id);
                return View("Details", product);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Id Invalid");
                ViewData["Response"] = "Enter the Valid Product Id";
                return View("Details");
            }
            catch (InventoryManagementException)
            {                
                ViewData["Response"] = "Enter the Product Id";
                return View("Details");
            }
            catch (InventoryNotFoundException ex)
            {
                Log.Error(ex, "Id Not Found");
                ViewData["Response"] = "Id Not Found!!!";
                return View("Details");
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return View("Details");
            }
}

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Log.Information("Create Method Invoked");
                    bool flag = _productBO.InsertProduct(product);

                    if (flag)
                    {
                        ViewData["Response"] = "Product Added SuccessFully";
                    }                    
                    return View("Create");
                }
                else
                {
                    return View();
                }
            }
           
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while Creating Product");
                ViewData["Response"] = "An error occurred while creating product . Please try again later or contact admin.";
                return View();
            }
            catch (InventoryManagementException)
            {
                ViewData["Response"] = "Enter the BarCode As Unique";
                return View();

            }
        }

        // GET: ProductController/Edit/5
        //[Route("~/[controller]/Edit")]
        //[Route("~/[controller]/Edit/{id:PositiveConstraint}")]


        public ActionResult Edit(int id)
        {

            Log.Information("Edit Method Invoked");
            try
            {
                var product = _productBO.FetchProductById(id);
                return View("Edit", product);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Id Invalid");
                ViewData["Response"] = "Enter the Valid Product Id";
                return View("Edit");
            }
            catch (InventoryManagementException)
            {
                ViewData["Response"] = "Enter the Product Id";
                return View("Edit");
            }
            catch (InventoryNotFoundException ex)
            {
                Log.Error(ex, "Id Not Found");
                ViewData["Response"] = "Id Not Found!!!";
                return View("Edit");
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return View("Edit");
            }
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                Log.Information("Edit Method Invoked");
                bool flag = _productBO.UpdateProduct(product);

                if (flag)
                {
                    ViewData["Response"] = "Product Updated SuccessFully";
                }
                else
                {
                    ViewData["Response"] = "Product Failed to Update";
                }
                return View("Edit", product);
            }
            return View();


        }

        //[Route("~/[controller]/Delete")]
        //[Route("~/[controller]/Delete/{id:PositiveConstraint}")]
        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {

            Log.Information("Delete Method Invoked");
            try
            {
                var product = _productBO.FetchProductById(id);
                return View("Delete", product);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Id Invalid");
                ViewData["Response"] = "Enter the Valid Product Id";
                return View("Delete");
            }
            catch (InventoryManagementException)
            {
                ViewData["Response"] = "Enter the Product Id";
                return View("Delete");
            }
            catch (InventoryNotFoundException ex)
            {
                Log.Error(ex, "Id Not Found");
                ViewData["Response"] = "Id Not Found!!!";
                return View("Delete");
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return View("Delete");
            }
        }

        

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            Log.Information("Delete Method Invoked");
            bool flag = _productBO.DeleteProduct(id);


            if (flag)
            {
                ViewData["Response"] = "Product Deleted SuccessFully";
            }
            else
            {
                ViewData["Response"] = "Product Failed to Delete";
            }
            return View("Delete");
        }
        [HttpGet]
        public ActionResult FetchAll()
        {
            try { 
            Log.Information("FetchAll Method Invoked");
            IEnumerable<Product> products = _productBO.FetchAllProducts();
            return View("FetchAll", products);
        }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return Redirect("Create");
            }

        }
        public ActionResult FetchProductWithSupplier()
        {
            try
            {
                Log.Information("Fetch All Product Using Join Method Invoked");
                var products = _productBO.FetchAllProductUsingJoin();
                return View("FetchProductWithSupplier", products);
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return Redirect("Create");
            }

        }

        public ActionResult FetchProductsUsingFilter()
        {
            try
            {
                Log.Information("Fetch Products Using Filter Method Invoked");
                IEnumerable<Product> products = _productBO.FetchProductsUsingFilter();
                return View("FetchProductsUsingFilter", products);
            }
           
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return Redirect("Create");
            }
        
        }
        public ActionResult FetchProductUsingAssociation(string productName, int quantity)
        {
            try
            {
                Log.Information(productName,quantity);
                Log.Information("Fetch Product Using Association Method Invoked");
                IEnumerable<Product> products = _productBO.FetchProductUsingAssociation(productName, quantity);
                return View("FetchProductUsingAssociation", products);
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error occurred while fetching product details");
                ViewData["Response"] = "An error occurred while fetching product details. Please try again later or contact admin.";
                return Redirect("Create");
            }
        }
    }
}
