<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_selector.aspx.cs" Inherits="admin_fm_selector" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
            #mydiv {
            position:fixed;
            top: 50%;
            left: 50%;
            width:30em;
            height:18em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
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
    <div id="mydiv">
        Select Branches to be manage <br />
         <asp:DropDownList ID="cbsalespoint" runat="server" Width="20em"></asp:DropDownList>
        <asp:Button ID="btgo" runat="server" Text="GO" OnClick="btgo_Click" />
    </div>
   
    </form>
</body>
</html>
