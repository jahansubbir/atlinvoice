$(document).ready(function () {
    function getBills() {
        var billbydateUrl = $("#getBillByDateUrl").val();
        $("#tableBody").empty();
        var employeeId = $("#EmployeeId").val();
        var dateFrom = $("#DateFrom").val();
        var dateTo = $("#DateTo").val();
        var json = {
            empId: employeeId,
            dateFrom: dateFrom,
            dateTo: dateTo
        };
        $.ajax({
            type: "POST",
            url:billbydateUrl, 
                //'@Url.Action("BillByDate", "Conveyance")',

            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {

                $("#myTable").show();
                $.each(data, function (key, value) {
                    $("#TotalAmount").val(data.total);
                });
                $.each(data.bill, function (key, value) {
                    var rawDate = value.Date;
                    var epochDate = rawDate.substring(6, rawDate.length - 2);
                    var myDate = new Date(epochDate * 1);
                    $("#tableBody").append('<tr><td>' + ' <input type="checkbox" id="Id" name="Id" value="' + value.Id + '"/>' + '</td><td>' + value.Name + '</td><td>' + value.EmpId + '</td><td>' + myDate.toDateString() + '</td><td>' + value.From + '</td><td>' + value.To + '</td><td>' + value.Project + '</td><td>' + value.VehicleBy + '</td><td>' + value.Amount + '</td><td>' + value.Remarks + '</td><td>' + value.Checked + '</td><td>' + value.Approved + '</td><td>' + value.Paid + '</td></tr>');
                });
                $("#myTable").dataTable(); /*.fnDestroy();*/
            }
        });
    }
    $("#DateFrom").change(function () {
        getBills();
    });
    $("#DateTo").change(function () {
        getBills();
    });
    function getbill(parameters) {
        var getbillUrl = $("#getBillUrl").val();
        $("#tableBody").empty();
        var employeeId = $("#EmployeeId").val();
        var dateFrom = $("#DateFrom").val();
        var dateTo = $("#DateTo").val();
        var json = {
            empId: employeeId,
            dateFrom: dateFrom,
            dateTo: dateTo
        };
        $.ajax({
            type: "POST",
            url: getbillUrl,
            // url: RootUrl + '/Conveyance/GetBills',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {

                $("#myTable").show();
                $.each(data, function (key, value) {
                    $("#TotalAmount").val(data.total);
                });
                $.each(data.bill, function (key, value) {
                    var rawDate = value.Date;
                    var epochDate = rawDate.substring(6, rawDate.length - 2);

                    var myDate = new Date(epochDate * 1);

                    $("#tableBody").append('<tr><td>' + ' <input type="checkbox" id="Id" name="Id" value="' + value.Id + '"/>' + '</td><td>' + value.Name + '</td><td>' + value.EmpId + '</td><td>' + myDate.toDateString() + '</td><td>' + value.From + '</td><td>' + value.To + '</td><td>' + value.Project + '</td><td>' + value.VehicleBy + '</td><td>' + value.Amount + '</td><td>' + value.Remarks + '</td><td>' + value.Checked + '</td><td>' + value.Approved + '</td><td>' + value.Paid + '</td></tr>');
                });
                $("#myTable").dataTable(); /*.fnDestroy();*/
            }
        });
    }
    $("#EmployeeId").change(function () {
        getbill();
    });
    $("#DateFrom").change(function () {
        getbill();
    });
    $("#DateTo").change(function () {
        getbill();
    });
});