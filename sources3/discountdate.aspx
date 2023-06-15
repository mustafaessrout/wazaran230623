<%@ Page Language="C#" AutoEventWireup="true" CodeFile="discountdate.aspx.cs" Inherits="discountdate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.3.1.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
     <style>
       #mydiv {
    position:fixed;
    top: 30%;
    left: 50%;
    width:40em;
    height:30em;
    margin-top: -9em; /*set to a negative number 1/2 of your height*/
    margin-left: -20em; /*set to a negative number 1/2 of your width*/
    border: 1px solid #ccc;
    background-color: #f3f3f3;
    padding:20px 20px 20px 20px;
}
     
     
     
        </style>
</head>
<body style="font-size:small;font-family:Verdana">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div id="mydiv">
        <div class="divheader">Update Discount Activated</div>
    <img src="div2.png" class="divid" />
        <table>
            <tr style="background-color:silver">
                <td>
                    Discount CD</td>
                <td>:</td>
                <td>
                             <asp:Label ID="lbdisccode" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                
            </tr>
            <tr style="background-color:silver">
                <td>Delivery Date</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbdelivery_dt" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr style="background-color:silver">
                <td>
                    Expire Date</td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="dtend_dt" runat="server" AutoPostBack="True"></asp:TextBox>

                    <asp:CalendarExtender ID="dtend_dt_CalendarExtender" runat="server" TargetControlID="dtend_dt" TodaysDateFormat="dd/M/yyyy">
                    </asp:CalendarExtender>

                </td>
                
            </tr>
            
            
            </table>
         <div class="navi">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
            <asp:Button ID="btclose" runat="server" CssClass="button2" OnClick="btclose_Click" Text="Close" />
        </div>
     </div>
        
    </form>
</body>
</html>
