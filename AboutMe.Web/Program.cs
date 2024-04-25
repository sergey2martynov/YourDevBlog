using Infrastructure;
using Application.Extensions;
using Core.Mapping;
using AboutMe.Web.Middlewares;
using AboutMe.Web.Extensions;
using Application.Dtos.Options;


var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureApp(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("secret.json", optional: false, reloadOnChange: true);
    var config = configBuilder.Build();
    services.AddSingleton(config);
    services.Configure<S3Options>(config.GetSection("S3Options"));

    services.AddRazorPages();
    services.ConfigureDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
    services.ConfigureIdentity();
    services.AddServices();
    services.AddAutoMapper(typeof(AutoMapperProfile));
}

void ConfigureApp(WebApplication app)
{
    app.SetupDataBase();

    if (!app.Environment.IsDevelopment())
    {
        ConfigureProductionEnvironment(app);
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}

void ConfigureProductionEnvironment(WebApplication app)
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

