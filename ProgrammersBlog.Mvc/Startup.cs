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
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");//login i�in bu sayfaya y�nlendiriyoruz.
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true, //g�venlik i�in yap�l�r. js kodlar� ile cookie bilgilerinin g�z�kmemesi i�in
                    SameSite = SameSiteMode.Strict, //g�venlik sebebiyle kullan�l�r. csrf zafiyeti, xsrf zaafiyeti ya da session riding olarak da adland�r�l�r."siteler aras� istek sahtekarl���". Strict ifadesi ile kullan�c� bilgileri sadece kendi sitemiz �zerinden geldi�inde i�lenmesini sa�lar.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest//ger�ek uygulamalarda her daim "always" �eklinde kullan�l�r.
                };
                options.SlidingExpiration = true;//kullan�c� sitemize giri� yapt�ktan sonra kullan�c�ya tan�nan zaman� ifade eder.
                options.ExpireTimeSpan = TimeSpan.FromDays(7);//7 g�n boyunca kullan�c�n�n tekrar giri� yapmas� gerekmez.
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");//yetkisi olmayan sayfalara girmeye �al��t��� zaman hangi sayfaya y�nlendirilece�idir.
            });
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
