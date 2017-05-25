<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ATLConveyanceBill.Controllers.Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/_Layout.cshtml" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #reportViewFrom {
            height: 191px;
        }
        .auto-style1 {
            width: 77px;
        }
        .auto-style2 {
            width: 132px;
        }


    </style>

</head>
<body>
    <link href="../Content/css/bd.css" rel="stylesheet" />
    <div>
       
        <form id="reportViewFrom" runat="server">
            <table style="height: 148px; margin-top: 32px">
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="SelectLabel" runat="server" Text="Select All"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:CheckBox runat="server" ID="CheckAllCheckBox" Text="All" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="FromDateLabel" runat="server" Text="Date From"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="FromDateTextBox" runat="server" Height="24px" Style="margin-left: 0px" Width="141px"></asp:TextBox></td>

                    <td class="auto-style2"></td>
                    <td>
                        <asp:CheckBox ID="CheckedCheckBox" runat="server" Text="Unchecked" /></td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="ToDateLabel" runat="server" Text="Date To"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="ToDateTextBox" runat="server" Height="24px" Style="margin-left: 0px" Width="141px" ></asp:TextBox>
                    </td>
                    <td class="auto-style2"></td>
                    <td>
                        <asp:CheckBox ID="ApprovedCheckBox" runat="server" Text="Denied" /></td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="EmployeeLabel" runat="server" Text="Employee"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="EmployeeDropDownList" runat="server" Height="30px" Style="margin-left: 0px; margin-top: 0px;" Width="153px"   >
                            <asp:ListItem Text="Select" Selected="False"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="auto-style2"></td>
                    <td>
                        <asp:CheckBox ID="PaidCheckBox" runat="server" Text="Unpaid" /></td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                 
                    <td>
                        <asp:Button ID="ReportButton" runat="server" Text="Report" OnClick="ReportButton_Click" OnClientClick="document.forms[0].target = '_blank';"/></td>
                    <td class="auto-style2"></td>
                       <td></td>
                </tr>
                 <tr>
                    <td class="auto-style1"></td>
                 
                    <td>
                        <asp:Button ID="VoucherButton" runat="server" Text="Voucher" OnClick="VoucherButton_Click" OnClientClick="document.forms[0].target = '_blank';"/></td>
                    <td class="auto-style2"></td>
                       <td></td>
                </tr>
            </table>


        </form>
    </div>
    <link href="../Content/jquery-ui.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.1.1.min.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/jquery.validate.min.js"></script>
    <script src="../Scripts/jquery.validate.unobtrusive.min.js"></script>
   <script>
       $(function () {
           $("#FromDateTextBox").datepicker({ dateFormat: 'dd-M-yy' });
           $("#ToDateTextBox").datepicker({ dateFormat: 'dd-M-yy' });
           //$("#DateFrom2").datepicker({ dateFormat: 'dd-M-yy' });
           //$("#DateTo2").datepicker({ dateFormat: 'dd-M-yy' });
       });
   </script>
   <%-- <script>
        $(document).ready(function() {
            $("#EmployeeDropDownList").append('<option>Select</option');
        })
        
    </script>--%>
  
</body>
</html>
