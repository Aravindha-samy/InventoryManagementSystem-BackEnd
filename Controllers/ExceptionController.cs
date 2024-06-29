using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ExceptionController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
        [Route("Error/{code}")]
        public IActionResult StatusCodeError(int code)
        {
            Response.Clear();
            Response.StatusCode = code;
            switch(code)
            {
                    case 401:
                    return View("UnAuthorizedError");
                    
                case 404:
                    return View("PagenotFoundError");
                default:
                    return View("Home/Error");

            }
        }
        public IActionResult UnAuthorized()
        {
            string role = "User";
            if (role != "Admin")
            {
                return Unauthorized();
            }
            else
            { 
                return View();
            }
        }
    }
}
