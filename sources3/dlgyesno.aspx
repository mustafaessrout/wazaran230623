<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlgyesno.aspx.cs" Inherits="dlgyesno" %>

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
    /*background-color:  #f3f3f3;*/
    vertical-align:central;
    text-align:center;
    background-color:gray;
}
        </style>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body style="font-family:Verdana;font-size:large">
    <form id="form1" runat="server">
    <div id="mydiv">
        Do you want to execute ?
        <div class="navi">
        <asp:Button ID="btno" runat="server" Text="Cancel" CssClass="button2 delete" />
        <asp:Button ID="btyes" runat="server" Text="Yes" CssClass="button2 add" />
    </div>
    </div>
    
    </form>
</body>
</html>
