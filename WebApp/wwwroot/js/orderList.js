

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled");
    }
    else if (url.includes("completed")) {
        loadList("completed");
    }
    else if (url.includes("ready")) {
        loadList("ready");
    }
    else {
        loadList("inproccess");
    }
});


function loadList(param) {
    $('#orderList_load').DataTable({
        "ajax": {
            "url": "/api/order?status=" + param,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "pickupName", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpTime", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                                <a href="/Admin/Order/OrderDetails?id=${data}"
                                    class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-fill"></i>
                                </a>    
                            </div>`
                },
                "width": "20%"
            }
        ],
        "width": "100%"
    });
}