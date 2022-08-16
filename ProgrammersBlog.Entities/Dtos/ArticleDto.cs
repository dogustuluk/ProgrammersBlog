using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    //View tarafında esneklik sağlamak istersek bu sınıfta ResultStatus taşımamız gerekir. Örneğin frontend kısmında gelen verinin status'ü error ise tabloyu gösterme ya da farklı şekilde göster şeklindeki senaryolar için gerekli.
    public class ArticleDto:DtoGetBase
    {
        public Article Article { get; set; }
       // public override ResultStatus ResultStatus { get; set; } = ResultStatus.Success; //-> örnek default kod istersek.
    }
}
