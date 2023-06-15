<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_selectsalespoint.aspx.cs" Inherits="fm_selectsalespoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Salespoint</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="divheader" style="font-size:larger;font-family:Tahoma,Verdana">
        Please select salespoint for Data
    </div>
    <img src="div2.png" class="divid" />
    <div style="background-color:greenyellow;top:40%;left:30%;text-align:center;padding:5em 5em 5em 5em;font-family:Tahoma,Verdana;font-size:larger">
        Salespoint : <asp:DropDownList ID="cbosp" runat="server" Width="20em" OnSelectedIndexChanged="cbosp_SelectedIndexChanged"></asp:DropDownList>
        <asp:Button ID="btselect" runat="server" Text="SELECT" CssClass="button2 add" OnClick="btselect_Click" />
    </div>
        <img src="div2.png" class="divid" />
    </form>
</body>
</html>
