<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcity.aspx.cs" Inherits="fm_mstcity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Master City</h3>
    <img src="line.gif" class="divid" />
    <div>
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" BorderStyle="None" GridLines="Horizontal" PageSize="15" OnSelectedIndexChanged="grd_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="City Code">
                    <ItemTemplate>
                        <asp:Label ID="lbcitycode" runat="server" Text='<%# Eval("city_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City Name">
                    <ItemTemplate><%# Eval("city_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arabic">
                    <ItemTemplate><%# Eval("city_arabic") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Region">
                    <ItemTemplate><%# Eval("region_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <SelectedRowStyle BackColor="#0066FF" />
        </asp:GridView>
    </div>
    <div class="navi">
        <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
    </div>
</asp:Content>

