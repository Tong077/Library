using Library.Data;
using Library.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DapperDbConnext>();
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

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
