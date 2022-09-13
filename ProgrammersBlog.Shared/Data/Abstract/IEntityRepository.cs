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
        Task<T> GetAsyncV2(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T, object>>> includeProperties);/*Burada ne yapmış olduk
                                                                                                                             * Hem predicates'leri hem de includeproperty'leri bir liste halinde almış olduğumuzu belirtmiş olduk.
                                                                                                                             *örnek-> if(isActive==true) predicates.Add()
                                                                                                                             *bu şekilde get işlemlerinde birbinin aynısı olan metotları tekrardan tanımlamak yerine bunu kullanabiliriz filtre ekleyerek
                                                                                                                             */
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate =null, params Expression<Func<T, object>>[] includeProperties); //predicate=null vermemizin sebebi ise tüm entity'leri yükleriz eğer null gelmez ise bizlere gelen filtreye göre ekliyor olucaz
        //-
        Task<IList<T>> GetAllAsyncV2(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T, object>>> includeProperties);/*Burada ne yapmış olduk
                                                                                                                             * Hem predicates'leri hem de includeproperty'leri bir liste halinde almış olduğumuzu belirtmiş olduk.
                                                                                                                             *örnek-> if(isActive==true) predicates.Add()
                                                                                                                             *bu şekilde get işlemlerinde birbinin aynısı olan metotları tekrardan tanımlamak yerine bunu kullanabiliriz filtre ekleyerek
                                                                                                                             */
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IList<T>> SearchAsync(IList<Expression<Func<T,bool>>> predicates, params Expression<Func<T,object>>[] includeProperties); /*IList predicates eklememizin sebebi-> 
                                                                                 *buradaki predicate'leri servis katmanında ayrı ayrı eklemek isteyebiliriz. Yani belirli kontrolleri yaptıktan sonra isteyebiliriz.
                                                                                 *Bunu istememizin sebebine örnek vermek gerekirse -> 6 farklı parametre alabiliriz ve bunlar opsiyonel olabilir. yani hepsi de gelebilir 1 tanesi de gelebilir. dolayısıyla burada tek bir predicate ile alırsak bunu "&" operatörü ile göndermeye çalışırsak problem yaşarız. Her bir olasılığı kontrol etmek yerine böyle bir predicate'in olup olmadığını kontrol etmek daha doğru bir yol olacaktır.
                                                                                 */
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);//null ile predicate vermezsek ilgili tablonun, sınıfın tüm verilerinin sayısını dönmesini sağlarız.
        IQueryable<T> GetAsQueryable();
    }
}
