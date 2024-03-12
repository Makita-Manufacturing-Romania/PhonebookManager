using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class DBUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
