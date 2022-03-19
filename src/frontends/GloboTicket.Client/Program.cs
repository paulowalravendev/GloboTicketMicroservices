using GloboTicket.Client.Models;
using GloboTicket.Client.Services;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
    mvcBuilder.AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<IEventCatalogService, EventCatalogService>(c => {
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiConfigs:EventCatalog:Uri"));
});

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
"{controller=EventCatalog}/{action=Index}/{id?}");
app.Run();