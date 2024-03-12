using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class PhoneLineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
