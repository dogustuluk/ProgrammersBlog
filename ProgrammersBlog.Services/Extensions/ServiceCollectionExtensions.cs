using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Extensions
{//extensions sınıf ve metotlarımızı kullanabilmek için static yapmamız gerekir.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<ProgrammersBlogContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User Password Options
                options.Password.RequireDigit = false; //şifrede rakam bulunmasının ayarı.
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;//özel karakterlerden kaç tane olması gerektiği(!,? gibi)
                options.Password.RequireNonAlphanumeric = false;//@,!,?,$ işareti gibi özel karakterlerin kullanılmasını zorunlu kılıp kılmamanın ayarı.
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                //User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$#%"; //kullanıcı adı oluşturulurken kullanılan karakterler bütününü temsil eder. Özel karakterlerin bulunup bulunmadığını temsil eder.
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ProgrammersBlogContext>();
            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15); /*
                                                             * Eğer fazla sayıda kullanıcıya sahip isek asla Zero olarak vermemeliyiz. Çünkü bu  kod ile her saniye veri tabanına bir istek atmış oluyoruz. Genellikle 30dk kafi olmaktadır.
                                                             */
            });
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();
            return serviceCollection;
        }
    }
}
