using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Dal.Repository.Interface;
using DotNetCoreWebAPI.Dal.Repository.StudentRepository;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Dal.UnitOfWork;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Services;
using DotNetCoreWebAPI.StudentData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWebAPI
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
            services.AddControllers();

            services.AddCors(options => {
                // options.AddDefaultPolicy(builder => builder.AllowAnyOrigin());
                // options.AddDefaultPolicy(builder => builder.WithOrigins("https://localhost:44380"));
                options.AddPolicy("mypolicy", builder => builder.WithOrigins("https://localhost:44380"));
            });

            services.AddDbContextPool<StudentDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StudentConnection")));

            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<ISubjectsRepository, SubjectsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork > ();

            services.AddScoped<Service> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
