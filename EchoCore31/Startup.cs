using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EchoCore31
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
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.Run(async context =>
            {
                String s = "";
                var request = context.Request;
                var response = context.Response;
                s = s + "Method: " + request.Method + "<br/>\r\n PATH: " + request.Path + "<br/>\r\n";
                s += $"Host:{request.Host} <br/>\r\nPathBase: {request.PathBase} \r\n";


                foreach (var header in request.Headers)
                {
                    s = s + header + "<br/>\r\n";
                }
                context.Response.ContentType = "text/html";

                s = s + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + "<br/>\r\n";
                s = s + Environment.GetEnvironmentVariable("ASPNETCORE_LOCAL") + "<br/>\r\n";
                s = s + Environment.GetEnvironmentVariable("ASPNETCORE_VSLOCAL") + "<br/>\r\n";
                s = s + Environment.GetEnvironmentVariable("ASPNETCORE_VS") + "<br/>\r\n";

                s = s + "ENVIRONMENT: " + Configuration.GetValue<String>("ENVIRONMENT") + "<br/>\r\n";
                s = s + "LOCAL: " + Configuration.GetValue<String>("LOCAL") + "<br/>\r\n";
                s = s + "VSLOCAL: " + Configuration.GetValue<String>("VSLOCAL") + "<br/>\r\n";
                s = s + "NOENV: " + Configuration.GetValue<String>("NOENV") + "<br/>\r\n";


                await context.Response.WriteAsync(s);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
