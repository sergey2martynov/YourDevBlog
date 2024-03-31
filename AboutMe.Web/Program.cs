using AboutMe.HelperExtensions;
using Infrastructure;
using Application.AppStart;
using Core.Mapping;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddContext(builder.Configuration.GetConnectionString("DefaultConnection"));
    services.AddServices();
    services.AddAutoMapper(typeof(AutoMapperProfile));
}

void Configure(WebApplication app)
{
    app.SetupDataBase();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

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
