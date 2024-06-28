using CurrieTechnologies.Razor.SweetAlert2;
using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DapperDbConnext>();
builder.Services.AddTransient<DapperDbConnext>();

var connectionString = builder.Configuration.GetConnectionString("MyConnection");



builder.Services.AddScoped<IAppUserService, AppUserServiceImp>();
builder.Services.AddScoped<IAppUserpermissionService, AppUserpermissionServiceImp>();
builder.Services.AddScoped<IBookService, BookServiceImp>();
builder.Services.AddScoped<IBorrowService, BorrowServiceImp>();
builder.Services.AddScoped<ICatalogService, CatalogServiceImp>();
builder.Services.AddScoped<ICustomerService, CustomerServiceImp>();
builder.Services.AddScoped<ICustomerTypeService, CustomerTypeServiceImp>();
builder.Services.AddScoped<ILibrarianService, LibrarianServiceImp>();
builder.Services.AddScoped<IBorrowDetailService, BorrowDetailServiceImp>();
builder.Services.AddScoped<IAccountService, AccountServiceImp>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
builder.Services.AddSingleton<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.LogoutPath = "/Account/Logout";
        // Configure other options as needed
    });
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});



var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Login",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.MapDefaultControllerRoute();
app.Run();