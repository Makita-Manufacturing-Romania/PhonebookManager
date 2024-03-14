using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PhonebookManager.Data;
using PhonebookManager.Models;
using System.Diagnostics;

namespace PhonebookManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DataContext _context;
        private readonly IJSRuntime _js;
        public DashboardController(DataContext context, IJSRuntime js)
        {
            _context = context;
            _js = js;

        }
        // GET: DashboardController
        public async Task<IActionResult> Index(string searchText)
        {
            ViewBag.SearchText = searchText;
            List<PhoneLine> dbPhoneLines = new List<PhoneLine>();

            if (!string.IsNullOrEmpty(searchText) && searchText.Length == 10)
            {
                dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes)
                    .Where(x => x.PhoneNumber == searchText).ToListAsync();
            }
            else
            {
                dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes).ToListAsync();

            }
            return View(dbPhoneLines);

        }

        // GET: DashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<JsonResult> ModalDelete(string id/*, string name*/)
        {
            string returnLink = "";
            try
            {

                returnLink = "Dashboard/Index/";
            }
            catch (Exception)
            {

                returnLink = "invalidData";
                throw;
            }

            return Json(returnLink);
        }

        [HttpPost]
        public async Task<JsonResult> SearchPhoneLine(string searchText)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    string[] words = searchText.Split(' ');
                    string firstWord = words[0];
                    var dbPhoneLines = await _context.PhoneLines.ToListAsync();
                    // firstWord.GetType() == typeof(int) or int.TryParse(firstWord, out int value)

                    if (long.TryParse(firstWord, out long longValue) && firstWord.Length == 10)
                    {
                        dbPhoneLines = dbPhoneLines.Where(p => p.LineOwner == null ? p.PhoneNumber.Contains(firstWord) : p.LineOwner.Badge.Contains(firstWord) || p.LineOwner.Name.Contains(firstWord)
                         || p.PhoneNumber.Contains(firstWord)).Take(10).ToList();
                        var dbPhoneLinesFiltered = (from line in dbPhoneLines
                                                    select new
                                                    {
                                                        label = line.PhoneNumber,
                                                        val = line.Id
                                                    }).ToList();

                        if (dbPhoneLinesFiltered.Count != 0)
                        {
                            return Json(dbPhoneLinesFiltered);

                        }
                        else
                        {
                            ViewBag.PhoneNumber = firstWord;
                            return Json("Not found");
                        }

                    }

                    if (int.TryParse(firstWord, out int intValue))
                    {
                        dbPhoneLines = dbPhoneLines.Where(p => p.PhoneNumber.Contains(firstWord)).Take(10).ToList();

                        var dbPhoneLinesFiltered = (from line in dbPhoneLines
                                                    select new
                                                    {
                                                        label = line.PhoneNumber,
                                                        val = line.Id
                                                    }).ToList();


                        if (dbPhoneLinesFiltered.Count != 0)
                        {
                            return Json(dbPhoneLinesFiltered);

                        }
                        else
                        {
                            return Json("");
                        }
                    }

                    if (firstWord.Length >= 3)
                    {
                        dbPhoneLines = dbPhoneLines.Where(p => p.LineOwner == null ? p.PhoneNumber.Contains(firstWord) : p.LineOwner.Badge.Contains(firstWord) || p.LineOwner.Name.Contains(firstWord)
                         || p.PhoneNumber.Contains(firstWord)).Take(10).ToList();

                        var dbPhoneLinesFiltered = (from line in dbPhoneLines
                                                    select new
                                                    {
                                                        label = line.PhoneNumber,
                                                        val = line.Id
                                                    }).ToList();
                        if (dbPhoneLinesFiltered.Count != 0)
                        {
                            return Json(dbPhoneLinesFiltered);

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
                await _js.InvokeAsync<string>("console.log", ex.Message, " Search Function");
                return Json("Error-" + ex.Message + " stackTrace-" + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddQuickPhoneLine(string phoneLine)
        {
            try
            {
                var dbPhoneLine = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber == phoneLine);
                if (dbPhoneLine == null && !string.IsNullOrEmpty(phoneLine))
                {
                    _context.PhoneLines.Add(new PhoneLine { PhoneNumber = phoneLine });
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                await _js.InvokeAsync<string>("console.log", ex.Message, " AddQuickPhoneLine");
            }


            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
