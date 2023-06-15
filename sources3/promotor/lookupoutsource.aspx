<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupoutsource.aspx.cs" Inherits="promotor_lookupoutsource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <style>
        .mydatagrid a {
    background-color: Transparent;
    padding: 5px 5px 5px 5px;
    text-decoration: none;
    font-weight: bold;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbidno" runat="server" Text='<%#Eval("idno") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type"><ItemTemplate><%#Eval("id_typname") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Name"><ItemTemplate><%#Eval("fullname") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Nationality"><ItemTemplate><%#Eval("nationality") %></ItemTemplate></asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="Blue" />
            </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
