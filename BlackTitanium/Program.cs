using System.Text.Json;
using BlackTitanium.Models;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using React.AspNet;

namespace BlackTitanium;

internal static class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices();

        var app = builder.Build();
        app.MapGet("/", () => "Hello World!");

        app.ConfigureApp();

        app.Run();
    }

    public static void ConfigureServices(this IServiceCollection services) {
        services.AddDbContext<DatabaseContext>();
        services.AddControllers().AddNewtonsoftJson(x => {
            x.SerializerSettings.Formatting = Formatting.None;
            x.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        services.AddAuthorization();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // React
        services.AddMemoryCache();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddReact();
        services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
            .AddChakraCore();
    }

    public static void ConfigureApp(this WebApplication app) {
        if (app.Environment.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.MapControllers();

        app.UseAuthentication();
        app.UseAuthorization();

        // TODO LOW: Свой формат ошибок
        app.UseExceptionHandler(a => a.Run(async context => {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            await context.Response.WriteAsJsonAsync(new Error() {
                Message = exception?.Message ?? ""
            });
        }));
        
        // React
        app.UseReact(config => { });
        app.UseDefaultFiles();
        app.UseStaticFiles();
    }
}