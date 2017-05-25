$(document).ready(function () {
    $("#billForm").validate({
        rules: {
            //WorkDate: "required",
            Date: "required",
            ProjectId: "required",
            WorkSystemId: "required",
            WorkTypeId: "required",
            Description: "required",
            From: "required",
            To: "required",
            VehicleBy: "required",
            Amount: "required"

        },
        messages: {
            //WorkDate: "Select Date",
            Date: "Select Date",
            ProjectId: "Select Project",
            //WorkSystemId: "Select Work System",
            //WorkTypeId: "Select Work Type",
            //Description: "Work Description",
            From: "Source of Travel",
            To: "Destination of Travel",
            VehicleBy: "Vehicle(s)",
            Amount: "Amount Spent"

        }
    });
})