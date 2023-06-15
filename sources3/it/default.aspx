<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="it_default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT Request</title>
    <link href="../css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="divheader">
        Priority Change Request  / Enhancement Form
    </div>
        <img src="../div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Request No.
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txrequestno" runat="server"></asp:TextBox>
             </td>
        </tr>
    </table>
    </form>
</body>
</html>
