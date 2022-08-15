using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        //UnitOfWork sayesinde tüm repository'lerimizi tek bir yerden kontrol etmemizi sağlıyor olucaz.
        //burada EntityFramework yapısına bağımlı kalmıyor olucaz. ado.net ya da dapper yapısı da kullanıyor olsak bu UnitOfWork yapısını kullanabiliriz.
        IArticleRepository Articles { get; } //unitofwork.Articles diyerek makalelere erişebiliyor olucaz.
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; } //_unitOfWork.Categories.AddAsync(); -> kullanım örneği.
        Task<int> SaveAsync(); //int olarak veriyoruz çünkü etkilenen kayıt sayısını almak isteyebiliriz.
        /*örnek ->
        _unitOfWork.Categories.AddAsync(user);
        _unitOfWork.Users.AddAsync(user);
        _unitOfWork.SaveAsync();
        ->> herhangi birinde hata olursa tüm işlemler geri alınır.
        */
    }
}
