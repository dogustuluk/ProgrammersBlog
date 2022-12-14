using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Utilities
{
    public static class Messages
    {
        public static class General
        {
            public static string ValidationError()
            {
                return $"Bir veya daha fazla validasyon hatası ile karşılaşıldı.";
            }
        }
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }
            public static string NotFoundById(int categoryId)
            {

                return $"{categoryId} kategori koduna sahip bir kategori bulunamadı.";
            }
            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklenmiştir.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla kalıcı bir şekilde silinmiştir.";
            }
            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla arşivden geri getirilmiştir.";
            }
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Herhangi bir makale bulunamadı.";
                return "Böyle bir makale bulunamadı";
            }
            public static string NotFoundById(int articleId)
            {

                return $"{articleId} makale koduna sahip bir makale bulunamadı.";
            }
            public static string Add(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla eklendi.";
            }
            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla silindi.";
            }
            public static string HardDelete(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla kalıcı bir şekilde silindi.";
            }
            public static string Update(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla güncellendi.";

            }
            public static string UndoDelete(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla arşivden geri getirilmiştir.";
            }
            public static string IncreaseViewCount(string articleTitle)
            {
                return $"{articleTitle} başlıklı makalenin okunma sayısı başarıyla arttırılmıştır.";
            }

        }
        public static class Comment
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Herhangi bir yorum bulunamadı.";
                return "Böyle bir yorum bulunamadı";
            }
            public static string Approve(int commentId)
            {
                return $"{commentId} nolu yorum başarıyla onaylanmıştır.";
            }
            public static string Add(string createdByName)
            {
                return $"Sayın {createdByName}, yorumunuz başarılı bir şekilde eklenmiştir.";
            }
            public static string Delete(string createdByName)
            {
                return $"{createdByName} adlı kullanıcının yorumu başarıyla silinmiştir.";
            }
            public static string HardDelete(string createdByName)
            {
                return $"{createdByName} adlı kullanıcının yaptığı yorum kalıcı olarak başarılı bir şekilde silinmiştir.";
            }
            public static string Update(string createdByName)
            {
                return $"{createdByName} adlı kullanıcının yaptığı yorum başarıyla güncellenmiştir.";
            }
            public static string UndoDelete(string createdByName)
            {
                return $"{createdByName} adlı kullanıcının eklediği yorum başarıyla arşivden geri getirilmiştir.";
            }
        }
        public static class User
        {
            //public static string NotFound(bool isPlural)
            //{
            //    if (isPlural) return "Hiç bir kategori bulunamadı.";
            //    return "Böyle bir kategori bulunamadı.";
            //}
            public static string NotFoundById(int userId)
            {

                return $"{userId} kullanıcı koduna sahip bir kullanıcı bulunamadı.";
            }
            //public static string Add(string categoryName)
            //{
            //    return $"{categoryName} adlı kategori başarıyla eklenmiştir.";
            //}
            //public static string Update(string categoryName)
            //{
            //    return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            //}
            //public static string Delete(string categoryName)
            //{
            //    return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            //}
            //public static string HardDelete(string categoryName)
            //{
            //    return $"{categoryName} adlı kategori başarıyla kalıcı bir şekilde silinmiştir.";
            //}
            //public static string UndoDelete(string categoryName)
            //{
            //    return $"{categoryName} adlı kategori başarıyla arşivden geri getirilmiştir.";
            //}
        }
    }


}
