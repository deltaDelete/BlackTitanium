namespace BlackTitanium; 

internal class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);

        var app = builder.Build();
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<DatabaseContext>();
    }
}