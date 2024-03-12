using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class PhoneUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
