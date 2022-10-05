using Microsoft.EntityFrameworkCore;
using Task_Tracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TaskTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Entry}/{action=Index}/{id?}");

app.Run();

