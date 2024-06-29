using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var username = collection["UserName"].ToString();
                var role = collection["Role"].ToString();
                HttpContext.Session.SetString("SessionUserName", username);
                HttpContext.Session.SetString("SessionRole", role);
                return Redirect("/Product/Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("SessionUserName");
            HttpContext.Session.Remove("SessionRole");
            return Redirect("/");
        }
        
    }
}

