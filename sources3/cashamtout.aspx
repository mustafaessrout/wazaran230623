<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cashamtout.aspx.cs" Inherits="cashamtout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
 <style>
       #mydiv {
    position:fixed;
    top: 50%;
    left: 50%;
    width:40em;
    height:18em;
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
    <div>
        <div class="divheader">Cash Out Amount</div>
    </div>
        <img src="div2.png" class="divid" />
        <div id="mydiv">
        <table style="width:100%">
            <tr>
                <td>Amount Out </td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbamoutout" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                <td>Person In Charge</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbpic" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Paid for</td>
                <td>:</td>
                <td colspan="4">
                    <asp:Label ID="lbitemconame" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ref. No.</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbrefno" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1">
                    </td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1">
                    </td>
            </tr>
            <tr>
                <td colspan="6" class="auto-style2"><strong>Do you want to paid this ?</strong></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: right">
                    <asp:Button ID="btno" runat="server" Text="No" CssClass="button2 delete" OnClick="btno_Click" />
                </td>
                <td colspan="3">
                    <asp:Button ID="btyes" runat="server" Text="Yes" CssClass="button2 add" OnClick="btyes_Click" />
                </td>
            </tr>
        </table>
            </div>
    </form>
</body>
</html>
