$(document).ready(function () {
    const dataTable = $('#articlesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET', //kategori listesi almayı istediğimiz için type ->GET
                        url: '/Admin/Article/GetAllArticles/',
                        contentType: "application/json",//hangi formatta çalıştığımız veriyoruz. xml'de verebiliriz.
                        //ajax işleminde yapacağımız işlemelere geçiyoruz
                        beforeSend: function () { //ajax işlemini yapmadan önce yapmamız gereken işlemler.Örnek-> tablonun gizlenmesi ve spinner kısmının aktif edilmesii
                            //tabloyu gizle ve spinner'ı aktif et yani görünür yap
                            $('#articlesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const articleResult = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(articleResult);
                            //ResultStatus durumunu kontrol etmeliyiz.
                            if (articleResult.Data.ResultStatus === 0) { //üç eşittir hem değerin aynı olup olmadığını hem de değerin
                                //tipini kontrol eder. burada hem int hem de 0 olmasını istiyoruz. 0 ise başarılı
                                let categoriesArray = [];//aynı kategori olunca hata vermesinin çözümü - başlangıç
                                $.each(articleResult.Data.Articles.$values, function (index, article) {
                                    const newArticle = getJsonNetObject(article, articleResult.Data.Articles.$values);
                                    let newCategory = getJsonNetObject(newArticle.Category, newArticle);//aynı kategori olunca hata vermesinin çözümü
                                    if (newCategory != null) {
                                        categoriesArray.push(newCategory);
                                    }
                                    if (newCategory === null) {
                                        newCategory = categoriesArray.find((category) => {
                                            return category.$id== newArticle.Category.$ref;
                                        })
                                    }
                                    console.log(newCategory);
                                    console.log(newArticle);
                                    const newTableRow = dataTable.row.add([
                                        newArticle.Id,
                                        newCategory.Name, //aynı kategori olunca hata vermesinin çözümü - son
                                        newArticle.Title,
                                        `<img src="/img/${newArticle.Thumbnail}" alt="${newArticle.Title}" class="my-image-table" />`,
                                        `${convertToShortDate(newArticle.Date)}`,
                                        newArticle.ViewsCount,
                                        newArticle.CommentCount,
                                        `${newArticle.IsActive ? "Evet" : "Hayır"}`,
                                        `${newArticle.IsDeleted ? "Evet" : "Hayır"}`,
                                        `${convertToShortDate(newArticle.CreatedDate)}`,
                                        newArticle.CreatedByName,
                                        `${convertToShortDate(newArticle.ModifiedDate)}`,
                                        newArticle.ModifiedByName,
                                        `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${newArticle.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${newArticle.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                    ]).node();
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${newArticle.Id}`);
                                });
                                //devamında ise gelen veri ile tablodaki verinin yer değiştirmesi gerekir.
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#articlesTable').fadeIn(1500);
                            }
                            else {
                                toastr.error(`${articleResult.Data.Message}`, 'İşlem Başarısız!');

                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#articlesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');

                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });
    //DataTables ends here

    //Ajax Post / Deleting a User starts from here
    //silme işlemini yapabilmek için öncelikle sil butonuna tıklanma olayını yakalamamız gerekir.
    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();//burada butonun kendi bir işlevselliği varsa bunu deaktif ediyoruz.
            const id = $(this).attr('data-id');//hangi buton üzerine basıldıysa onu bu fonksiyon içerisinde kullanmak için $(this) kullanılır.
            //data-id sil butonundaki (et)category.id gelir
            ///tıklanan butonu yakalama
            const tableRow = $(`[name = "${id}"]`);
            const articleTitle = tableRow.find('td:eq(2)').text();

            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${articleTitle} başlıklı makale silinecektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, silmek istiyorum!',
                cancelButtonText: 'Hayır, silmek istiyorum!'
            }).then((result) => { //bu işlem açıldıktan sonra(yukarıdaki işlem bittikten sonra da denebilir) yapılacak işlemler
                if (result.isConfirmed) //evet butonuna basıp basmadığının kontrolü
                {
                    //kategori silme işlemi başarılı mı kontrolü
                    //ajax post start
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { articleId: id }, //categoryService'teki parametreyi veriyoruz(categoryId).
                        url: '/Admin/Article/Delete/',
                        success: function (data) {
                            //IResult geliyor, bunu öncelikle json'a parse et.
                            const articleResult = jQuery.parseJSON(data);
                            //ResultStatus tarafını kontrol et.
                            if (articleResult.ResultStatus === 0) {
                                Swal.fire
                                    (
                                        'Silindi!',
                                        `${articleResult.Message}`,
                                        'success'
                                    );
                                //sil butonuna basıldığında ilgili satırın gizlenmesini sağlamak için

                                dataTable.row(tableRow).remove().draw();
                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${articleResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!")
                        }
                    });
                    //ajax post end
                }
            });
        });
   
});