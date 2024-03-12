using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
