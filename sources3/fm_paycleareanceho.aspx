<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paycleareanceho.aspx.cs" Inherits="fm_paycleareanceho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function RefreshData() {
            $get('<%=btrefresh.ClientID%>').click();
            sweetAlert('Clearance Or Rejected has been completed', '', 'success'); return (false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Bank Transfer/Cheque Cleareance - Head Office</div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Salespoint">
                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>
                        <%# Eval("cust_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cheque/Bank Trf No">
                    <ItemTemplate><a href="javascript:popupwindow('bankcleareanceho.aspx?id=<%# Eval("deposit_id") %>');"> <%# Eval("deposit_no") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doc Date">
                    <ItemTemplate><%# Eval("deposit_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amt">
                    <ItemTemplate><%# Eval("amt","{0:F2}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date">
                    <ItemTemplate><%# Eval("clear_dt","{0:d/M/yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bank"></asp:TemplateField>
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
    <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" CssClass="divhid" />
    <img src="div2.png" class="divid" />
</asp:Content>

