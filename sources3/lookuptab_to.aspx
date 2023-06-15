<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookuptab_to.aspx.cs" Inherits="lookuptab_to" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body style="font-family:Calibri,Tahoma">
    <form id="form1" runat="server">
    <div class="divheader">
        Take Order Tablet Data
    </div>
        <img src="div2.png" class="divid" />
        <div class="divgrid">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="So No.">
                        <ItemTemplate><%# Eval("so_cd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%# Eval("so_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Code">
                        <ItemTemplate><%# Eval("cust_cd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Name">
                        <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman Code">
                        <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman Name"></asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
