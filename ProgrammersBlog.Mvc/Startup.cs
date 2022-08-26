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
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");//login için bu sayfaya yönlendiriyoruz.
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true, //güvenlik için yapýlýr. js kodlarý ile cookie bilgilerinin gözükmemesi için
                    SameSite = SameSiteMode.Strict, //güvenlik sebebiyle kullanýlýr. csrf zafiyeti, xsrf zaafiyeti ya da session riding olarak da adlandýrýlýr."siteler arasý istek sahtekarlýðý". Strict ifadesi ile kullanýcý bilgileri sadece kendi sitemiz üzerinden geldiðinde iþlenmesini saðlar.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest//gerçek uygulamalarda her daim "always" þeklinde kullanýlýr.
                };
                options.SlidingExpiration = true;//kullanýcý sitemize giriþ yaptýktan sonra kullanýcýya tanýnan zamaný ifade eder.
                options.ExpireTimeSpan = TimeSpan.FromDays(7);//7 gün boyunca kullanýcýnýn tekrar giriþ yapmasý gerekmez.
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");//yetkisi olmayan sayfalara girmeye çalýþtýðý zaman hangi sayfaya yönlendirileceðidir.
            });
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
