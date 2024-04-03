using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AboutMe.Web.Extensions
{
    public static class DataBaseStartUpExtension
    {
        public static void SetupDataBase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating/migrating or seeding the database.");

                    throw;
                }
            }
        }
    }
}
