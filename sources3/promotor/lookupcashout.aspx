<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcashout.aspx.cs" Inherits="promotor_lookupcashout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grd" CssClass="mydatagrid" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Cashout No">
                    <ItemTemplate>
                        <asp:Label ID="lbcashoutno" runat="server" Text='<%#Eval("cashout_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%#Eval("cashout_dt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark"><ItemTemplate><%#Eval("remark") %></ItemTemplate></asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
            </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
