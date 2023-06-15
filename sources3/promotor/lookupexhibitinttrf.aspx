<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupexhibitinttrf.aspx.cs" Inherits="promotor_lookupexhibitinttrf" %>

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
                <asp:TemplateField HeaderText="Trf No">
                    <ItemTemplate>
                        <asp:Label ID="lbtrfno" runat="server" Text='<%#Eval("trfno") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%#Eval("trf_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trf Type">
                    <ItemTemplate><%#Eval("trftyp_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Depo">
                    <ItemTemplate><%#Eval("exhibit_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sect">
                    <ItemTemplate><%#Eval("prod_nm") %></ItemTemplate>
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
