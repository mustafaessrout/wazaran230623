<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupstockin.aspx.cs" Inherits="promotor_lookupstockin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass ="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Stock In No">
                    <ItemTemplate>
                        <asp:Label ID="lbstockinno" runat="server" Text='<%#Eval("stockin_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# Eval("stockin_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Source">
                    <ItemTemplate><%#Eval("salespoint_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Warehouse">
                    <ItemTemplate><%#Eval("whs_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bin">
                    <ItemTemplate><%#Eval("bin_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
