<%@ Page Language="C#" AutoEventWireup="true" CodeFile="alert_denied.aspx.cs" Inherits="alert_denied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server"><br /><br /><br />
    <div style="padding:10px 10px 10px 10px;text-align:center;border:1px solid blue;font-family:Calibri">
        <div>
            <asp:Label ID="lbalert" runat="server"></asp:Label></div>
        <div>Please contact Wazaran User via WhatsApp Group</div>
    </div>
        <div style="width:100%;text-align:center">
            <asp:Button ID="btdef" runat="server" Text="OK" OnClick="btdef_Click" style="text-align: left" />
            <asp:Button ID="btclosingdaily" runat="server" OnClick="btclosingdaily_Click" Text="Closing Daily" />
            <asp:Button ID="btclosingmonthly" runat="server" OnClick="btclosingmonthly_Click" Text="Closing Monthly" />
            <asp:Button ID="btclosingyearly" runat="server" OnClick="btclosingyearly_Click" Text="Closing Yearly" />
        </div>
    </form>
</body>
</html>
