using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.AutoMapper.Profiles;
using ProgrammersBlog.Mvc.Filters;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Mvc.Helpers.Concrete;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;
using ProgrammersBlog.Shared.Utilities.Extensions;
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
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.Configure<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.ConfigureWritable<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.ConfigureWritable<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boþ geçilmemelidir!");
                options.Filters.Add<MvcExceptionFilter>();
            }).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());//enum converter
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; //nested object //json'a dönüþecek nesneler içerisinde farklý nesneler de var ise ->örn. Category gönderdiðimizde içerisinde include edilen Article'lar da var ise. Burada bug vardýr, dolayýsýyla controller'a da yazýyor olucaz.
            }).AddNToastNotifyToastr(); //runtimecompilation ile frontend kýsmýnda deðiþiklikler yaptýðýmýzda uygulamayý tekrardan derlememize gerek kalmýyor.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile),typeof(UserProfile),typeof(ViewModelsProfile),typeof(CommentProfile));//derlenme esnasýnda automapper'ýn buradaki sýnýflarý taramasýný saðlar.Profiles sýnýflarýný buluyor ve ekliyor.
            services.LoadMyServices(connectionString:Configuration.GetConnectionString("LocalDB"));
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");//login için bu sayfaya yönlendiriyoruz.
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true, //güvenlik için yapýlýr. js kodlarý ile cookie bilgilerinin gözükmemesi için
                    SameSite = SameSiteMode.Strict, //güvenlik sebebiyle kullanýlýr. csrf zafiyeti, xsrf zaafiyeti ya da session riding olarak da adlandýrýlýr."siteler arasý istek sahtekarlýðý". Strict ifadesi ile kullanýcý bilgileri sadece kendi sitemiz üzerinden geldiðinde iþlenmesini saðlar.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest//gerçek uygulamalarda her daim "always" þeklinde kullanýlýr.
                };
                options.SlidingExpiration = true;//kullanýcý sitemize giriþ yaptýktan sonra kullanýcýya tanýnan zamaný ifade eder.
                options.ExpireTimeSpan = TimeSpan.FromDays(7);//7 gün boyunca kullanýcýnýn tekrar giriþ yapmasý gerekmez.
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");//yetkisi olmayan sayfalara girmeye çalýþtýðý zaman hangi sayfaya yönlendirileceðidir.
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

            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=home}/{action=Index}/{id?}"
                    ); //sadece bir adet area olacaðý için MapAreaControllerRoute kullandýk. farklý arealar var ise->MapControllerRoute kullan.
                /*conventional route*/
                endpoints.MapControllerRoute
                (
                    name:"article", //route'a verilecek olan isim.
                    pattern:"Makaleler/{title}/{articleId}", /*buradaki routing'in neye göre çalýþacaðý.
                                                   * makale baþlýðý / makale id -> ayný baþlýklý birden fazla makale olabileceði için id'sini de ekliyoruz.
                                                   */
                    defaults:new {controller = "Article", action = "Detail"}
                );

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
