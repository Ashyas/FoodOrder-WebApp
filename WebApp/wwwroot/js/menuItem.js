
var = dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/menuitem",
            "type":"GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "foodType.name", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div>
                                <a href="/Admin/MenuItems/Upsert?id=${data}"
                                    class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-fill"></i>
                                </a>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a onClick=Delete('/api/MenuItem'+${data})
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
            ajax({
                url: url,
                type: "DELETE",
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