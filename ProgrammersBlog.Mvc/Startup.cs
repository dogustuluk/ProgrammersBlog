using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());//enum converter
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; //nested object //json'a dönüþecek nesneler içerisinde farklý nesneler de var ise ->örn. Category gönderdiðimizde içerisinde include edilen Article'lar da var ise. Burada bug vardýr, dolayýsýyla controller'a da yazýyor olucaz.
            }); //runtimecompilation ile frontend kýsmýnda deðiþiklikler yaptýðýmýzda uygulamayý tekrardan derlememize gerek kalmýyor.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile));//derlenme esnasýnda automapper'ýn buradaki sýnýflarý taramasýný saðlar.Profiles sýnýflarýný buluyor ve ekliyor.
            services.LoadMyServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //sitemizde bulunmayan bir view içerisine gittiðimiz zaman hata almamýz için yazýlýr.
            }
            app.UseSession();
            app.UseStaticFiles(); //css,image,javascript dosyalarý olabilir.

            app.UseRouting();

            app.UseAuthentication();//kimlik doðrulamasý

            app.UseAuthorization();//yetki kontrolü

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=home}/{action=Index}/{id?}"
                    ); //sadece bir adet area olacaðý için MapAreaControllerRoute kullandýk. farklý arealar var ise->MapControllerRoute kullan.
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
