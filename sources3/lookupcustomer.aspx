<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcustomer.aspx.cs" Inherits="lookupcustomer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Customer</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form2" runat="server">
    <div>
    
    </div>
    <div>
        <asp:GridView ID="grd" runat="server" CssClass="mygrid" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:TemplateField HeaderText="Cust Code">
                    <ItemTemplate>
                        <a href="javascript:window.opener.RefreshData('<%# Eval("cust_cd") %>');window.close();"> <%# Eval("cust_cd") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust Name">
                    <ItemTemplate>
                        <%# Eval("cust_nm") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group">
                    <ItemTemplate><%# Eval("cusgrcd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Channel">
                    <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
