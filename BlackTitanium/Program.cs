using System.Text.Json;
using BlackTitanium.Models;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlackTitanium;

internal class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);

        var app = builder.Build();
        app.MapGet("/", () => "Hello World!");

        ConfigureApp(app);

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<DatabaseContext>();
        services.AddControllers().AddNewtonsoftJson(x => {
            x.SerializerSettings.Formatting = Formatting.None;
            x.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });
    }

    public static void ConfigureApp(WebApplication app) {
// #if DEBUG
        // app.UseDeveloperExceptionPage();
// #endif

        app.UseRouting();
        app.MapControllers();
        
        // TODO LOW: Свой формат ошибок
        app.UseExceptionHandler(a => a.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            await context.Response.WriteAsJsonAsync(new Error() {
                Message = exception?.Message ?? ""
            });
        }));
    }
}