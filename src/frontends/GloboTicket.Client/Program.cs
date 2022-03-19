using GloboTicket.Client.Models;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
    mvcBuilder.AddRazorRuntimeCompilation();

builder.Services.AddSingleton<Settings>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
"default",
"{controller=Home}/{action=Index}/{id?}");
app.Run();