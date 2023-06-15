<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupexhibition.aspx.cs" Inherits="lookupexhibition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False" PagerStyle-CssClass="pager" 
                    HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="true" OnSelectedIndexChanging="grd_SelectedIndexChanging" >
                <Columns>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lbexhibitcode" runat="server" Text='<%# Eval("exhibit_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exhibition Name">
                        <ItemTemplate><%# Eval("exhibit_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate><%# Eval("loc_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start">
                        <ItemTemplate><%# Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End">
                         <ItemTemplate><%# Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate>
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
