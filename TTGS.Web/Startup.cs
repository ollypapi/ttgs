using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;
using TTGS.AzureServices.AzureStorage;
using TTGS.Core.Commands;
using TTGS.Core.Interfaces;
using TTGS.Core.Services;
using TTGS.Infrastructure.EF;
using TTGS.Infrastructure.EF.Data;
using TTGS.Infrastructure.UnitOfWork;
using TTGS.Shared.Entity;
using TTGS.Shared.Helper;
using TTGS.Shared.Settings;
using TTGS.Shared.Validator;
using TTGS.Web.Filters;

namespace TTGS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            configuration = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile($"appsettings.json", optional: false).Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddApiVersioning();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TTGS API",
                    Description = "TTGS API Description"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
                options.SchemaFilter<SwaggerExcludeFilter>(); 
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddMemoryCache();

            services.AddApplicationInsightsTelemetry();

            var dbConnection = Configuration.GetConnectionString("DbConnection");
            Log.Logger.Information($"Database information: {dbConnection}");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(dbConnection));
            services.AddDefaultIdentity<AspNetUsers>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.Password = new PasswordOptions
                {
                    RequireNonAlphanumeric = false,
                    RequiredUniqueChars = 0
                };
            })
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            var azureBlobConnection = Configuration.GetConnectionString("AzureBlobConnection");
            var azureBlobContainer = Configuration.GetValue<string>("AzureServices:AzureBlobContainer");

            services.AddDbContext<TTGSContext>(options => options.UseSqlServer(dbConnection));

            services.AddScoped<IDataBlob>(service => new AzureDataBlob(azureBlobConnection, azureBlobContainer));
            services.AddTransient<ITTGSUnitOfWork, TTGSUnitOfWork>();
            services.AddMediatR((typeof(CreateUserCommandHandler).Assembly));
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc(x =>
            {
                x.Filters.Add<CustomExceptionFilter>();
                x.Filters.Add<ValidatorActionFilter>();
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<AuthenticateRequestValidator>())
            .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.Configure<EmailSettings>(option => Configuration.GetSection("EmailSettings").Bind(option));

            services.AddSingleton<IEmailService, EmailService>();

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("SystemSettings");
            services.Configure<SystemSettings>(appSettingsSection);

            // configure jwt authentication
            var systemSettings = appSettingsSection.Get<SystemSettings>();
            var key = Encoding.ASCII.GetBytes(systemSettings.JwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IUserService, UserService>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                c.RoutePrefix = "ttgsapidocs";
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AspNetUsers>>();
            try
            {
                UpdateDatabases(app);
                SeedData.Initialize(context, userManager);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }

        private static void UpdateDatabases(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TTGSContext>())
                {
                    context.Database.Migrate();
                }

                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
