using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Mvc.Models;
using ProgrammersBlog.Services.Abstract;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.ViewComponents
{
    public class RightSideBarViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public RightSideBarViewComponent(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        //kategorilerin ve makalelerin alınmasını sağlar.
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            var articlesResult = await _articleService.GetAllByViewCountAsync(isAscending:false, takeSize:5);
            return View(new RightSideBarViewModel
            {
                Categories = categoriesResult.Data.Categories,
                Articles = articlesResult.Data.Articles
            });
        }
    }
}
