using Infrastructure;
using Application.AppStart;
using Core.Mapping;
using AboutMe.Web.Middlewares;
using AboutMe.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureApp(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
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

