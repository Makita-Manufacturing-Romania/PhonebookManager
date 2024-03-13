using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;
using PhonebookManager.Models;

namespace PhonebookManager.Controllers
{
    public class AppUserController : Controller
    {
        private readonly DataContext _context;
        public AppUserController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var dbUsers = await _context.AppUsers.ToListAsync();
            return View(await _context.AppUsers.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser user) // Bind("EmployeeID, etc")] 
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee = await _context.AppUsers.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbUser == null)
            {
                return NotFound();
            }

            return View(dbUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dbUser = await _context.AppUsers.FindAsync(id);
            if (dbUser != null)
            {
                _context.AppUsers.Remove(dbUser);
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
