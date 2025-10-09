/* import the models from the models namespace
   import the EFCore library for database functionalities
*/
using Assignment1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Add services to the container.

/// <summary>
/// Register DbContext with the connection string from appsettings.json
/// </summary>
builder.Services.AddDbContext<ContactContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactContext"))
    );

/// <summary>
/// Configure routing options
/// </summary>
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

/// <summary>
/// Configure the HTTP request pipeline.
/// </summary>
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection(); // Make sure https instead of http is used
app.UseRouting(); // Allow routing to be used
app.UseAuthorization();
app.MapStaticAssets(); // Allow static objects (css files,html files, images, etc.) to be used
// Set the default route to the index page of the home controller
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}") // include id and slug in the url if they exist
    .WithStaticAssets();
app.Run();