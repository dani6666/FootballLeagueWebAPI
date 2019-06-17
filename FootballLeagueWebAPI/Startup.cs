using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Services;

namespace FootballLeagueWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LeagueContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddTransient<MatchRepository>();
            services.AddTransient<TeamRepository>();
            services.AddTransient<PlayerRepositiory>();
            services.AddTransient<LeagueInputService>();
            services.AddTransient<LeagueOutputService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LeagueContext>();
                DataInitializer.Seed(context);
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
