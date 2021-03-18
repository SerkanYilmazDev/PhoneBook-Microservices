using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Reporting.Api.Commands;
using Reporting.Api.Commands.Handlers;
using Reporting.Api.Data;
using Reporting.Api.Events;
using Reporting.Api.HttpServices;
using Shared;
using Shared.RabbitMq;
using System;
using System.Net.Http.Headers;
using System.Reflection;


namespace Reporting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReportDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            var phoneBookHttpServiceUrl = Configuration.GetValue<string>("HttpServices:PhoneBookHttpServiceUrl");
            services.AddHttpClient<IPersonHttpService, PersonHttpService>(client =>
            {
                client.BaseAddress = new Uri(phoneBookHttpServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient<ILocationHttpService, LocationHttpService>(client =>
            {
                client.BaseAddress = new Uri(phoneBookHttpServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services.BuildContainer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRabbitMq()
              .SubscribeCommand<CreatePersonReportsByLocationCommand>()
              .SubscribeEvent<ReportCreated>();

            DbInitialializer.Initialize(app.ApplicationServices);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("", async context => await context.Response.WriteAsync("Report service is up."));
            });
           
        }
    }
}
