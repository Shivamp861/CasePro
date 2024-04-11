using Domaincasepro.Queries;
using Domaincasepro.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injections
var conString = builder.Configuration.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;
builder.Services.AddDbContext<Modelcasepro.Entities.CaseproDbContext>(options =>
                options.UseSqlServer(conString));
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<ActivityQueryHandler>();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
