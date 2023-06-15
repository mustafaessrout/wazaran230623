<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mgtsales.aspx.cs" Inherits="fm_mgtsales" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Management Report (SALES)
    </div>
    <img src="div2.png" class="divid" />
    <div id="mydiv">
        <table>
            <tr>
                <td>Salespoint</td>
                <td>:</td>
                <td style="margin-left: 80px">
                    <asp:DropDownList ID="cbsp" runat="server" Width="10em"></asp:DropDownList>
                    <asp:CheckBox ID="chall" runat="server" AutoPostBack="True" Text="All Salespoint" />
                </td>
                <td style="margin-left: 80px" rowspan="3">
                    <asp:Button ID="btsearch" runat="server" Height="6em" Width="10em" CssClass="button2 search" />
                </td>
            </tr>
            <tr>
                <td>Start Date</td>
                <td>:</td>
                <td style="margin-left: 80px">
                    <asp:TextBox ID="dtstart" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>End Date</td>
                <td>:</td>
                <td style="margin-left: 80px">
                    <asp:TextBox ID="dtend" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 80px">
                    &nbsp;</td>
                <td style="margin-left: 80px">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Salesman"></asp:TemplateField>
                <asp:TemplateField HeaderText="Product Name"></asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name"></asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price"></asp:TemplateField>
                <asp:TemplateField HeaderText="Qty"></asp:TemplateField>
                <asp:TemplateField HeaderText="Total Amt"></asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

