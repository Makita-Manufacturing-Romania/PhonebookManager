using Microsoft.AspNetCore.Mvc;

namespace PhonebookManager.Controllers
{
    public class PhoneUserController : Controller
    {
        public IActionResult Index(string phoneNumber)
        {
            if(!string.IsNullOrEmpty(phoneNumber))
            {
                ViewBag.PhoneNumber = phoneNumber;
            }
            return View();
        }
    }
}
