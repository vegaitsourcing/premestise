using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Intefaces;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces.Contracts;
using Persistence.Repositories;

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
            services.AddMvc();
            

            services.AddScoped<IKindergardenService, KindergardenService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IRequestService, RequestService>();


            services.AddScoped<IKindergardenRepository, KindergardenRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IPendingRequestRepository, PendingRequestRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
