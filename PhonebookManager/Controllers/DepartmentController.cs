using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PhonebookManager.Data;
using PhonebookManager.Models;
using static PhonebookManager.ViewModels.DepartmentViewModel;
using static PhonebookManager.ViewModels.RoleAndUserViewModel;

namespace PhonebookManager.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DataContext _context;
        public INotyfService _notifyService { get; }

        public DepartmentController(DataContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        public async Task<IActionResult> Index(string searchText)
        {
            List<Department> dbDepartments = new();
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                //searchText = searchText.Replace(" ", "");
                dbDepartments = await _context.Departments.Include(x => x.Lines).Where(x=>x.Code.Contains(searchText) || x.Name.Contains(searchText))
                    .ToListAsync();
            }
            else
            {
                dbDepartments = await _context.Departments.Include(x => x.Lines).ToListAsync();
            }
            var dbUsers = await _context.AppUsers.Include(x => x.Role).ToListAsync();
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
                foreach (var option in selectedOptions)
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



        public async Task<IActionResult> ShowEditModal(uint id)
        {
            var dbDep = _context.Departments.Include(x => x.Lines).FirstOrDefault(x => x.Id == id);
            var dbUsers = await _context.AppUsers.Include(x => x.Role).ToListAsync();
            var dbPhoneLines = await _context.PhoneLines.Include(x => x.Department).ToListAsync();


            if (dbDep == null) return NoContent();

            var dep = new DepartmentVM()
            {
                Id = dbDep.Id,
                Name = dbDep.Name,
                Code = dbDep.Code,
                Manager = dbDep.Manager,
                Responsible = dbDep.Responsible,
                Lines = dbDep.Lines,
                PhoneLines = dbPhoneLines,
                AppUsers = dbUsers


            };

            return View("EditDepartment", dep);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEdit(DepartmentVM dep)
        {
            var dbDep = _context.Departments.Include(x => x.Lines).FirstOrDefault(x => x.Id == dep.Id);
            List<PhoneLine> newLines = _context.PhoneLines.Where(u => dep.AddLineIds.Contains(u.Id)).ToList();
            List<PhoneLine> removeLines = _context.PhoneLines.Where(u => dep.RmoveLineIds.Contains(u.Id)).ToList();


            if (dbDep is not null)
            {
                dbDep.Name = dep.Name;
                dbDep.Code = dep.Code;
                dbDep.ManagerId = dep.ManagerId;
                dbDep.ResponsibleId = dep.ResponsibleId;

                dbDep.Lines.AddRange(newLines);
                dbDep.Lines = dbDep.Lines.Except(removeLines).ToList();

                _context.Departments.Update(dbDep);
                await _context.SaveChangesAsync();
                _notifyService.Success($"{dbDep.Name} has been updated.");

            }

            return RedirectToAction(nameof(Index));
            //return RedirectToAction("Index", "Department");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dbDep = await _context.Departments.Include(x => x.Lines).FirstOrDefaultAsync(x => x.Id == id);
            if (dbDep != null)
            {
                if (dbDep.Lines is not null || dbDep.Lines.Count != 0)
                {
                    dbDep.Lines.Clear();
                }
                _context.Departments.Remove(dbDep);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SearchDepartment(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.Replace(" ", "");

                var dbDepartments = await _context.Departments.Include(x => x.Lines).Where(x => x.Code.Contains(searchText) || x.Name.Contains(searchText))
                        .ToListAsync();
                if (dbDepartments == null)
                {
                    return Json("Not found");

                }
                else
                {
                    return Json("");
                }

            }
            return Json("");
        }
    }
}
