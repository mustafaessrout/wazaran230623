<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appitem.aspx.cs" Inherits="fm_appitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function refreshdata()
        {
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Approval New Item</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row divgrid">
            <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" CellPadding="1" GridLines="None" AllowPaging="True" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                <AlternatingRowStyle/>
                <Columns>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate><a href="javascript:popupwindow('lookup_item.aspx?item=<%# Eval("item_cd") %>');"> <%# Eval("item_nm") %></a></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Arabic">
                        <ItemTemplate><%# Eval("item_arabic") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate><%# Eval("size") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branded">
                        <ItemTemplate>
                            <asp:Label ID="lbbranded" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("item_sta_nm") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbstatus" runat="server" Width="100px"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
                <EditRowStyle CssClass="table-edit"/>
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page"/>
                <RowStyle />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div class="row navi">
            <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" style="display:none" />
        </div>
    </div>
    
     
    
</asp:Content>

