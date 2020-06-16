
function GetIVRLogs(name, pageNumber, pageSize) {

    var settings = {
        "url": "https://localhost:44384/api/Report/GetIVRLogs",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(
            {
                "name": name,
                "pageNumber": pageNumber,
                "pageSize": pageSize
            }
        ),
    };

    $.ajax(settings).done(function (response, textStatus, jqXHR) {
        console.log("response", response);

        if (jqXHR.status == 200) {
            var data = response.data;
            var items = ``;
            $.each(data, function (index, value) {
                items +=
                    `
                    <tr>
                        <td width="5%" scope="row">${ ((response.pageIndex - 1) * 10) + index + 1}</td>
                        <td width="15%"> ${value.id} </a></td>
                        <td>${value.value1}</td>
                        <td>${value.value2}</td>
                        <td>${value.value3}</td>
                        <td width="15%">
                            <a class="btn-circle btn-sm btn-success" href=/Events/Edit/${value.id}>แก้ไข</a>
                        </td>
                    </tr>
                    `
            });

            $("#tblResults >tbody").html(items);
            $("#tblResults >tbody").html(items);
            $("#lblPageIndex").html(response.pageIndex + " / " + response.totalPages);
            $("[name='PageNumber']").val(response.pageIndex);

            GenerateTotalItems(response.totalItems, "lblTotalItems");
            GeneratePaginationHtml(pageNumber, response.totalPages, "ulPagination");
        }
    });

}


function GenerateTotalItems(totalItems, forId) {
    $("#" + forId).html(totalItems);
}

function GeneratePaginationHtml(pageNumber, totalPages, forId) {

    var url = window.location.pathname;
    var urlParam = new URLSearchParams(window.location.search);
    urlParam.delete("PageNumber");

    for (var pair of urlParam.entries()) {
        url += `&${pair[0]}=${pair[1]}`;
    }


    var pageNumber = parseInt(pageNumber == null ? 1 : pageNumber);

    var start = 1;
    var end = totalPages;
    if ((pageNumber + 5) < end) {
        end = pageNumber + 5;
    }
    start = end - 9;
    if (start < 1) {
        start = 1;
        end = Math.min(10, totalPages);
    }

    console.log("pageNumber:" + pageNumber + " totalPages:" + totalPages);
    console.log("start:" + start + " end:" + end);

    var items_1 = "";
    for (var i = start; i <= end; i++) {
        var urlWithPageNumber = (url + "&PageNumber=" + i).replace("&", "?");
        items_1 += `<li class="page-item ${i == pageNumber ? "active" : ""}"><a class="page-link" href="${urlWithPageNumber}">${i}</a></li>`;
    }


    if (totalPages > 0) {
        $("#" + forId).append(`<li class="page-item"><a class="page-link" href="${(url + "&PageNumber=" + 1).replace("&", "?")}"> |< </a></li>`);
    }

    $("#" + forId).append(items_1);

    if (totalPages > 0) {
        $("#" + forId).append(`<li class="page-item"><a class="page-link" href="${(url + "&PageNumber=" + totalPages).replace("&", "?")}"> >| </a></li>`);
    }
}


$(document).ready(function () {

    console.log("start");

    var urlParam = new URLSearchParams(window.location.search);
    var p1 = urlParam.get("Name");
    var p2 = urlParam.get("PageNumber");

    if (p1 != null) {
        $("[name='Name']").val(p1);
    }

    GetIVRLogs(p1, parseInt(p2), 10);
});


