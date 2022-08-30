using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
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
            } public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla kalıcı bir şekilde silinmiştir.";
            }
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Herhangi bir makale bulunamadı.";
                return "Böyle bir makale bulunamadı";
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

        }
    }
}
