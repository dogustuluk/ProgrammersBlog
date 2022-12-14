using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        public string Description { get; set; }

        [DisplayName("Kategori Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        public bool IsActive { get; set; }
    }
}
