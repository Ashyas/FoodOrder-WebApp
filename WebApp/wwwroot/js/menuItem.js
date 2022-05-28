
var dataTable;

$(document).ready(function () {
    dataTable = $('#dt_load').DataTable({
        "ajax": {
            "url":"https://localhost:44334/api/menuitem/",
            "type":"GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                                <a href="/Admin/MenuItems/Upsert?id=${data}"
                                    class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-fill"></i>
                                </a>
                                <a onClick=Delete('/api/menuitem/'+${data})
                                    class="btn btn-danger mx-2">
                                    <i class="bi bi-trash3-fill"></i>
                                </a>
                            </div>`
                },
                "width": "20%"
            }
        ],
        "width":"100%"
    });
});


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failaiure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}