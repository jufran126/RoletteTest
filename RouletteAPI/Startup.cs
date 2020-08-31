using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RouletteAPI.Data;
using RouletteAPI.Services.Contracts;
using RouletteAPI.Services.Implementations;

namespace RouletteAPI
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
            #region Services
            services.AddScoped<IAuthService, AuthService>();
            #endregion
            #region DB
            services.AddScoped<IDbContex, DbContex>();
            #endregion

            AddSwagger(services);
            AddJWT(services);
            services.AddControllers(x =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                x.Filters.Add(new AuthorizeFilter(policy));
            });
        }
        private void AddJWT(IServiceCollection services)
        {
            services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme
                ).AddJwtBearer(x =>{
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
            });
            services.AddAuthorization(x =>
            {
                x.AddPolicy("UserPolicy", pol => pol.RequireRole("User"));
            });

        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Roulette {groupName}",
                    Version = groupName,
                    Description = "Roulette API",
                    Contact = new OpenApiContact
                    {
                        Name = "Roulette",
                        Email = string.Empty
                    }
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roulette API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
