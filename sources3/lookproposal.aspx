<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookproposal.aspx.cs" Inherits="lookproposal" %>

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
        Lookup Proposal
    </div>
     <div>
        Proposal Status : <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
         Proposal Code : <asp:DropDownList ID="cbcode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList> 
         &nbsp;&nbsp;&nbsp;
         Year : <asp:DropDownList ID="cbYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
             <asp:ListItem Value="2018">2018</asp:ListItem>
             <asp:ListItem Value="2017">2017</asp:ListItem>
             <asp:ListItem Value="2016">2016</asp:ListItem>
             <asp:ListItem Value="2015">2015</asp:ListItem>
             <asp:ListItem Value="2014">2014</asp:ListItem>
                </asp:DropDownList> 
         &nbsp;&nbsp;&nbsp;
         Proposal No.<asp:TextBox ID="txsearchprop" runat="server" Width="20em"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
    <div>
           
    </div>
        <img src="div2.png" class="divid" />
        <div class="divgrid">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="90%" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True" AllowPaging="True" OnPageIndexChanging="grdprop_PageIndexChanging" PageSize="33" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Proposal Code">
                                <ItemTemplate><a href="javascript:window.opener.RefreshData('<%# Eval("prop_no") %>');window.close();"> <%# Eval("prop_no") %></a></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Proposal Reference"><ItemTemplate><%# Eval("prop_no_vendor") %></ItemTemplate></asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Principal (Vendor)"><ItemTemplate><%# Eval("vendor_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget"><ItemTemplate><%# Eval("budget_limit","{0:c}") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer"><ItemTemplate><%# Eval("customer") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Product"><ItemTemplate><%# Eval("product") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date"><ItemTemplate><%# Eval("start_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date"><ItemTemplate><%# Eval("end_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Status"><ItemTemplate><%# Eval("approval_nm") %></ItemTemplate></asp:TemplateField>
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
