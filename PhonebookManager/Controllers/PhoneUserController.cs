using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PhonebookManager.Data;

namespace PhonebookManager.Controllers
{
    public class PhoneUserController : Controller
    {
        private readonly DataContext _context;
        private readonly IJSRuntime _js;
        public PhoneUserController(DataContext context, IJSRuntime js)
        {
            _context = context;
            _js = js;

        }
    
        public async Task<IActionResult> Index(string phoneNumber)
        {
            if(!string.IsNullOrEmpty(phoneNumber)) // phoneNumber.ToString().Length == 10
            {
                ViewBag.PhoneNumber = phoneNumber;
            }
            var dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes).ToListAsync();
            return View(dbPhoneLines);
        }
    }
}
