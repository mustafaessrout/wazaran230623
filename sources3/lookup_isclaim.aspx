<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_isclaim.aspx.cs" Inherits="lookup_isclaim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="divheader">
        Claim To Head office Information
    </div>
        <img src="div2.png" class="divid" />
        <div class="divgrid">
            <table>
                <tr style="vertical-align:top"><td>Proposal No.</td><td>:</td><td>
                    <asp:DropDownList ID="cbproposal" runat="server" Width="20em"></asp:DropDownList></td></tr>
                <tr style="vertical-align:top"><td>Remark</td><td>:</td><td>
                    <asp:TextBox ID="txremark" runat="server" Rows="5" TextMode="MultiLine" Width="20em"></asp:TextBox>
                    </td></tr>
                <tr style="vertical-align:top"><td>&nbsp;</td><td>&nbsp;</td><td>
                    &nbsp;</td></tr>
            </table>
        </div>
        <div class="navi">
            <asp:Button ID="btcancel" runat="server" Text="Cancel" CssClass="button2 edit" />
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        </div>
    </form>
</body>
</html>
