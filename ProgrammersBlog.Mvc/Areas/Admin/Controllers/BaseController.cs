using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Helpers.Abstract;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = imageHelper;
        }
        protected UserManager<User> UserManager { get;}
        protected IMapper Mapper { get;}
        protected IImageHelper ImageHelper { get;}

        //LoggedInUser'ın otomatik olarak set edilmesini istiyoruz.
        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result;
       

    }
}
