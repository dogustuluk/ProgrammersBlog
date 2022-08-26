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
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; //nested object //json'a d�n��ecek nesneler i�erisinde farkl� nesneler de var ise ->�rn. Category g�nderdi�imizde i�erisinde include edilen Article'lar da var ise. Burada bug vard�r, dolay�s�yla controller'a da yaz�yor olucaz.
            }); //runtimecompilation ile frontend k�sm�nda de�i�iklikler yapt���m�zda uygulamay� tekrardan derlememize gerek kalm�yor.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile));//derlenme esnas�nda automapper'�n buradaki s�n�flar� taramas�n� sa�lar.Profiles s�n�flar�n� buluyor ve ekliyor.
            services.LoadMyServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //sitemizde bulunmayan bir view i�erisine gitti�imiz zaman hata almam�z i�in yaz�l�r.
            }
            app.UseSession();
            app.UseStaticFiles(); //css,image,javascript dosyalar� olabilir.

            app.UseRouting();

            app.UseAuthentication();//kimlik do�rulamas�

            app.UseAuthorization();//yetki kontrol�

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=home}/{action=Index}/{id?}"
                    ); //sadece bir adet area olaca�� i�in MapAreaControllerRoute kulland�k. farkl� arealar var ise->MapControllerRoute kullan.
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
