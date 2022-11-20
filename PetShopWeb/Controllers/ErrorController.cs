using Microsoft.AspNetCore.Mvc;

namespace PetShopWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
