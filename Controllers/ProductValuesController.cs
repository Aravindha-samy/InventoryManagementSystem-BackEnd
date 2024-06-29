using InventoryManagementSystem.BO;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystem.Controllers
{
    [EnableCors]
    [Route("Product/[Action]")]
    [ApiController]

    public class ProductValuesController : ControllerBase
    {
        private readonly ProductBO _productBO;
        public ProductValuesController(IProductRepository productRepos)
        {
            _productBO = new ProductBO(productRepos);
        }



        [HttpGet]
        public ActionResult TotalProducts()
        {
            var totalProducts = _productBO.TotalProducts();
            return Ok(totalProducts);
        }

        [HttpGet]
        public ActionResult LowStock()
        {
            var lowStock = _productBO.LowStock();
            return Ok(lowStock);
        }

        [HttpGet]
        public ActionResult SupplierCount()
        {
            var supplierCount = _productBO.SupplierCount();
            return Ok(supplierCount);
        }
        [HttpGet]
        public ActionResult Customer()
        {
            var lowStock = _productBO.Customer();
            return Ok(lowStock);
        }
        [HttpGet]
        public ActionResult MonthProfit()
        {
            var lowStock = _productBO.MonthProfit();
            return Ok(lowStock);
        }
        [HttpGet]
        public ActionResult YearProfit()
        {
            var lowStock = _productBO.YearProfit();
            return Ok(lowStock);
        }


        //Create

        /*
     "ProductName":"Product Z",
    "ProductDescribtion":"Very Good Product",
    "ProductQuantity":123,
    "ProductCost":345,
    "ProductBarCode":"47325844423",
    "ProductCategory":"Electrical",
    "SupplierId":2
        */

        //URL::https://localhost:44343/Product/Post
        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (ModelState.IsValid)
            {
                bool flag = _productBO.InsertProduct(product);

                if (flag)
                {
                    return CreatedAtAction(nameof(Get), new
                    {
                        flag = true,
                    }, product);
                }

                else
                {
                    return StatusCode(500, "Failed To Add");
                }
            }
            return BadRequest(ModelState);
        }


        //Edit
        //URL::https://localhost:44343/Product/Put/34
        //First do Details method and then do edit Method to get data
        [HttpPut("{id}")]
        public ActionResult Put(int id, Product product)
        {

            bool flag = _productBO.UpdateProduct(product);
            if (flag)
            {
                return CreatedAtAction(nameof(Get), new
                {
                    flag = true,
                }, product);
            }

            else
            {
                return StatusCode(500, "Failed to update product.");
            }
        }

        //FetchBYID
        //URL::https://localhost:44343/Product/Get/1
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productBO.FetchProductById(id);
            if (product == null)
            {
                return NotFound("Id is Not Found");
            }
            return Ok(product);
        }
      



        //Delete
        //URL::https://localhost:44343/Product/Delete

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool flag = _productBO.DeleteProduct(id);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return NotFound("Id Not Found");
            }
        }




        //FetchALL
        //URL::https://localhost:44343/Product/GetAll
        [HttpGet]                
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            IEnumerable<Product> products = _productBO.FetchAllProducts(); 
            return Ok(products);
        }
       


        // GET: FetchProductWithSupplier
        //URL::https://localhost:44343/Product/GetProductWithSupplier
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProductWithSupplier()
        {
            IEnumerable<Product> products = _productBO.FetchAllProductUsingJoin();
            return Ok(products);
        }

        // GET: api/Product/FetchProductsUsingFilter
        //URL::https://localhost:44343/Product/GetProductsUsingFilter
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProductsUsingFilter()
        {
            IEnumerable<Product> products = _productBO.FetchProductsUsingFilter();
            return Ok(products);
        }

        // GET: api/Product/FetchProductUsingAssociation
        //URL::https://localhost:44343/Product/GetProductUsingAssociation/?productName=Laptop&quantity=100

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProductUsingAssociation(string productName, int quantity)
        {
            IEnumerable<Product> products = _productBO.FetchProductUsingAssociation(productName, quantity);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSuppliers(int id)
        {
            var supplier = _productBO.FetchSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Id is Not Found");
            }
            return Ok(supplier);
        }
        [HttpGet]
        public ActionResult<Supplier> GetAllSuppliers()
        {
            IEnumerable<Supplier> suppliers = _productBO.FetchAllSuppliers();
            return Ok(suppliers);
        }

        public ActionResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                bool flag = _productBO.InsertUser(user);

                if (flag)
                {
                    return CreatedAtAction(nameof(Get), new
                    {
                        flag = true,
                    }, user);
                }

                else
                {
                    return StatusCode(500, "Failed To Add");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public ActionResult<User> Login(string Username, string PasswordHash)
        {
            var user = _productBO.Login(Username, PasswordHash);
            return StatusCode(200,user);
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users = _productBO.FetchAllUsers();
            return Ok(users);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> ProfitList()
        {
            IEnumerable<Product> list = _productBO.ProfitList();
            return Ok(list);
        }
        //https://localhost:44354/Product/FilterByProduct/
        [HttpGet]
        public ActionResult<IEnumerable<Product>> FilterByProduct(string productName)
        {
            IEnumerable<Product> products = _productBO.FilterByProduct(productName);
            return Ok(products);
        }
        //https://localhost:44354/Product/FilterByCategory/

        [HttpGet]
        public ActionResult<IEnumerable<Product>> FilterByCategory(string productCategory)
        {
            IEnumerable<Product> products = _productBO.FilterByCategory(productCategory);
            return Ok(products);
        }
    }
}

