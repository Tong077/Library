using CurrieTechnologies.Razor.SweetAlert2;
using Library.Areas.Account.Data;
using Library.Data;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DapperDbConnext>();
builder.Services.AddTransient<DapperDbConnext>();

builder.Services.AddScoped<DapperdbContext>();

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

builder.Services.AddSweetAlert2();
builder.Services.AddToaster();
var app = builder.Build();

app.UseStaticFiles();



app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapAreaControllerRoute(
    name: "areas",
    areaName:"Account",
    pattern:"{controller=Account}/{action=Login}"
    );
app.Run();