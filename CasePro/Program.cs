using Domaincasepro.Commands;
using Domaincasepro.Queries;
using Domaincasepro.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injections
var conString = builder.Configuration.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;
builder.Services.AddDbContext<Modelcasepro.Entities.CaseproDbContext>(options =>
                options.UseSqlServer(conString));
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddTransient<IActivitydetailsRepository, ActivitydetailsRepository>();
builder.Services.AddTransient<ITrailerTippingRepository, TrailerTippingRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IResourseRepository, ResourseRepository>();
builder.Services.AddScoped<IActivitySignOffRepository, ActivitySignOffRepository>();
builder.Services.AddScoped<IInstructOperationRepository, InstructOperationRepository>();
builder.Services.AddScoped<IInstructOperationRepository, InstructOperationRepository>();

builder.Services.AddScoped<ActivityQueryHandler>();
builder.Services.AddScoped<ActivitydetailsQueryHandler>();
builder.Services.AddScoped<SiteInstallationCommandHandler>();
builder.Services.AddScoped<ActivityCommandHandler>();
builder.Services.AddScoped<CustomerCommadHandler>();
builder.Services.AddScoped<TrailerTippingCommandHandler>();
builder.Services.AddScoped<TrailerTippingQueryHandler>();
builder.Services.AddScoped<LoginQueryHandler>();
builder.Services.AddScoped<LoginCommandHandler>();
builder.Services.AddScoped<ProductQueryHandler>();
builder.Services.AddScoped<ProductDataCommandHandler>();
builder.Services.AddScoped<ResourceQueryHandler>();
builder.Services.AddScoped<ResourceDataCommandHandler>();
builder.Services.AddScoped<NotesCommandHandler>();
builder.Services.AddScoped<NotesQueryHandler>();
builder.Services.AddScoped<SignoffCommadnHandler>();
builder.Services.AddScoped<InstructorDataCommandHandler>();
builder.Services.AddScoped<InstructQueryHandler>();
builder.Services.AddScoped<DeleteActivityCommandHandler>();
builder.Services.AddScoped<ActivitySummaryQueryhandler>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(x => x.LoginPath = "/");


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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
