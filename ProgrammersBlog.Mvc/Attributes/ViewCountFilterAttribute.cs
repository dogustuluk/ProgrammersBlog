using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ProgrammersBlog.Services.Abstract;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ProgrammersBlog.Mvc.Attributes
{
    //-> buradaki ViewCountFilterAttribute'ünü nerede kullanmak istediğimizi belirtiyoruz. bir sınıf veya metotta kullanacağımızı belirtiyoruz.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] 
    //articleController -> detail action'ında kodluyor olucaz.
    //Yani buradaki tüm işlemleri detail action'ına göre yapıyormuş gibi düşünüyor olucaz.
    public class ViewCountFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var articleId = context.ActionArguments["articleId"];
            if (articleId is not null)
            {
                //ilk olarak cookie üzerinden articleId'yi al.
                string articleValue = context.HttpContext.Request.Cookies[$"article{articleId}"];//article5, article1,...
                if (string.IsNullOrEmpty(articleValue))
                {//daha önce kullanıcı makalenin okuyup okumadığının kontrolü. Eğer boş ise kullanıcı okumamış sayılır.
                    //cookie ataması yap. /*1 ifadesi -> bir yıl anlamında*/
                    Set($"article{articleId}", articleId.ToString(), 1, context.HttpContext.Response);
                    //okunma sayısını arttır. çünkü ilk defa cookie eklendi.
                    var articleService = context.HttpContext.RequestServices.GetService<IArticleService>();//daha farklı tasarım desenleri var. gerekli attribute'ler veya filtreler ile servisi sarmalayarak kullanabiliriz.
                    await articleService.IncreaseViewCountAsync(Convert.ToInt32(articleId));
                    
                    //view'in yüklenmesini sağla
                    await next();
                }
            }
            await next();
        }
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime, HttpResponse response)
        {/*string key -> saklayacağımız adı belirtir. Bizim örneğimizdeki article5, article2 yani "article{articleId}" kısmı.
          * value ise article'ın id'si 5 ise value 5.
          * expireTime ise tamamlanma süresi.
          * response -> response üzerindeki cookie'lere bu değerleri yazmamızı sağlar.
          */
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddYears(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMonths(6);

            response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }
    }
}
