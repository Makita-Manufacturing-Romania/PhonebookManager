using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
