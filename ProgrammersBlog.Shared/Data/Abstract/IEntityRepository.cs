using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Data.Abstract
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        Task<T> GetAsync(Expression<Func<T,bool>> predicate, params Expression<Func<T, object>>[] includeProperties);//Expression ile sorgu yapabiliriz(filtre yapabiliriz.). kullanacağımız lambda expression'lar predicate olarak geçer. >> var kullanici = repository.Get(k => k.FirstName == "Doğuş"); <<örnek.. 
        //2.Expression ile kullanıcının makalelerini ya da bir makaleyi hangi kullanıcının yazdığını öğrenmek istersek kullanırız. Bu sefer array object alıyor olucaz çünkü hem yorumlarını hem de bir kullanıcıyı da yüklemek isteyebiliriz. Birden fazla includeProperties verebileceğimiz için de params keyword'ünü alması gerekmektedir. params, parametreler anlamındadır. Bir ya da daha fazla parametre vermemiz demektir. bu parametreler de includeProperties'in array'ine tek tek ekleniyor olacak.
        
        //yukarıdaki metotta bir tane entity getiriyoruz. Eğer 2 veya daha fazla getirmek istersek;
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate =null, params Expression<Func<T, object>>[] includeProperties); //predicate=null vermemizin sebebi ise tüm entity'leri yükleriz eğer null gelmez ise bizlere gelen filtreye göre ekliyor olucaz
        //-

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    }
}
