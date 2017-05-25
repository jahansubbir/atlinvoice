$(document).ready(function () {
    var RootUrl = '@Url.Content("~/")';
    $("#EmployeeId").change(function () {
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
            //url:' @*@Url.Action("GetBills", "Conveyance")*@,
            url: RootUrl + '/Conveyance/GetBills',
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
                    //alert(rawDate);
                    // alert(epochDate);
                    var myDate = new Date(epochDate * 1);
                    //alert(value.Name);
                    //$("#EmpName").val(value.Name);
                    //$("#TotalAmount").val(value.Amount);
                    //alert(myDate);
                    var check;
                    var approve;
                    var pay;
                    if (value.Checked) {
                        check = "Y";
                    } else {
                        check = "N";
                    }
                    if (value.Approved) {
                        approve = "Y";
                    } else {
                        approve = "N";
                    }
                    if (value.Paid) {
                        pay = "Y";
                    } else {
                        pay = "N";
                    }
                    $("#tableBody").append('<tr><td>' + ' <input type="checkbox" id="Id" name="Id" value="' + value.Id + '"/>' + '</td><td>' + value.Name + '</td><td>' + value.EmpId + '</td><td>' + myDate.toDateString() + '</td><td>' + value.From + '</td><td>' + value.To + '</td><td>' + value.Project + '</td><td>' + value.VehicleBy + '</td><td>' + value.Amount + '</td><td>' + value.Remarks + '</td><td>' + value.Checked + '</td><td>' + value.Approved + '</td><td>' + value.Paid + '</td></tr>');
                });
                $("#myTable").dataTable(); /*.fnDestroy();*/
            }
        });
    });
    $("#DateFrom").change(function () {
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
            //url:' @*@Url.Action("GetBills", "Conveyance")*@,
            url: RootUrl + '/Conveyance/GetBills',
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
                    //alert(rawDate);
                    // alert(epochDate);
                    var myDate = new Date(epochDate * 1);
                    //alert(value.Name);
                    //$("#EmpName").val(value.Name);
                    //$("#TotalAmount").val(value.Amount);
                    //alert(myDate);
                    var check;
                    var approve;
                    var pay;
                    if (value.Checked) {
                        check = "Y";
                    } else {
                        check = "N";
                    }
                    if (value.Approved) {
                        approve = "Y";
                    } else {
                        approve = "N";
                    }
                    if (value.Paid) {
                        pay = "Y";
                    } else {
                        pay = "N";
                    }
                    $("#tableBody").append('<tr><td>' + ' <input type="checkbox" id="Id" name="Id" value="' + value.Id + '"/>' + +value.Name + '</td><td>' + value.EmpId + '</td><td>' + myDate.toDateString() + '</td><td>' + value.From + '</td><td>' + value.To + '</td><td>' + value.Project + '</td><td>' + value.VehicleBy + '</td><td>' + value.Amount + '</td><td>' + value.Remarks + '</td><td>' + value.Checked + '</td><td>' + value.Approved + '</td><td>' + value.Paid + '</td></tr>');
                });
                $("#myTable").dataTable(); /*.fnDestroy();*/
            }
        });
    });
    $("#DateTo").change(function () {
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
            //url:' @*@Url.Action("GetBills", "Conveyance")*@,
            url: RootUrl + '/Conveyance/GetBills',
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
                    //alert(rawDate);
                    // alert(epochDate);
                    var myDate = new Date(epochDate * 1);
                    //alert(value.Name);
                    //$("#EmpName").val(value.Name);
                    //$("#TotalAmount").val(value.Amount);
                    //alert(myDate);
                    var check;
                    var approve;
                    var pay;
                    if (value.Checked) {
                        check = "Y";
                    } else {
                        check = "N";
                    }
                    if (value.Approved) {
                        approve = "Y";
                    } else {
                        approve = "N";
                    }
                    if (value.Paid) {
                        pay = "Y";
                    } else {
                        pay = "N";
                    }
                    $("#tableBody").append('<tr><td>' + ' <input type="checkbox" id="Id" name="Id" value="' + value.Id + '"/>' + +value.Name + '</td><td>' + value.EmpId + '</td><td>' + myDate.toDateString() + '</td><td>' + value.From + '</td><td>' + value.To + '</td><td>' + value.Project + '</td><td>' + value.VehicleBy + '</td><td>' + value.Amount + '</td><td>' + value.Remarks + '</td><td>' + value.Checked + '</td><td>' + value.Approved + '</td><td>' + value.Paid + '</td></tr>');
                });
                $("#myTable").dataTable(); /*.fnDestroy();*/
            }
        });
    });
});