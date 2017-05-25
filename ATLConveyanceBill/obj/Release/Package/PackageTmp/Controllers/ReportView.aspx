<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="ATLConveyanceBill.Controllers.ReportView" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <%--<form id="form1" runat="server">--%>
    <div>
        <a href="Report.aspx">Report.aspx</a>
        <CR:CrystalReportViewer ID="Voucher" runat="server" AutoDataBind="true" />


    </div>
    <%--</form>--%>
    </div>
    </form>
</body>
</html>
