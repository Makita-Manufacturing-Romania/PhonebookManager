using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using PhonebookManager.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();
builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

// This is used to get the images from server and convert them to base64 for the pdf
var handler = new HttpClientHandler()
{
    UseDefaultCredentials = false,
    Credentials = System.Net.CredentialCache.DefaultCredentials,
    AllowAutoRedirect = true
};

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetService<IConfiguration>();
    var baseAddress = configuration["BaseAddress"] ?? "http://localhost:5118/";

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(baseAddress)
    };
});

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools())); // DinkToPdf


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Dashboard/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.UseStatusCodePagesWithRedirects("/Error/NotFound"); // Important, redirect to 404

app.Run();
