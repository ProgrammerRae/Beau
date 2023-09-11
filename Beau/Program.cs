using Beau.Data;
using Beau.Models;
using Beau.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<UserRepository>();


builder.Services.AddDbContext<DataBContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddLogging(builder => builder.AddConsole()); 
builder.Services.AddHttpClient();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(1000); // Set your desired session timeout
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LoginView}/{id?}");

app.MapControllerRoute(
        name: "dynamicroutes",
        pattern: "{controller}/{action}/{id?}");


app.Run();
