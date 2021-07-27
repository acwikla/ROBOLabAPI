using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ROBOLab.Core.Models;

namespace ROBOLab.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().Build());
                //.AllowCredentials().Build());
            });

            services.AddControllers();

            //services.AddDbContext<ROBOLabDbContext>(opt => opt.UseInMemoryDatabase("ROBOLabDB"));
            //services.AddDbContext<ROBOLabDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ROBOLabDB")));
            services.AddDbContext<ROBOLabDbContext>(opt => opt
                .UseSqlite(@"Data Source=ROBOLab.db")
                .EnableSensitiveDataLogging()           // log sql queries
                );

            //TODO: sciezke do sqlite umiescic w conn stringu

            // Do przegladania baz Sqlite mozna uzyc softu DBeaver (https://dbeaver.io)
            // Supports all popular databases: MySQL, PostgreSQL, SQLite, Oracle, DB2, SQL Server, Sybase, MS Access,
            // Teradata, Firebird, Apache Hive, Phoenix, Presto, etc.


            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "ROBOLabApi"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ROBOLabDbContext dbContext)
        {
            // run migrations on startup (database update)
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ROBOLabApi V1");
            });
        }
    }
}
