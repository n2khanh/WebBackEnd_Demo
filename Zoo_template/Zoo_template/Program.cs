using Microsoft.EntityFrameworkCore;
using Zoo_template.Interfaces;
using Zoo_template.Models;
using Zoo_template.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZooContext")));

builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
