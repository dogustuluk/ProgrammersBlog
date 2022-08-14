using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    //abstract vermemizin nedeni >> çünkü verdiğimiz base değerlerin diğer sınıflarda değişikliğe uğramasını isteyebiliriz. 
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now; //başka bir sınıfta override edebiliriz abstract olarak tanımlarsak. Tabii override edilmesini istiyorsak "virtual" keyword'ü ile de işaretlememiz gerekmektedir.
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false; //bir şey oluşturduğum anda "silinmedi" cevabını vermek için 'false' olarak belirttik.
        public virtual bool IsActive { get; set; } = true;
        public virtual string CreatedByName { get; set; }
        public virtual string ModifiedByName { get; set; }
        //her ikisini de string olarak belirtmemizin sebebi kayıt olma durumunun olmaması.
    }
}
