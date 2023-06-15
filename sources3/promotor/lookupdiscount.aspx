<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupdiscount.aspx.cs" Inherits="promotor_lookupdiscount" %>

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
                <asp:TemplateField HeaderText="Disc Code">
                    <ItemTemplate>
                        <asp:Label ID="lbdisccode" runat="server" Text='<%#Eval("disc_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description"><ItemTemplate><%#Eval("disc_desc") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Exhibition"><ItemTemplate><%#Eval("exhibit_nm") %></ItemTemplate></asp:TemplateField>
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
