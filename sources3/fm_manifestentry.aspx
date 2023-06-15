<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_manifestentry.aspx.cs" Inherits="fm_manifestentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
       Trip Manifest
    </div>
    <img src="div2.png" class="divid"/>
    <div class="divgrid">
        <table>
            <tr>
                <td>Trip No</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtripno" runat="server" CssClass="makeitreadonly" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dttrip" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Driver Name
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbdriver" runat="server" Width="300px"></asp:DropDownList>    
                </td>
            </tr>
            <tr>
                <td>
                    Expedition Company</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbexpedition" runat="server" Width="300px"></asp:DropDownList>    
                </td>
            </tr>
            <tr>
                <td>
                    From</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbfrom" runat="server" Width="300px"></asp:DropDownList>    
                </td>
            </tr>
            <tr>
                <td>
                    To</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbto" runat="server" Width="300px"></asp:DropDownList>    
                </td>
            </tr>
            <tr>
                <td>
                    Trailer Box No.</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbtrailer" runat="server" Width="300px"></asp:DropDownList>    
                </td>
            </tr>
            <tr>
                <td>
                    Invoice No.</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbinvoice" runat="server" Width="300px"></asp:DropDownList>
                    <asp:Button ID="btsearchinv" runat="server" Text="Add Invoice" CssClass="button2 add" OnClick="btsearchinv_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <asp:GridView ID="grdinv" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Invoice No.">
                    <ItemTemplate>
                        <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("invoice_no") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice Date">
                    <ItemTemplate><%# Eval("invoice_dt","{0:dd-MMM-yyyy}")%></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btsearchinv" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print Manifest" CssClass="button2 print" style="left: 0px; top: 0px" OnClick="btprint_Click" />
    </div>
</asp:Content>

