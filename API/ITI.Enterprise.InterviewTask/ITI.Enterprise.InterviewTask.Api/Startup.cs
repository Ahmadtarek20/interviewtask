using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using ITI.Enterprise.InterviewTask.DataModel;
using ITI.Enterprise.InterviewTask.DomainClasses;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using ITI.Enterprise.InterviewTask.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ITI.Enterprise.InterviewTask.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Using DbContextPool so that the application does not create a DbContext each time but look into the context pool for free context instead. 
            services.AddDbContext<AppDbContext>(options =>
            {
                if (!options.IsConfigured)
                {
                    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                }
            });
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddCors(c => c.AddPolicy("AllowAll", options =>
            {
                options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));


            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication(x =>
                {
                    // Using jwt bearer as the authentication schema.

                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {


                    //x.RequireHttpsMetadata = false;
                    // x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                    };
                });
            services.AddMvc(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                  
                    var jsonOutputFormatter =
                        setupAction.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                    if (jsonOutputFormatter != null)
                    {
                        // remove text/json as it is not the approved media type for working json at API level

                        if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                        {
                            jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                        }
                    }
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("InterviewTaskOpenApi"
                    , new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "InterviewTask API",
                        Version = "1",
                        Description = "This is the Interview Task API which you can access Products and Companies through it.\n Note: Authorization is implemented but it is turned off.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact() {
                            Email = "moamen_mohammed@hotmail.com",
                            Name = "Moamen Mohamed Helmey",
                    },
                    });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/InterviewTaskOpenApi/swagger.json",
                    "InterviewTask API");
                setupAction.RoutePrefix = "";
            });
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
