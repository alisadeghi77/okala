using CryptoQuotes.Application;
using CryptoQuotes.Core;
using CryptoQuotes.Infrastructure;
using CryptoQuotes.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(builder.Configuration)
    .Configure<CurrencySettings>(builder.Configuration.GetSection("CurrencySettings"));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) => config
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(context.Configuration));

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();