using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;
using PhonebookManager.Models;
using static PhonebookManager.ViewModels.DepartmentViewModel;
using static PhonebookManager.ViewModels.RoleAndUserViewModel;

namespace PhonebookManager.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DataContext _context;

        public DepartmentController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dbUsers = await _context.AppUsers.Include(x => x.Role).ToListAsync();
            var dbDepartments = await _context.Departments.Include(x => x.Lines).ToListAsync();
            var dbPhoneLines = await _context.PhoneLines.ToListAsync();

            var viewModel = new DepartmentVM()
            {
                AppUsers = dbUsers,
                Departments = dbDepartments,
                PhoneLines = dbPhoneLines,
                SecondListPhoneLines = new List<PhoneLine>()
            };
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(List<string> selectedOptions, string depCode, string depName, int depManager, int depResponsible)
        {
            var dep = new Department();
            var phoneLines = new List<PhoneLine>();
            var dbPhoneLines = await _context.PhoneLines.ToListAsync();
            foreach (var line in dbPhoneLines)
            {
                foreach(var option in selectedOptions)
                {
                    if (line.Id.ToString() == option)
                    {
                        phoneLines.Add(line);
                    }

                }
            }
            var manager = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == depManager);
            var responsible = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == depResponsible);

            dep.Lines = phoneLines;
            dep.Manager = manager;
            dep.Responsible = responsible;
            dep.Code = depCode;
            dep.Name = depName;

            _context.Departments.Add(dep);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //return Json(new { success = true });
        }

    }
}
