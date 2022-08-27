using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{//Dto'yu kullanmamamızın nedeni -> buradaki view model sadece mvc katmanımızı ilgilendirir ve sadece burada kullanılacak. Dto'larımızı service katmanında, mvc katmanında hatta istersek api katmanında da kullanabiliriz. Dto'lar biraz daha generic bir yapıdadır; istenilen projede kullanabiliriz. buradaki view model ise sadece ajax işlemlerimiz ile ilgili ve view ile ilgili.
    public class UserUpdateAjaxViewModel
    {
        public UserUpdateDto UserUpdateDto { get; set; }
        public string UserUpdatePartial { get; set; }
        public UserDto UserDto { get; set; }
    }
}
