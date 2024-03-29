﻿using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PhonebookManager.Data;
using PhonebookManager.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PhonebookManager.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly DataContext _context;

        public TestController(ILogger<TestController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;

        }
        // [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeText()
        {
            // ViewModel vm = new ViewModel();
            //Employee employee = new Employee();
            string test = "qqqqq";
            return Json(test);
        }
        public class SearchViewModel
        {
            [DisplayName("Search Query")]
            [Required]
            public string? Query { get; set; }
        }



        //public async Task<Exception> SendNotification(string email, string subject, string body)
        //{
        //    SqlParameter parameter1 = new SqlParameter("@subjectFromCSharp", subject);
        //    SqlParameter parameter2 = new SqlParameter("@bodyFromCSharp", body);
        //    SqlParameter parameter3 = new SqlParameter("@recipientsFromCSharp", email);
        //    Exception ex = null;
        //    try
        //    {
        //        var result = await _context.Database.SqlQueryRaw<string>("exec SendMailProcedure @subjectFromCSharp,@bodyFromCSharp,@recipientsFromCSharp", parameter1, parameter2, parameter3).ToListAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        ex = e;
        //    }
        //    return ex!;
        //}

        //// Get nuget package Mailkit
        ////using System.Net;
        ////using System.Net.Mail;
        //public async Task SendTestNotification(string email, string subject, string body)
        //{
        //    //List<string> userEmails = new();
        //    //userEmails.Add(email);
        //    //int testPort = 587;
        //    int port = _config.GetValue<int>("SmtpConfig:Port");
        //    string server = _config.GetValue<string>("SmtpConfig:Server");
        //    string username = _config.GetValue<string>("SmtpConfig:Username");
        //    string password = _config.GetValue<string>("SmtpConfig:Password");
        //    string configEmail = _config.GetValue<string>("SmtpConfig:Email");

        //    SmtpClient smtp = new SmtpClient(server)
        //    {
        //        Port = port,
        //        Credentials = new NetworkCredential(username, password)
        //    };

        //    MailMessage mail = new MailMessage()
        //    {
        //        Subject = subject,
        //        IsBodyHtml = true,
        //        From = new MailAddress(configEmail),
        //        Body = body,
        //        Priority = MailPriority.High

        //    };

        //    //if (email.Attachments != null && email.Attachments.Count > 0)
        //    //{
        //    //    foreach (var attachment in email.Attachments)
        //    //    {
        //    //        using (var ms = new MemoryStream())
        //    //        {
        //    //            attachment.CopyTo(ms);
        //    //            var fileBytes = ms.ToArray();
        //    //            Attachment emailAttachment = new Attachment(new MemoryStream(fileBytes), attachment.FileName);
        //    //            mail.Attachments.Add(emailAttachment);
        //    //        }
        //    //    }
        //    //}

        //    //foreach (var email in userEmails)
        //    //{
        //    //    mail.To.Add(email);
        //    //}

        //    mail.To.Add(email);
        //    smtp.Send(mail);

        //    return;
        //}





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
