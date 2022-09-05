using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent:ViewComponent
    {//her bir viewcomponent'ın bir tane invoke metoduna ihtiyacı vardır
        //view'ü return etmeden önce ->> model kullanma ya da belirli hesaplamalar yapma şansı tanır. bu yüzden partial view'e göre oldukça büyük artıları vardır.
        //bir nevi controller içerisinde işlem yapmaya benzer.
        //viewComponent asenkron işlem yapmaya izin vermez.
        private readonly UserManager<User> _userManager;

        public AdminMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //.Result ile direkt olarak işlemin sonucunu alırız.
            var roles = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return Content("Kullanıcı Bulunamadı.");
            }
            if (roles == null)
            {
                return Content("Roller Bulunamadı.");
            }
            return View(new UserWithRolesViewModel
            {
                User = user,
                Roles = roles
            });//admin menüsü yüklendiğinde içerisinde bu model ile gelecek.
            
        }
    }
}
