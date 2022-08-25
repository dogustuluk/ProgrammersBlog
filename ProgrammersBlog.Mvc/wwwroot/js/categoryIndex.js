﻿$(document).ready(function () {
    $('#categoriesTable').DataTable({
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
                        url: '/Admin/Category/GetAllCategories/',
                        contentType: "application/json",//hangi formatta çalıştığımız veriyoruz. xml'de verebiliriz.
                        //ajax işleminde yapacağımız işlemelere geçiyoruz
                        beforeSend: function () { //ajax işlemini yapmadan önce yapmamız gereken işlemler.Örnek-> tablonun gizlenmesi ve spinner kısmının aktif edilmesii
                            //tabloyu gizle ve spinner'ı aktif et yani görünür yap
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            console.log(categoryListDto);
                            //ResultStatus durumunu kontrol etmeliyiz.
                            if (categoryListDto.ResultStatus === 0) { //üç eşittir hem değerin aynı olup olmadığını hem de değerin
                                //tipini kontrol eder. burada hem int hem de 0 olmasını istiyoruz. 0 ise başarılı
                                let tableBody = ""; //sebebi -> foreach ile döndüğümüzde tr etiketlerimizin içerisine eklemektir.
                                $.each(categoryListDto.Categories.$values, function (index, category) {
                                    //ilk parametre olarak hangi değerler içerisinde dönüleceği sorulur.
                                    tableBody += `
                                             <tr>
                                                <td>${category.Id}</td>
                                                <td>${category.Name}</td>
                                                <td>${category.Description}</td>
                                                <td>${convertFirstLetterToUpperCase(category.IsActive.toString())}</td>
                                                <td>${convertFirstLetterToUpperCase(category.IsDeleted.toString())}</td>
                                                <td>${category.Note}</td>
                                                <td>${convertToShortDate(category.CreatedDate)}</td>
                                                <td>${category.CreatedByName}</td>
                                                <td>${convertToShortDate(category.ModifiedDate)}</td>
                                                <td>${category.ModifiedByName}</td>
                                                <td>
                                                <button class="btn btn-primary btn-sm "><span class="fas fa-edit"></span></button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span></button>
                                              </td>
                                             </tr>`;
                                });
                                //devamında ise gelen veri ile tablodaki verinin yer değiştirmesi gerekir.

                                $('#categoriesTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(1500);
                            }
                            else {
                                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!');

                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
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
    //@* DataTables ends here *@
    //        @* Ajax Get / Getting the _CategoryAddPartial as Modal Form starts from here. *@
    $(function () {
        const url = '/Admin/Category/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });
        //@* Ajax Get / Getting the _CategoryAddPartial as Modal Form end here. *@
        //        @* Ajax Post / Posting the FormData as CategoryAddDto starts from here *@
        placeHolderDiv.on('click', '#btnSave',
            function (event) {
                event.preventDefault();//butonun kendi click işlemini engeller
                const form = $('#form-category-add');//form seçme
                const actionUrl = form.attr('action');//form'daki asp-action ifadesine karşılık gelir
                const dataToSend = form.serialize(); //formdaki veriyi CategoryAddDto yapıyoruz.
                $.post(actionUrl, dataToSend).done(function (data) {
                    console.log(data);
                    const categoryAddAjaxModel = jQuery.parseJSON(data);//'data' parse. CategoryAddAjaxModel oldu.
                    console.log(categoryAddAjaxModel);
                    const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                            <tr>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Id}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
                                <td>${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                <td>${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                                <td>
                                    <button class="btn btn-primary btn-sm"><span class="fas fa-edit"></span></button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span> </button>
                                </td>
                            </tr>`; //template literals -> stringleri formatlayabilir, ve formatlı olan
                        //stringlerde değişkenlerimizin değerlerini kullanabiliriz.

                        const newTableRowObject = $(newTableRow); //js // jQuery objects
                        newTableRowObject.hide();
                        $('#categoriesTable').append(newTableRowObject);//tabloya yeni satırları ekler
                        newTableRowObject.fadeIn(2500)//ekrana yavaştan görünür bir halde gelmesini sağlar

                        //toastr start
                        toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!');
                    }
                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            //her bir li'nin text özelliğini seçiyoruz
                            let text = $(this).text(); //$(this) ->içerisinde bulunduğumuz li'yi seçmemizi sağlar. seçilen li'nin text'ini almak istediğimiz için ->text()
                            //her bir text'i içerisinde toplayabileceğimiz farklı bir değişkene ihtiyaç var. bu döngünün başına gelip let summaryText oluştur.
                            summaryText = `*${text}\n`;//* -> uyarı mesajının önüne konacak olan işaret
                        });
                        toastr.warning(summaryText);
                    }
                });
            }); //div üzerinde eklediğimiz event calıştığında ya da tetiklendiğinde çalışacak işlemleri eklememizi sağlıyor
    });
    //Ajax Post / Posting the FormData as CategoryAddDto ends here
    //Ajax Post / Deleting a Category starts from here
    //silme işlemini yapabilmek için öncelikle sil butonuna tıklanma olayını yakalamamız gerekir.
    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();//burada butonun kendi bir işlevselliği varsa bunu deaktif ediyoruz.
            const id = $(this).attr('data-id');//hangi buton üzerine basıldıysa onu bu fonksiyon içerisinde kullanmak için $(this) kullanılır.
            //data-id sil butonundaki (et)category.id gelir
            ///tıklanan butonu yakalama
            const tableRow = $(`[name = "${id}"]`);
            const categoryName = tableRow.find('td:eq(1)').text(); //<td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td> alanını almış olduk.

            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${categoryName} adlı kategori silinecektir!`,
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
                        data: { categoryId: id }, //categoryService'teki parametreyi veriyoruz(categoryId).
                        url: '/Admin/Category/Delete/',
                        success: function (data) {
                            //IResult geliyor, bunu öncelikle json'a parse et.
                            const categoryDto = jQuery.parseJSON(data);
                            //ResultStatus tarafını kontrol et.
                            if (categoryDto.ResultStatus === 0) {
                                Swal.fire
                                    (
                                        'Silindi!',
                                        `${categoryDto.Category.Name} adlı kategori başarıyla silinmiştir.`,
                                        'success'
                                    );
                                //sil butonuna basıldığında ilgili satırın gizlenmesini sağlamak için

                                tableRow.fadeOut(1500);
                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${categoryDto.Message}`,
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