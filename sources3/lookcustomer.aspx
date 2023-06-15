<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookcustomer.aspx.cs" Inherits="lookcustomer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="divheader">
        Lookup Customer (<asp:Label ID="lbsalespoint" runat="server" ForeColor="Red"></asp:Label>)
    </div>
     <div>
        Customer Group: <asp:DropDownList ID="cbgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
         Customer Outlet : <asp:DropDownList ID="cbotl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged"></asp:DropDownList> 
         &nbsp;&nbsp;&nbsp;
         Customer Name<asp:TextBox ID="txsearchcust" runat="server" Width="20em"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
    <div>
           
    </div>
        <img src="div2.png" class="divid" />
        <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="90%" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True" AllowPaging="True" OnPageIndexChanging="grdprop_PageIndexChanging" PageSize="20" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Customer Code">
                    <ItemTemplate><a href="javascript:window.opener.RefreshData('<%# Eval("cust_cd") %>');window.close();"> <%# Eval("cust_cd") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer Name"><ItemTemplate><%# String.Format("{0} ({1})", Eval("cust_nm"), Eval("cust_arabic")) %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Outlet"><ItemTemplate><%# Eval("otlcd") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Group"><ItemTemplate><%# String.Format("{0} - {1}", Eval("cusgrcd"), Eval("fld_desc")) %></ItemTemplate></asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
