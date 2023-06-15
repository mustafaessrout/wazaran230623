<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentlist.aspx.cs" Inherits="fm_paymentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Payment List
    </div>
    <img src="div2.png" class="divid" />
    <div style="padding:10px 10px 10px 10px">
        Payment Status : <asp:DropDownList ID="cbstatus" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" AutoPostBack="True" Width="300px"></asp:DropDownList>
    </div>
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="90%" AllowPaging="True" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Payment No.">
                    <ItemTemplate>
                        <asp:Label ID="lbpaymentno" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                       </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# Eval("payment_dt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salesman">
                    <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salespoint">
                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate><%# Eval("totamt","{0:#,0##.##}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Type">
                    <ItemTemplate><%# Eval("payment_typ_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("payment_sta_nm") %>'></asp:Label></ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:CommandField ShowSelectButton="True" ShowEditButton="True" />
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
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 add" OnClick="btnew_Click" />
    </div>
</asp:Content>

