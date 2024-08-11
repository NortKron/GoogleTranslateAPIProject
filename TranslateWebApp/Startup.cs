using Microsoft.EntityFrameworkCore;

using TranslateWebApp.Clients;
using TranslateWebApp.Data;
using TranslateWebApp.Interfaces;
using TranslateWebApp.Services;

namespace TranslateWebApp
{
    public class Startup
    {
        public const string CFG_DATA_DB = @"ConnectionStrings:DefaultConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            //services.AddGrpc();
            services.AddSwaggerGen();

            services.AddTransient<ITranslationService, TranslationService>();

            services.AddGrpcClient<GrpcTranslationClient>(options =>
            {
                options.Address = new Uri("https://localhost:5174/");
            });

            services.AddHttpClient<RestTranslationClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7212/");
            });

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
            });
        }
    }
}
