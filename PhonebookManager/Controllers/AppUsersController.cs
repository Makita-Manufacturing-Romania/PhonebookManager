using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;
using PhonebookManager.Models;
using static PhonebookManager.ViewModels.RoleAndUserViewModel;

namespace PhonebookManager
{
    public class AppUsersController : Controller
    {
        private readonly DataContext _context;
        public INotyfService _notifyService { get; }

        public AppUsersController(DataContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }


        // GET: AppUsers
        public async Task<IActionResult> Index(string searchText, string depName)
        {
            var dbUsers = await _context.AppUsers.Include(x => x.Role).ToListAsync();
            var dbDepartments = await _context.Departments.Include(x => x.Lines).ToListAsync();
            ViewBag.DepartmentName = "All departments";

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                dbUsers = await _context.AppUsers.Include(x => x.Role).Where(x => x.BadgeNo.Contains(searchText) || x.Name.Contains(searchText))
                    .ToListAsync();
            }

            //if (!string.IsNullOrEmpty(depName))
            //{
            //    if (depName != "0")
            //    {
            //        ViewBag.DepartmentName = depName;
            //        dbUsers = await _context.AppUsers.Where(x => x.Department.Name.Contains(depName)).ToListAsync();
            //        if (dbUsers.Count == 0)
            //        {
            //            dbUsers = await _context.AppUsers.Include(x => x.Role).ToListAsync();
            //            _notifyService.Information("No users found for " + depName);
            //        }
            //    }
            //}

            var dbRoles = await _context.AppRoles.ToListAsync();
            var viewModel = new AppUserVM()
            {
                AppUsersList = dbUsers,
                UserRolesList = dbRoles,
                DepartmentList = dbDepartments
            };
            return View(viewModel);
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public async Task<IActionResult> Create()
        {
            var dbRoles = await _context.AppRoles.ToListAsync();
            //var roles = _context.AppRoles.Select(s => new SelectListItem
            //{
            //    Value = s.Role,
            //    Text = s.Role
            //}).ToList();

            //var selectList = new SelectList(roles, "Value", "Text");

            var viewModel = new AppUserVM()
            {
                // UserRoles = selectList,
                UserRolesList = dbRoles
            };
            return View(viewModel);
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUserVM appUser) // [Bind("Id,AdIdentity,Email,BadgeNo,Name,RoleName")]
        {
            var dbUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.BadgeNo == appUser.BadgeNo);
            var dbRole = _context.AppRoles.FirstOrDefault(x => x.Role == appUser.RoleName);
            //ModelState.Remove("UserRoles");

            if (ModelState.IsValid && dbUser == null)
            {
                var user = new AppUser()
                {
                    Name = appUser.Name,
                    AdIdentity = appUser.AdIdentity,
                    BadgeNo = appUser.EmployeeID,
                    Email = string.IsNullOrEmpty(appUser.Email) && !string.IsNullOrEmpty(appUser.AdIdentity) ? $"{appUser.AdIdentity}@mmrmakita.eu" : !string.IsNullOrEmpty(appUser.Email) ? appUser.Email : "N/A",
                    RoleId = dbRole.Id
                };

                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        //GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string name, string badgeNo, string role)
        {
            if (id == null)
            {
                return Json("Not found");
                // return NotFound();
            }

            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return Json("Not found");
                // return NotFound();
            }
            else
            {
                try
                {
                    var roleId = int.Parse(role);
                    appUser.RoleId = roleId;
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

        }
        //public async Task<JsonResult> Edit(string id)
        //{
        //        string returnLink = "Users/Index/";
        //        return Json(returnLink);
        //}

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost] //, ActionName("Delete")
        //[ValidateAntiForgeryToken] // don't use
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser != null)
            {
                _context.AppUsers.Remove(appUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> CheckExistence(string badgeNo)
        {
            var dbUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.BadgeNo == badgeNo);
            if (dbUser != null)
            {
                return Json("Exists");
            }
            else
            {
                return Json("");

            }
        }
        [HttpPost]
        public async Task<IActionResult> SearchAppUsers(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.Replace(" ", "");
                var dbUsers = await _context.AppUsers.Include(x => x.Role).Where(x => x.BadgeNo.Contains(searchText) || x.Name.Contains(searchText))
                    .ToListAsync();
                if (dbUsers == null || dbUsers.Count() == 0)
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
            var dbUser = await _context.Employees.FromSqlRaw("SELECT * FROM All_employees").FirstOrDefaultAsync(x=>x.EmployeeID == searchText.Replace(" ", ""));
            if(dbUser == null)
            {
                return Json("");
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dbUser);
            return Json(json);
        }

    }
}

