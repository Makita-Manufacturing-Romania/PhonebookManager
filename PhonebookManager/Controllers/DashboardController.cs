using AspNetCoreHero.ToastNotification.Abstractions;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;
using PhonebookManager.Models;
using System.Diagnostics;
using System.Globalization;
using static PhonebookManager.ViewModels.DashboardViewModel;

namespace PhonebookManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DataContext _context;
        public INotyfService _notifyService { get; }

        public DashboardController(DataContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;

        }

        // GET: DashboardController
        public async Task<IActionResult> Index(string searchText, int depId)
        {
            var dbPhoneLines = await _context.PhoneLines.Include(x => x.LineOwner).Include(y => y.Department).Include(z => z.LineUsers).Include(u => u.Changes).ToListAsync();
            var dbDepartments = await _context.Departments.Include(x => x.Lines).ToListAsync();
            ViewBag.DepartmentId = 0;

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                if (long.TryParse(searchText, out long longValue) && searchText.Length > 4) // && searchText.Length == 10
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

            if (depId != 0)
            {
                ViewBag.DepartmentId = depId;
                dbPhoneLines = await _context.PhoneLines.Where(x => x.Department.Id == depId).ToListAsync();
            }

            var viewModel = new DashboardDepartmentVM()
            {
                Lines = dbPhoneLines,
                DepartmentList = dbDepartments
            };
            return View(viewModel);

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
        public async Task<IActionResult> Edit(int? id, string name, string badgeNo, string depName, string depCode, string phonelineNo)
        {
            if (id == null)
            {
                return Json("Not found");
                // return NotFound();
            }

            var phoneLine = await _context.PhoneLines.FindAsync(id);
            if (phoneLine == null)
            {
                return Json("Not found");
                // return NotFound();
            }
            else
            {
                try
                {
                    phoneLine.PhoneNumber = phonelineNo;
                    phoneLine.LineOwner = new PhoneUser { Name = name, Badge = badgeNo };
                    phoneLine.Department = new Department { Name = depName, Code = depCode };

                    _context.Update(phoneLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneLine = await _context.PhoneLines.FindAsync(id);
            if (phoneLine != null)
            {
                _context.PhoneLines.Remove(phoneLine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> ModalDelete(string id/*, string name*/)
        //{
        //    return RedirectToAction(nameof(Index));
        //}

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
                return Json("Error-" + ex.Message + " stackTrace-" + ex.StackTrace);
            }
        }


        public async Task<IActionResult> SearchPhoneLine(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                if (long.TryParse(searchText, out long longValue) && searchText.Length > 4) // && searchText.Length == 10
                {
                    searchText = searchText.Replace(" ", "");
                    var dbPhoneNumber = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber.Contains(searchText));
                    if (dbPhoneNumber == null)
                    {
                        return Json("Not found");

                    }
                    else
                    {
                        return Json("");
                    }
                }
                else if (searchText.Length > 3)
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
        public async Task<IActionResult> CheckNumberToAllocate(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                if (long.TryParse(searchText, out long longValue) && searchText.Length > 4) // && searchText.Length == 10
                {
                    searchText = searchText.Replace(" ", "");
                    var dbPhoneNumber = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber.Contains(searchText));
                    if (dbPhoneNumber == null)
                    {
                        return Json("Not found");

                    }

                }
            }
            return Json("NaN");

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

            }

            return RedirectToAction(nameof(Index));

            // return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckPhoneNumber(string phoneNumber)
        {
            var dbPhoneNumber = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (dbPhoneNumber is not null)
            {
                return Json("Exists");
            }
            return Json("");

        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string[] allowedExtensions = { /*".pdf", ".doc", ".docx", */ ".csv" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        var maxFileSize = 5 * 1024 * 1024; // 5MB

                        if (file.Length > maxFileSize)
                        {
                            _notifyService.Warning("File size exceeds the maximum limit of 5MB.");
                            //return View();
                        }

                        // Save the file to the server here or perform any other operations
                        string phonenumber = "";
                        string ownerbadge = "";
                        string depcode = "";
                        PhoneLine phoneLine = new();

                        using (var reader = new StreamReader(file.OpenReadStream()))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<CSVFile>();
                            //phonenumber = records?.FirstOrDefault()?.PhoneNumber;
                            //ownerbadge = records?.FirstOrDefault()?.LineOwnerBadge;
                            //depcode = records?.FirstOrDefault()?.DepartmentCode;

                            foreach (var record in records)
                            {
                                phonenumber = record.PhoneNumber;
                                ownerbadge = record.LineOwnerBadge;
                                depcode = record.DepartmentCode;
                                //phonenumbers.Add(record.PhoneNumber);
                                //ownerbadges.Add(record.LineOwnerBadge);
                                //epcodes.Add(record.DepartmentCode);
                            }

                            var dbline = await _context.PhoneLines.FirstOrDefaultAsync(x => x.PhoneNumber.Contains(phonenumber));
                            var dbPhoneUsers = await _context.PhoneUsers.ToListAsync();
                            var dbDeps = await _context.Departments.ToListAsync();

                            if (dbline is null)
                            {
                                phoneLine = new PhoneLine
                                {
                                    PhoneNumber = phonenumber,
                                    LineOwner = !string.IsNullOrEmpty(ownerbadge) ? dbPhoneUsers.FirstOrDefault(x => x.Badge == ownerbadge) : null,
                                    Department = !string.IsNullOrEmpty(depcode) ? dbDeps.FirstOrDefault(x => x.Code == depcode) : null,
                                };

                                _context.PhoneLines.Add(phoneLine);
                                await _context.SaveChangesAsync();
                                _notifyService.Success("Phone line allocated: " + phonenumber);
                            }
                            else
                            {
                                var dbUser = dbPhoneUsers.FirstOrDefault(x => x.Badge == ownerbadge);
                                var dbDep = dbDeps.FirstOrDefault(x => x.Code == depcode);

                                if (dbUser is null && !string.IsNullOrEmpty(ownerbadge))
                                {
                                    _notifyService.Error("User does not exist");
                                }
                                else if (dbDep is null && !string.IsNullOrEmpty(depcode))
                                {
                                    _notifyService.Error("Department does not exist");
                                }
                                else
                                {
                                    dbline.LineOwner = !string.IsNullOrEmpty(ownerbadge) ? dbPhoneUsers.FirstOrDefault(x => x.Badge == ownerbadge) : null;
                                    dbline.Department = !string.IsNullOrEmpty(depcode) ? dbDeps.FirstOrDefault(x => x.Code == depcode) : null;

                                    _context.Update(dbline);
                                    await _context.SaveChangesAsync();
                                    _notifyService.Success("Phone line updated");

                                }

                            }

                        }


                        //_notifyService.Success("File uploaded successfully!" + file.FileName);
                        //_notifyService.Success(phonenumber + " " + ownerbadge + " " + depcode);
                    }
                    else
                    {
                        _notifyService.Warning("Only csv files are allowed.");
                    }
                }
                else
                {
                    _notifyService.Warning("Please select a file to upload.");
                }
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
            }


            //return new EmptyResult();
            return Json("");

        }

        //public async Task<IActionResult> FilterDepartmentsById(int id)
        //{


        //    return RedirectToAction(nameof(Index));

        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
