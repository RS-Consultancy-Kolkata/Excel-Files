using ClosedXML.Excel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using Rssolar_api.Services;
using Rssolar_api.Services.Interfaces;
using Rssolar_api.Services.Inverter;
using Rssolar_api.Services.Mfm;
using v24.OpenApi.Models.ApiV2;

var builder = WebApplication.CreateBuilder(args);
// Add this to Program.cs (for .NET 6+) or Startup.cs
// Add services
builder.Services.AddControllers();
// Remove this using directive:
// using Microsoft.OpenApi;

// Keep this using directive:
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(static c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RS Solar API",
        Version = "v1"
    });
});
// Register Services
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IPowerDataService, PowerDataService>();
builder.Services.AddScoped<ISolarDataService, SolarDataService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IWeatherDataService, WeatherDataService>();
builder.Services.AddScoped<IUserConfigService, UserConfigService>();
builder.Services.AddScoped<ISMBService, SMBService>();
builder.Services.AddScoped<IMfmReportService, MfmReportService>();
builder.Services.AddScoped<IInverterDataService, InverterDataService>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RS Solar API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
