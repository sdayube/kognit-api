using Kognit.API.Application;
using Kognit.API.Infrastructure.Persistence;
using Kognit.API.Infrastructure.Persistence.Contexts;
using Kognit.API.Infrastructure.Shared;
using Kognit.API.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);

    Log.Information("Application Startup - Registering services...");

    builder.Services.AddApplicationLayer();
    builder.Services.AddPersistenceInfrastructure(builder.Configuration);
    builder.Services.AddSharedInfrastructure(builder.Configuration);
    builder.Services.AddSwaggerExtension();
    builder.Services.AddControllersExtension();
    builder.Services.AddCorsExtension();
    builder.Services.AddRoutingExtension();
    builder.Services.AddHealthChecks();
    builder.Services.AddApiVersioningExtension();
    builder.Services.AddMvcCore().AddApiExplorer();
    builder.Services.AddVersionedApiExplorerExtension();

    var app = builder.Build();

    Log.Information("Application Startup - Registering middleware...");

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseCors("AllowAll");

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();
        app.UseCors("AllowSpecificOrigin");
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseSwaggerExtension();
    app.UseErrorHandlingMiddleware();
    app.UseHealthChecks("/health");
    app.MapControllers();

    Log.Information("Application Starting");

    app.Run();
}

catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
}

finally
{
    Log.CloseAndFlush();
}
