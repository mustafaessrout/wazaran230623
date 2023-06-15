<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupdo.aspx.cs" Inherits="fm_lookupdo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <strong>Search DO</strong>
        <div>Search :&nbsp;
            <asp:TextBox ID="txsearch" runat="server"></asp:TextBox>
            <input id="btsearch" type="button" value="button" onclick="openwindow()"/></div>
    </div>
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px">
            <Columns>
                <asp:TemplateField HeaderText="DO Number">
                    <ItemTemplate><%# Eval("do_no") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DO Date">
                    <ItemTemplate><%# Eval("do_dt","{0:dd-MM-yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By"></asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
