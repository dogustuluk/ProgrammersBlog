using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T>:IResult //bu şekilde hem bir liste hem de tek bir data taşıyabiliriz.
    {
        public T Data { get;} //new DataResult<Category>(ResıltStatus.Success, category);
        //eğer liste göndermek istersek -> new DataResult<IList<Category>>(ResultStatus.Success, categoryList); bunu yapabilmek için 'T'yi tek başına değil, out ile kullandık.
    }
}
