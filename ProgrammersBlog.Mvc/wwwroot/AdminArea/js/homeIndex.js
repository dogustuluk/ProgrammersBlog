$(document).ready(function () {
    //DataTable
    $('#articlesTable').DataTable({
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
        },
        //tarihe göre sıralama yapmak için
        // "order":[[4,"desc"]]
    });
    //DataTable


    //Chart.js
    const categories =
    [
            {
                name: 'C#',
                viewCount:'55583'
            },
            {
                name: 'C++',
                viewCount: '84241'
            },
            {
                name: 'Javascript',
                viewCount: '65700'
            },
            {
                name: 'Dart',
                viewCount: '6274'
            },
            {
                name: 'PHP',
                viewCount: '88750'
            },
            {
                name: 'TypeScript',
                viewCount: '10274'
            }
    ]

    let viewCountContext = $('#viewCountChart');
    let viewCountChart = new Chart(viewCountContext,
        {
            type: 'bar',
            data: {
                labels: categories.map(category => category.name),
                datasets: [
                    {
                        label: 'Okunma Sayısı',
                        data: categories.map(category => category.viewCount),
                        backgroundColor: ['#EC7272', '#F7A76C', '#E0D98C', '#C3FF99', '#293462','#D61C4E'],
                        hoverBorderWidth: 2,
                        hoverBorderColor: 'black'
                    }]
            },
            options: {
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                size:18
                            }
                        }
                    }
                }
            }
        });




    //Chart.js
})