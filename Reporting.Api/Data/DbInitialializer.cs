using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Reporting.Api.Data
{
    public class DbInitialializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ReportDbContext>();
            context.Database.Migrate();
        }
    }
}
