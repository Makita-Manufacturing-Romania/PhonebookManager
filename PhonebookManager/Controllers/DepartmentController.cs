using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
