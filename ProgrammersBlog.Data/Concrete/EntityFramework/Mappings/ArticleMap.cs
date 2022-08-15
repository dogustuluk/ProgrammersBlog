using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();//id alanının bir bir artmasını sağlıyor.
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.SeoAuthor).IsRequired();
            builder.Property(x => x.SeoAuthor).HasMaxLength(50);
            builder.Property(x => x.SeoDescription).HasMaxLength(150);
            builder.Property(x => x.SeoDescription).IsRequired();
            builder.Property(x => x.SeoTags).IsRequired();
            builder.Property(x => x.SeoTags).HasMaxLength(70);
            builder.Property(x => x.ViewsCount).IsRequired();
            builder.Property(x => x.CommentCount).IsRequired();
            builder.Property(x => x.Thumbnail).IsRequired();
            builder.Property(x => x.Thumbnail).HasMaxLength(250);
            builder.Property(x => x.CreatedByName).IsRequired();
            builder.Property(x => x.CreatedByName).HasMaxLength(50);
            builder.Property(x => x.ModifiedByName).IsRequired();
            builder.Property(x => x.ModifiedByName).HasMaxLength(50);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId); //generic kısım verilmeyebilir ama okunurluk artması için yazdık.
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);
            builder.ToTable("Articles"); //tabloya dönüşürken hangi ismi alacağını veriyoruz.

            builder.HasData(
                new Article
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "C# 9.0 ve .NET5 Yenilikleri",
                    Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
                    Thumbnail = "Default.jpg",
                    SeoDescription = "C# 9.0 ve .NET5 Yenilikleri",
                    SeoTags = "C#, C# 9.0, .NET5, .NET Framework, .NET Core",
                    SeoAuthor = "Doğuş Tuluk",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# 9.0 ve .NET5 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 189,
                    CommentCount = 1
                },
                new Article
                {
                    Id = 2,
                    CategoryId = 2,
                    Title = "C++ 11 ve 19 Yenilikleri",
                    Content = "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.",
                    Thumbnail = "Default.jpg",
                    SeoDescription = "C++ 11 ve 19 Yenilikleri",
                    SeoTags = "C++, C++ 9, C++ 11",
                    SeoAuthor = "Doğuş Tuluk",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ 11 ve 19 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 105,
                    CommentCount = 1
                },
                new Article
                {
                    Id = 3,
                    CategoryId = 3,
                    Title = "JavaScript Es2019 ve ES2020 Yenilikleri",
                    Content = "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.",
                    Thumbnail = "Default.jpg",
                    SeoDescription = "JavaScript Es2019 ve ES2020 Yenilikleri",
                    SeoTags = "JavaScript, JS, JS ES2019, JS ES2020, ES2019, ES2020, JavaScript ES2019, JavaScript ES2020",
                    SeoAuthor = "Doğuş Tuluk",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "JavaScript Es2019 ve ES2020 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 209,
                    CommentCount = 1
                }
                );
        }
    }
}
