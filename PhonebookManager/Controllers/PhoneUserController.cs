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
        public PhoneUserController(DataContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Allocate(string lineOwnerId, uint depId, string lineNo, string[] userIds)
        {
            var viewEmployees = await _context.Employees.FromSqlRaw("SELECT * FROM All_employees").ToListAsync();
            var lineOwner = viewEmployees.FirstOrDefault(x=>x.EmployeeID == lineOwnerId);
            var filteredUsers = viewEmployees.Where(emp => userIds.Contains(emp.EmployeeID)).ToList();
            var dbDep = await _context.Departments.FirstOrDefaultAsync(x=>x.Id == depId);

            List<PhoneUser> lineUsers = new();

            foreach (var user in filteredUsers)
            {
                if (user != null)
                {
                    lineUsers.Add(new PhoneUser {Badge = user.EmployeeID, Email = user.Email, Name = user.FullName });
                }
            }

            PhoneLine line = new PhoneLine
            {
                PhoneNumber = lineNo,
                Department = dbDep,
                LineOwner = new PhoneUser { Name = lineOwner.FullName, Badge = lineOwner.EmployeeID, Email = lineOwner.Email },
                LineUsers = lineUsers
            };
            _context.PhoneLines.Add(line);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> AutocompleteSearchUsers(string searchText)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    string[] words = searchText.Split(' ');
                    string firstWord = words[0];
                    List<Employee> Employees = new();
                    try
                    {
                        Employees = await _context.Employees.FromSqlRaw("SELECT * FROM All_employees").ToListAsync();
                    }
                    catch { }

                    if (Employees is not null || Employees.Count() != 0)
                    {
                        Employees = Employees.Where(x => x.EmployeeID.Contains(searchText.Replace(" ", "")) || x.FullName.Contains(searchText.Replace(" ", ""))).ToList();
                        var employeesFiltered = (from user in Employees
                                                 select new
                                                 {
                                                     label = user.EmployeeID + " - " + user.FullName,
                                                     val = user.EmployeeID
                                                 }).ToList();
                        if (employeesFiltered.Count != 0)
                        {
                            return Json(employeesFiltered);

                        }
                        else
                        {
                            return Json("");
                        }
                    }
                }

                return Json("");

            }
            catch (Exception ex)
            {
                return Json("Error-" + ex.Message + " stackTrace-" + ex.StackTrace);
            }
        }

        public async Task<JsonResult> FindUser(string searchText)
        {
            var dbUser = await _context.Employees.FromSqlRaw("SELECT * FROM All_employees").FirstOrDefaultAsync(x => x.EmployeeID == searchText.Replace(" ", ""));
            if (dbUser == null)
            {
                return Json("");
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dbUser);
            return Json(json);
        }
    }
}
