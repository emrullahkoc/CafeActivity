using Microsoft.AspNetCore.Mvc;

namespace CafeActivity.Areas.Management.Controllers
{
    [Area("Management")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
