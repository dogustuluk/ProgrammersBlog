using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{//Dto'yu kullanmamamızın nedeni -> buradaki view model sadece mvc katmanımızı ilgilendirir ve sadece burada kullanılacak. Dto'larımızı service katmanında, mvc katmanında hatta istersek api katmanında da kullanabiliriz. Dto'lar biraz daha generic bir yapıdadır; istenilen projede kullanabiliriz. buradaki view model ise sadece ajax işlemlerimiz ile ilgili ve view ile ilgili.
    public class CategoryAddAjaxViewModel
    {
        public CategoryAddDto CategoryAddDto { get; set; }
        public string CategoryAddPartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
