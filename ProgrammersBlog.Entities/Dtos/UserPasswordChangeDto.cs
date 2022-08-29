using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şu anki Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifrenizi Tekrar Giriniz")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Yeni şifreniz ile yeni şifrenizin tekrarı birbiriyle uyuşmuyor",)]
        public string RepeatPassword { get; set; }
    }
}
