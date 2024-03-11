using Microsoft.AspNetCore.Mvc;
using PhonebookManager.Models;
using System.Diagnostics;

namespace PhonebookManager.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
