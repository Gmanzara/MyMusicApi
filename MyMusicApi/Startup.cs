using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MyMusicApi.Core;
using MyMusicApi.Core.Repositories;
using MyMusicApi.Core.Repository;
using MyMusicApi.Core.Services;
using MyMusicApi.Data;
using MyMusicApi.Data.MongoDb.Repository;
using MyMusicApi.Data.MongoDb.Setting;
using MyMusicApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApi
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
            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            //Configuration SQLserver*
            services.AddDbContext<MyMusicDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //Pour chaque requete on lui associe une instance de UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Configuration MongoDB
            services.Configure<Settings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetValue<string>("MongoDB:ConnectionString");
                    options.Database = Configuration.GetValue<string>("MongoDB:DataBase");
                });
            services.AddSingleton<IMongoClient, MongoClient>(_=> new MongoClient(Configuration.GetValue<string>("MongoDB:ConnectionString")));
            services.AddScoped<IComposerRepository, ComposerRepository>();
            services.AddTransient<IDatabaseSettings,DatabaseSettings>();

            //Service of application
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<IComposerService, ComposerService>();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "Put title Here", Description = "DotNet Core Api 3 - with swagger" });
            });

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/Swagger/v1/swagger.json", "My Music V1");
            });
        }
    }
}
