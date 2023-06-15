<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_drivertaken.aspx.cs" Inherits="fm_drivertaken" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <style>
      #mydiv {
            position:fixed;
            top: 5%;
            left: 5%;
            width:70%;
            height:70%;
            margin-top: -9em; set to a negative number 1/2 of your height
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
            border: 1px solid #ccc;
            background-color: #f3f3f3;
            vertical-align:central;
            text-align:center;
            font-size:xx-large;
            font-family:Calibri;
            }
       
        </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="divheader">Invoice Received by Driver</div>
        <img src="div2.png" class="divid" />
    <div style="font-family:Calibri">
        <table><tr><td>
            Driver
                   </td><td>
                       <asp:DropDownList ID="cbdriver" runat="server" Width="20em"></asp:DropDownList></td></tr>
            <tr>
                <td>Received By Driver Date</td><td>
                    <asp:TextBox ID="dtreceived" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtreceived_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtreceived">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td><td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btsave" runat="server" CssClass="button2 save" Text="Execute" OnClick="btsave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
