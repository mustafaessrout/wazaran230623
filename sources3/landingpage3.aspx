<%@ Page Language="C#" AutoEventWireup="true" CodeFile="landingpage3.aspx.cs" Inherits="landingpage3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="align-content:center;width:100%;text-align:center;vertical-align:central">
            <div style="background-color:silver;padding:50px 50px 50px 50px;text-align:center;vertical-align:central;border:10px solid black;width:50%;float:inherit;margin-left:20%;margin-top:20%">
                <asp:Label ID="lbstatus" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0033CC"></asp:Label>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Timer ID="Timer1" runat="server" Interval="400" ontick="Timer1_Tick">
        </asp:Timer>
        <div>
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
                <ContentTemplate>
                    <asp:Label ID="lblReturnPage" runat="server" Visible  ="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>


</body>
</html>
