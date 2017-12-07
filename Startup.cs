using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using windforce_corp.Data;

namespace windforce_corp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("WindForce"));

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ApplicationDbContext datacontext
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseResponseCompression();
            app.UseMvc();
            app.Use(async (context, next) =>
            {   
                if(context.Request.Path == "/api") 
                {
                    await context.Response.WriteAsync("Hello MCT Africa Summit");
                } 
                else 
                {
                    await next();
                }
            });
            DbInitializer.Initialize(datacontext);
        }
    }
}
