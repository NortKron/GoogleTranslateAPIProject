using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using TranslateWebApp.Data;
using TranslateWebApp.Interfaces;
using TranslateWebApp.Services;

namespace TranslateWebApp
{
    public class Startup
    {
        public const string CFG_DATA_DB = @"ConnectionStrings:DefaultConnection";
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddGrpc();

            services.AddSwaggerGen(options =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "TranslateServiceAPI_Docs.xml");

                options.IncludeXmlComments(xmlPath);
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Transaltion service API",
                    Description = "Example ASP.NET Core API",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });

            services.AddTransient<ITranslationService, TranslationService>();
            services.AddTransient<IExternalTranslationApi, ExternalTranslationApi>();
            services.AddTransient<ITranslationCache, DataContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                DbInitializer.Initialize(context);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<TranslationGRPCServiceImpl>();
            });
        }
    }
}
