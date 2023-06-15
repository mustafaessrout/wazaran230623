<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupexhso.aspx.cs" Inherits="promotor_lookupexhso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Order No">
                    <ItemTemplate>
                        <asp:Label ID="lborderno" runat="server" Text='<%#Eval("exhso_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%#Eval("exhso_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exhibition">
                    <ItemTemplate><%#Eval("exhibit_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <HeaderStyle CssClass="header" />
            <PagerStyle CssClass="pager" />
            <RowStyle CssClass="rows" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
