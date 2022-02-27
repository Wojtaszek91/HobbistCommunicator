using DAL.DataContext;
using DAL.Repositories;
using DAL.Repositories.IRepositories;
using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HobbistCommunicator.CommunicationHub;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace HobbistCommunicator
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

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptions.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<UserMessageRepository>(services => new UserMessageRepository(dbContextOptions.Options));
            //services.AddSingleton<IUserMessageRepository, UserMessageRepository>();
            services.AddSingleton<IMessageService, MessageService.MessageService>();
            services.AddSingleton<HobbistHub>();

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HobbistCommunicator", Version = "v1" });
            });

            var appSettingsClass = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsClass.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.Configure<AppSettings>(appSettingsClass);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hobbistHub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(a =>
            {
                a.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HobbistCommunicator v1"));
            }

            app.UseWebSockets();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HobbistHub>("/hobbistHub");
            });
        }
    }
}
