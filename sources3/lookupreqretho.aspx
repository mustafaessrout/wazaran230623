<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupreqretho.aspx.cs" Inherits="lookupreqretho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Request</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Content/jquery.min.js"></script>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Request Date">
                        <ItemTemplate><%#Eval("retur_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prod Spv">
                        <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate><%#Eval("amt") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%#Eval("reqretho_sta_nm") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="text-align: center">
            <asp:LinkButton ID="btclose" CssClass="btn btn-danger" OnClientClick="javascript:window.close();" runat="server">Close</asp:LinkButton>
        </div>
    </form>
</body>
</html>
