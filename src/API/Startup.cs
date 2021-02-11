using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace RESTAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services){

            services.AddDbContext<AppDbContext>(options => {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(
					Configuration.GetConnectionString("DBConnection"),
                    innerOptions => {
                        innerOptions.MigrationsAssembly("CW.RESTAPI");
                        innerOptions.SetPostgresVersion(new Version(12, 10));
                    });
            }, ServiceLifetime.Transient);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //services.AddAuthorization(options => {
            //    options.AddPolicy("RequireLoggedId", policy => policy.RequireRole("Administrators", "Moderators").RequireAuthenticatedUser());
            //    options.AddPolicy("RequireAdministrator", policy => policy.RequireRole("Administrators").RequireAuthenticatedUser());
            //});

            services.Configure<IdentityOptions>(options => {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}