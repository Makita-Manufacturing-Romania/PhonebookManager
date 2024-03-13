using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;
using PhonebookManager.Models;

namespace PhonebookManager.Controllers
{
    public class AppClaimsController : Controller
    {
        private readonly DataContext _context;

        public AppClaimsController(DataContext context)
        {
            _context = context;
        }

        // GET: AppClaims
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppClaims.ToListAsync());
        }

        // GET: AppClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appClaim = await _context.AppClaims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appClaim == null)
            {
                return NotFound();
            }

            return View(appClaim);
        }

        // GET: AppClaims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Claim")] AppClaim appClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appClaim);
        }

        // GET: AppClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appClaim = await _context.AppClaims.FindAsync(id);
            if (appClaim == null)
            {
                return NotFound();
            }
            return View(appClaim);
        }

        // POST: AppClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Claim")] AppClaim appClaim)
        {
            if (id != appClaim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppClaimExists(appClaim.Id))
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
            return View(appClaim);
        }

        // GET: AppClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appClaim = await _context.AppClaims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appClaim == null)
            {
                return NotFound();
            }

            return View(appClaim);
        }

        // POST: AppClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appClaim = await _context.AppClaims.FindAsync(id);
            if (appClaim != null)
            {
                _context.AppClaims.Remove(appClaim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppClaimExists(int id)
        {
            return _context.AppClaims.Any(e => e.Id == id);
        }
    }
}
