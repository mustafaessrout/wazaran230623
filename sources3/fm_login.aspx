<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login.aspx.cs" Inherits="fm_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
        .modalBackground {
            background-color: Gray;
            background-image:url("toya.png");
            background-size:cover;
            filter: alpha(opacity=30);
            opacity: 0.8;}
        .modalPopup {
           /* background-color: #076DAB;*/
            background-color:#0026ff;
            /*color: #FFFFFF;*/
            opacity: 1;
            color:white;
            border-color: #000000;
            border-width: 1px;
            border-style: solid;
            border-radius:20px;
            text-align: center;
            vertical-align:central;
            cursor: move;
            margin-left:auto;
            margin-right:auto;
            margin-top:200px;
            margin-bottom:auto;
            font-family:Tahoma,Verdana;
            box-shadow: blue 10px 10px inset;
            font-size: medium;}
        a:visited {color:white;}
     </style>
</head>
<body class="modalBackground">
    <form id="form1" runat="server">
    <div style="align-content:center;text-align:center;margin-left:auto;margin-right:auto;margin-top:auto;height:100%;width:100%">
        <asp:Login ID="Login1" runat="server" Height="177px" Width="333px" CssClass="modalPopup" OnAuthenticate="Login1_Authenticate" BorderStyle="None"></asp:Login>
    </div>
    </form>
</body>
</html>
