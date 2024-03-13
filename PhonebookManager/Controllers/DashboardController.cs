using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PhonebookManager.Data;
using PhonebookManager.Models;
using System.Diagnostics;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using System.Runtime.CompilerServices;

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
        public ActionResult Index()
        {
            return View();
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
                string[] words = searchText.Split(' ');
                string firstWord = words[0];
                var dbPhoneLines = await _context.PhoneLines.ToListAsync();
                // firstWord.GetType() == typeof(int) or int.TryParse(firstWord, out int value)

                if (long.TryParse(firstWord, out long value) && firstWord.Length == 10)
                {

                    dbPhoneLines = dbPhoneLines.Where(p => p.PhoneNumber.Contains(firstWord) || p.LineOwner.Badge.Contains(firstWord)
                    || p.LineOwner.Name.Contains(firstWord)).Take(10).ToList();

                    if(dbPhoneLines.Count != 0)
                    {
                        return Json(dbPhoneLines);

                    }
                    else
                    {
                        return Json("Not found");
                    }

                }
                else
                {
                    dbPhoneLines = dbPhoneLines.Where(p=> p.LineOwner.Name.Contains(firstWord) || p.LineOwner.Badge.Contains(firstWord)).Take(10).ToList();
                    if (dbPhoneLines.Count != 0)
                    {
                        return Json(dbPhoneLines);

                    }
                    else
                    {
                        return Json("");
                    }
                }
            }
            catch (Exception ex)
            {
                _js.InvokeAsync<string>("console.log", ex.Message, " Search Function");
                return Json("Error-" + ex.Message + " stackTrace-" + ex.StackTrace);
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
