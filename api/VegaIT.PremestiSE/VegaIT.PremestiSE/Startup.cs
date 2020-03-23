using Core.Clients;
using Core.Interfaces.Intefaces;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces.Contracts;
using Persistence.Repositories;
using System.IO;

namespace VegaIT.PremestiSE
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(ExceptionHandler));
            });

            services.AddScoped<IKindergardenService, KindergardenService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IRequestService, RequestService>();

            services.AddScoped<IKindergardenRepository, KindergardenRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IPendingRequestRepository, PendingRequestRepository>();
            services.AddScoped<IMatchedRequestRepository, MatchedRequestRepository>();

            services.AddScoped<ISmtpClientFactory, SmtpClientFactory>();
            services.AddScoped<ISmtpClientWrapper, SmtpClientWrapper>();

            services.AddScoped<IMailClient, MailClient>();
            services.AddScoped<IKindergardenRepository, KindergardenRepository>();
            services.AddSingleton<SmtpClientFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
            
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
            
        }
    }
}
