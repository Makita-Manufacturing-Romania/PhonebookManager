using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PhonebookManager.Data;
using PhonebookManager.Models;
using static PhonebookManager.ViewModels.PhoneLineViewModel;
using static PhonebookManager.ViewModels.RoleAndUserViewModel;

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
            var dbPhoneLines = await _context.PhoneLines.Include(z => z.LineUsers).Include(u => u.Changes).ToListAsync();
            var dbUsers = await _context.AppUsers.Include(x => x.Role).ToListAsync();
            var dbDepartments = await _context.Departments.Include(x=>x.Lines).ToListAsync();
            //var dbPhoneUsers = await _context.PhoneUsers.ToListAsync();

            var viewModel = new PhoneLineVM()
            {
                AppUsers = dbUsers,
                Departments = dbDepartments,
                PhoneLines = dbPhoneLines,
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Allocate(uint userId, uint depId, string lineNo)
        {
            var dbUser = await _context.AppUsers.FirstOrDefaultAsync(x=>x.Id == userId);
            var dbDep = await _context.Departments.FirstOrDefaultAsync(x=>x.Id == depId);
            PhoneLine line = new PhoneLine
            {
                PhoneNumber = lineNo,
                Department = dbDep,
                LineOwner = new PhoneUser { Name = dbUser.Name, Badge = dbUser.BadgeNo, Email = dbUser.Email }
            };
            _context.PhoneLines.Add(line);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
