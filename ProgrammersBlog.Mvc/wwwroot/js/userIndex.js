$(document).ready(function () {
    const dataTable = $('#usersTable').DataTable({
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
                        url: '/Admin/User/GetAllUsers/',
                        contentType: "application/json",//hangi formatta çalıştığımız veriyoruz. xml'de verebiliriz.
                        //ajax işleminde yapacağımız işlemelere geçiyoruz
                        beforeSend: function () { //ajax işlemini yapmadan önce yapmamız gereken işlemler.Örnek-> tablonun gizlenmesi ve spinner kısmının aktif edilmesii
                            //tabloyu gizle ve spinner'ı aktif et yani görünür yap
                            $('#usersTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const userListDto = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(userListDto);
                            //ResultStatus durumunu kontrol etmeliyiz.
                            if (userListDto.ResultStatus === 0) { //üç eşittir hem değerin aynı olup olmadığını hem de değerin
                                //tipini kontrol eder. burada hem int hem de 0 olmasını istiyoruz. 0 ise başarılı
                                $.each(userListDto.Users.$values, function (index, user) {
                                const newTableRow = dataTable.row.add([
                                        user.Id,
                                        user.UserName,
                                        user.Email,
                                        user.PhoneNumber,
                                        `<img src="/img/${user.Picture}" alt="${user.UserName}" class="my-image-table">`,
                                `       
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${user.Id}"><span class="fas fa-edit"></span></button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${user.Id}"><span class="fas fa-minus-circle"></span> </button>
                                `
                                ]).node();
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${user.Id}`);
                                });
                                //devamında ise gelen veri ile tablodaki verinin yer değiştirmesi gerekir.
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#usersTable').fadeIn(1500);
                            }
                            else {
                                toastr.error(`${userListDto.Message}`, 'İşlem Başarısız!');

                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(1000);
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
    //        @* Ajax Get / Getting the _UserAddPartial as Modal Form starts from here. *@
    $(function () {
        const url = '/Admin/User/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });
        //@* Ajax Get / Getting the _UserAddPartial as Modal Form end here. *@
        //        @* Ajax Post / Posting the FormData as UserAddDto starts from here *@
        placeHolderDiv.on('click', '#btnSave',
            function (event) {
                event.preventDefault();//butonun kendi click işlemini engeller
                const form = $('#form-user-add');//form seçme
                const actionUrl = form.attr('action');//form'daki asp-action ifadesine karşılık gelir
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url:actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType:false,
                    success: function (data) {
                        console.log(data);
                        const userAddAjaxModel = jQuery.parseJSON(data);//'data' parse. CategoryAddAjaxModel oldu.
                        console.log(userAddAjaxModel);
                        const newFormBody = $('.modal-body', userAddAjaxModel.UserAddPartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');
                            const newTableRow = dataTable.row.add([
                                userAddAjaxModel.UserDto.User.Id,
                                userAddAjaxModel.UserDto.User.UserName,
                                userAddAjaxModel.UserDto.User.Email,
                                userAddAjaxModel.UserDto.User.PhoneNumber,
                                `<img src="/img/${userAddAjaxModel.UserDto.User.Picture}" alt="${userAddAjaxModel.UserDto.User.UserName}" class="my-image-table">`,
                                `
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${userAddAjaxModel.UserDto.User.Id}"><span class="fas fa-edit"></span></button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${userAddAjaxModel.UserDto.User.Id}"><span class="fas fa-minus-circle"></span> </button>
                                `
                            ]).node();
                            const jqueryTableRow = $(newTableRow);
                            jqueryTableRow.attr('name', `${userAddAjaxModel.UserDto.User.Id}`);
                            dataTable.row(newTableRow).draw();
                            //toastr start
                            toastr.success(`${userAddAjaxModel.UserDto.Message}`, 'Başarılı İşlem!');
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
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }); //div üzerinde eklediğimiz event calıştığında ya da tetiklendiğinde çalışacak işlemleri eklememizi sağlıyor
    });
    //Ajax Post / Posting the FormData as UserAddDto ends here
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
            const userName = tableRow.find('td:eq(1)').text(); //<td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td> alanını almış olduk.

            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${userName} adlı kullanıcı silinecektir!`,
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
                        data: { userId: id }, //categoryService'teki parametreyi veriyoruz(categoryId).
                        url: '/Admin/User/Delete/',
                        success: function (data) {
                            //IResult geliyor, bunu öncelikle json'a parse et.
                            const userDto = jQuery.parseJSON(data);
                            //ResultStatus tarafını kontrol et.
                            if (userDto.ResultStatus === 0) {
                                Swal.fire
                                    (
                                        'Silindi!',
                                        `${userDto.User.UserName} adlı kullanıcı başarıyla silinmiştir.`,
                                        'success'
                                    );
                                //sil butonuna basıldığında ilgili satırın gizlenmesini sağlamak için

                                dataTable.row(tableRow).remove().draw();
                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${userDto.Message}`,
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
    //Ajax Get / getting the _UserUpdatePartial as Modal Form start from here
    $(function () {
        const url = '/Admin/User/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { userId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Bir hata oluştu");
            });
        });

    //Ajax Post / Updating a User starts from here
        //öncelikle modal form üzerindeki btn-save butonunun event'ini yakala
        placeHolderDiv.on('click', '#btnUpdate',
            function (event) {
                event.preventDefault();
                const form = $('#form-user-update');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        const userUpdateAjaxModel = jQuery.parseJSON(data);
                        console.log(userUpdateAjaxModel);
                        const id = userUpdateAjaxModel.UserDto.User.Id;
                        const tableRow = $(`[name="${id}"]`);
                        const newFormBody = $('.modal-body', userUpdateAjaxModel.UserUpdatePartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');
                            dataTable.row(tableRow).data([
                                userUpdateAjaxModel.UserDto.User.Id,
                                userUpdateAjaxModel.UserDto.User.UserName,
                                userUpdateAjaxModel.UserDto.User.Email,
                                userUpdateAjaxModel.UserDto.User.PhoneNumber,
                                `<img src="/img/${userUpdateAjaxModel.UserDto.User.Picture}" alt="${userUpdateAjaxModel.UserDto.User.UserName}" class="my-image-table" />`,
                                `
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${userUpdateAjaxModel.UserDto.User.Id}"><span class="fas fa-edit"></span></button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${userUpdateAjaxModel.UserDto.User.Id}"><span class="fas fa-minus-circle"></span> </button>
                                `
                            ]);
                            tableRow.attr("name", `${id}`);
                            dataTable.row(tableRow).invalidate();//invalidate -> datatable'ın değişiklikten haberi olmasını sağlarız.
                            toastr.success(`${userUpdateAjaxModel.UserDto.Message}`, "Başarılı İşlem!");
                        }
                        else {
                            let summaryText = "";
                            $('#validation-summary > ul > li').each(function () {
                                let text = $(this).text();
                                summaryText = `*${text}\n`;
                            });
                            toastr.warning(summaryText);
                        }
                    },
                    error: function (err) {
                        console.log(err)
                    }
               
                });
            });

    });
});