using CryptoQuotes.Application;
using CryptoQuotes.Infrastructure;
using CryptoQuotes.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services
    .RegisterApplication(builder.Configuration)
    .RegisterInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) => config
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(context.Configuration));

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