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

            if (!string.IsNullOrEmpty(searchText)) // && searchText.Length == 10
            {
                if (long.TryParse(searchText, out long longValue) && searchText.Length > 3)
                {
                    dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes)
                                        .Where(x => x.PhoneNumber.Contains(searchText)).ToListAsync();
                }
                else if (searchText.Length > 3)
                {
                    string[] words = searchText.Split(' ');
                    string firstWord = words[0];
                    dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes)
                    .Where(x => x.LineOwner.Name.Contains(firstWord)
                    || x.LineOwner.Badge.Contains(firstWord)).ToListAsync();
                }
                else
                {
                    dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes).ToListAsync();
                }
                    
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
        public async Task<JsonResult> AutocompleteSearchPhoneLine(string searchText)
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
        public async Task<IActionResult> SearchPhoneLine(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                if (long.TryParse(searchText, out long longValue) && searchText.Length > 3)
                {
                    searchText = searchText.Replace(" ", "");
                    var dbPhoneNumber = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber.Contains(searchText));
                    if (dbPhoneNumber == null)
                    {
                        return Json("No phone line found");

                    }
                    else
                    {
                        return Json("");
                    }
                }
                else if(searchText.Length > 3)
                {
                    string[] words = searchText.Split(' ');
                    string firstWord = words[0];
                    var dbPhoneLine = await _context.PhoneLines.Where(x => x.LineOwner.Name.Contains(firstWord)
                    || x.LineOwner.Badge.Contains(firstWord)).FirstOrDefaultAsync();

                    if (dbPhoneLine == null)
                    {
                        return Json("Not found");

                    }
                    else
                    {
                        return Json("");
                    }

                }
                else
                {
                    return Json("Not found");
                }


            }
            return Json("");

        }

        [HttpPost]
        public async Task<IActionResult> AddQuickPhoneLine(string phoneLine)
        {
            try
            {
                var dbPhoneLine = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber == phoneLine);
                if (dbPhoneLine == null && !string.IsNullOrEmpty(phoneLine)) // && phoneLine.Length == 10
                {
                    _context.PhoneLines.Add(new PhoneLine { PhoneNumber = phoneLine });
                    await _context.SaveChangesAsync();
                }
                else if (dbPhoneLine is not null)
                {
                    return Json("Exists");
                }
                else
                {
                    return Json("Is null");
                }

            }
            catch (Exception ex)
            {

                await _js.InvokeAsync<string>("console.log", ex.Message, " AddQuickPhoneLine");
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CheckPhoneNumber(string phoneNumber)
        {
            var dbPhoneNumber = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (dbPhoneNumber is not null)
            {
                return Json("Exists");
            }
            return Json("");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
