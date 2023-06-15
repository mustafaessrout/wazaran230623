<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstpricelevellist.aspx.cs" Inherits="fm_mstpricelevellist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader" style="font-size:16px">
        PRICE LEVEL SETUP
    </div>
    <img src="div2.png" class="divid" />
    <div>

        <asp:GridView ID="grdpricelevel" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grdpricelevel_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grdpricelevel_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Price Level Code">
                    <ItemTemplate>
                        <asp:Label ID="lbpricelevelcode" runat="server" Text='<%# Eval("pricelevel_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate><%# Eval("pricelevel_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer Type">
                    <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate><%# Eval("start_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate><%# Eval("end_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <HeaderStyle BackColor="#99FFCC" />
        </asp:GridView>
    </div>
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 add" OnClick="btnew_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btnew_Click" />
    </div>
</asp:Content>

