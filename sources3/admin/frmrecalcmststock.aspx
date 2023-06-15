<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmrecalcmststock.aspx.cs" Inherits="admin_frmrecalcmststock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ReCalculate Master Stock</title>
    <link href="../css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="divheader">
        ReCalculate Master Stock
    </div>
    <img src="div2.png" class="divid" />
    <div>
    <asp:Button ID="btsave" runat="server" Text="Calculate" Width="104px" OnClick="btsave_Click" CssClass="button2 save" style="left: 0px; top: 0px" />
    </div>
    </form>
</body>
</html>
