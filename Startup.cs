using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;
using OneStopApp.Service;
using OneStopApp_Api.EntityFramework.Data;
using OneStopApp_Api.Interface;
using OneStopApp_Api.Service;

namespace OneStopApp_api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                path: $"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<OsaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OspConnection")));
            // Add framework services.
            services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("http://localhost:4200");
                                  builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                              });
        });

            // services.AddResponseCaching();
            services.AddHttpClient<IWeatherService, WeatherService>();
            services.AddControllers();
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddRazorPages();
            services.AddDataProtection();
            services.AddTransient<ITechnologyService, TechnologyService>();
            services.AddTransient<ISaltPasswordService, SaltPasswordService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IWeatherService, WeatherService>();
             services.AddTransient<IUserService, UserService>();
              services.AddTransient<IEmailDomainService, EmailDomainService>();
            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           });
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
            app.UseMvc();
        }
    }
}
