using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;
using PhonebookManager.Models;
using PhonebookManager.ViewModels;
using static PhonebookManager.ViewModels.RoleAndUserViewModel;

namespace PhonebookManager
{
    public class AppUsersController : Controller
    {
        private readonly DataContext _context;
        public AppUsersController(DataContext context)
        {
            _context = context;
        }


        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            //var appRoles = await _context.AppRoles.ToListAsync();
            //var appUsers = await _context.AppUsers.ToListAsync();
            //var viewModel = new Tuple<List<AppRole>, List<AppUser>>(appRoles, appUsers);

            //return View(viewModel);

            return View(await _context.AppUsers.Include(x=>x.Role).ToListAsync());
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUserVM appUser) // [Bind("Id,AdIdentity,Email,BadgeNo,Name,RoleName")]
        {
            var dbRole = _context.AppRoles.FirstOrDefault(x => x.Role == appUser.RoleName);
            //ModelState.Remove("UserRoles");

            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    Name = appUser.Name,
                    Email = appUser.Email,
                    AdIdentity = appUser.AdIdentity,
                    BadgeNo = appUser.BadgeNo,
                    RoleId = dbRole.Id
                };


                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers.FindAsync(id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdIdentity,Email,BadgeNo,Name")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(appUser);
        }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
    }
}
