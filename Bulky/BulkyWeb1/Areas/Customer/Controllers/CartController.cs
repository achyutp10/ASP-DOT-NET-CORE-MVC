using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb1.Areas.Customer.Controllers
{
    [Area("customer")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
