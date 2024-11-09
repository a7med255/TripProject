using Microsoft.EntityFrameworkCore;
using TripProject.Bl;
using TripProject.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TripsContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//to get connect sql in app.json
builder.Services.AddScoped<ICategories, ClsCategories>();
builder.Services.AddScoped<ITrips, ClsTrips>();
builder.Services.AddScoped<ICity, ClsCity>();
builder.Services.AddScoped<ICountry, ClsCountry>();
builder.Services.AddScoped<IRequests, ClsRequest>();




// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())   
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Trip}/{action=List}");

    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Trip}/{action=Search}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
