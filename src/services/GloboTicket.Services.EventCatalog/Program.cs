using GloboTicket.Services.EventCatalog.DbContexts;
using GloboTicket.Services.EventCatalog.Repositories;
using GloboTicket.Services.EventCatalog.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.WebHost.ConfigureKestrel(options => {
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5001, o => o.Protocols =
                                HttpProtocols.Http2);
});

services.AddDbContext<EventCatalogDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
});
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IEventRepository, EventRepository>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddControllers();
services.AddGrpc();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<EventGrpcService>();
app.Run();