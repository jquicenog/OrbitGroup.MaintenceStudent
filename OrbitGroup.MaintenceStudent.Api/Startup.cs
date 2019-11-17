using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrbitGroup.MaintenceStudent.Core.Contracts;
using OrbitGroup.MaintenceStudent.Core.Services;
using OrbitGroup.MaintenceStudent.Dal;

namespace OrbitGroup.MaintenceStudent.Api
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
            var connection = Configuration["SqliteConnectionString"];
            services.AddScoped<IStudentRepository, StudentDBRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddDbContext<StudentContext>(options =>
                options.UseSqlite(connection)
            );

            services.AddCors(options => options.AddPolicy("StudentsCorsPolicy", builder =>
                  {
                      builder.WithOrigins(Configuration["AllowDomains"])
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
                  }
            ));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Control Err_Invalid_Response to workaround
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                       
            app.UseCors("StudentsCorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
