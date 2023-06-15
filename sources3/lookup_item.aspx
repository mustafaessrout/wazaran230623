<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_item.aspx.cs" Inherits="lookup_item" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Information</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Calibri,Tahoma">
        <div class="divheader">Item Detail</div>
        <img src="div2.png" class="divid" />
        <div class="divgrid">
            <table style="width:100%">
                <tr><td>Item Code</td><td>:</td><td style="margin-left: 40px">
                    <asp:Label ID="lbitemcode" runat="server" Text="lbitemcode" Font-Bold="True" ForeColor="Blue"></asp:Label></td><td>
                        Item Name</td><td>
                        :</td><td>
                        <asp:Label ID="lbitemname" runat="server" Text="Label" Font-Bold="True" ForeColor="Blue"></asp:Label>
                    </td><td>
                        Arabic</td><td>
                        :</td><td>
                        <asp:Label ID="lbarabic" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td></tr>
                <tr><td>Size</td><td>:</td><td style="margin-left: 40px">
                    <asp:Label ID="lbsize" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td><td>
                        Branded</td><td>
                        :</td><td>
                        <asp:Label ID="lbbranded" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td><td>
                        UOM Base</td><td>
                        :</td><td>
                        <asp:Label ID="lbuom" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td></tr>
                <tr><td>Vendor Price</td><td>:</td><td style="margin-left: 40px">
                        <asp:Label ID="lbvendorprice" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td><td>
                        Vendor</td><td>
                        :</td><td>
                        <asp:Label ID="lbvendorname" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td><td>
                        Packing</td><td>
                        :</td><td>
                        <asp:Label ID="lbpacking" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    </td></tr>
                <tr><td>&nbsp;</td><td>&nbsp;</td><td style="margin-left: 40px">
                    &nbsp;</td><td>
                        &nbsp;</td><td>
                        &nbsp;</td><td>
                        &nbsp;</td><td>
                        &nbsp;</td><td>
                        &nbsp;</td><td>
                        &nbsp;</td></tr>
            </table>
            <img src="div2.png" class="divid" />
            
            <div class="divheader">Target Salespoint And Target Price per Channel</div>
            <table style="width:80%;padding-right:10px;padding-top:10px"><tr><td>
            <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grdsp_PageIndexChanging" Width="90%">
                <Columns>
                    <asp:TemplateField HeaderText="Salespoint Code">
                        <ItemTemplate><%# Eval("salespointcd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salespoint Name">
                        <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
       
            </td><td style="vertical-align:top">
                <asp:GridView ID="grdprice" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Customer Type">
                            <ItemTemplate><%# Eval("cust_typ") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effective Date">
                            <ItemTemplate><%# Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
            </td></tr></table>
       </div>
    </div>
        <img src="div2.png" class="divid" />
    <div class="navi">
        <strong><span class="auto-style1">Do you want to Approve ?</span></strong> <asp:Button ID="btapp" runat="server" Text="Yes" CssClass="button2 save" OnClick="btapp_Click" /> &nbsp;<asp:Button ID="btclose" runat="server" Text="CANCEL" CssClass="button2 delete" OnClick="btclose_Click" /></div>
    </form>
</body>
</html>
