using ProgrammersBlog.Shared.Entities.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get;} //kullanımı-> ResultStatus.Success ya da ResultStatus.Error gibi.
        public string Message { get;}
        public Exception Exception { get;}
        public IEnumerable<ValidationError> ValidationErrors { get; set; }/*IEnumerable olmasının iki sebebi var
                                                                           1- Birden fazla hata olabilir 
                                                                           2- Dışarıdan düzenlenmesini istemiyoruz.*/
    } 
}
