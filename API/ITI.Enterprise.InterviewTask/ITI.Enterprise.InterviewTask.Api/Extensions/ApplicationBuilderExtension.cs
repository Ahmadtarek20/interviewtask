using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITI.Enterprise.InterviewTask.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void EnsureDbCreation<T>(this IApplicationBuilder app) where  T: DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
            }
        }
    }
}
