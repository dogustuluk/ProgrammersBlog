using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Models
{
    public class ArticleSearchViewModel
    {
        public ArticleListDto ArticleListDto { get; set; } /*paging değerleri ve makale listesini tutar
                                                            * 
                                                            */
        public string Keyword { get; set; }/*keyword'ü buraya eklememizin nedeni ->>
                                            * ileride daha fazla parametre eklemek isteyebiliriz. Eğer articleListDto'ya eklersek gereksiz bir çok değer eklenmiş olacak. her zaman bir arama işlemi yapılmıyor.
                                            */
    }
}
