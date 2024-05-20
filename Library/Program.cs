using CurrieTechnologies.Razor.SweetAlert2;

using Library.Data;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DapperDbConnext>();
builder.Services.AddTransient<DapperDbConnext>();

var connectionString = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<EntityContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped<IAppUserService, AppUserServiceImp>();
builder.Services.AddScoped<IAppUserpermissionService, AppUserpermissionServiceImp>();
builder.Services.AddScoped<IBookService, BookServiceImp>();
builder.Services.AddScoped<IBorrowService, BorrowServiceImp>();
builder.Services.AddScoped<ICatalogService, CatalogServiceImp>();
builder.Services.AddScoped<ICustomerService, CustomerServiceImp>();
builder.Services.AddScoped<ICustomerTypeService, CustomerTypeServiceImp>();
builder.Services.AddScoped<ILibrarianService, LibrarianServiceImp>();
builder.Services.AddScoped<IBorrowDetailService, BorrowDetailServiceImp>();



var app = builder.Build();




app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "Login",
//    pattern: "{controller=Account}/{action=Login}/{id?}");
app.MapDefaultControllerRoute();
app.Run();