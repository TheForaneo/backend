using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webapi.Models;
using webapi.Services;

namespace Nueva_carpeta__4_
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("secretkey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            /////Cliente
            services.Configure<ClientestoreDatabaseSettings>(Configuration.GetSection(nameof(ClientestoreDatabaseSettings)));
            services.AddSingleton<IClientestoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ClientestoreDatabaseSettings>>().Value);
            services.AddSingleton<ClienteService>();
            /////Cliente

            //****************************
            services.Configure<VehiculostoreDatabaseSettings>(Configuration.GetSection(nameof(VehiculostoreDatabaseSettings)));
            services.AddSingleton<IVehiculostoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<VehiculostoreDatabaseSettings>>().Value);
            services.AddSingleton<VehiculoService>();
            //****************************

            //****************************
            services.Configure<TallerstoreDatabaseSettings>(Configuration.GetSection(nameof(TallerstoreDatabaseSettings)));
            services.AddSingleton<ITallerstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TallerstoreDatabaseSettings>>().Value);
            services.AddSingleton<TallerService>();
            //****************************

            //****************************
            services.Configure<SolicitudstoreDatabaseSettings>(Configuration.GetSection(nameof(SolicitudstoreDatabaseSettings)));
            services.AddSingleton<ISolicitudstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SolicitudstoreDatabaseSettings>>().Value);
            services.AddSingleton<SolicitudService>();
            //****************************
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
