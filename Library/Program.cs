using Library.Data;
using Library.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DapperDbConnext>();
builder.Services.AddScoped<IAppUserService, AppUserServiceImp>();
var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
