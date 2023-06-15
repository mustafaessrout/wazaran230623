<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appdomodif.aspx.cs" Inherits="fm_appdomodif" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .table{
            border:none !important;
        }
        .table caption{
            display:block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Delivery Order Modification Approval</div>
    <div class="h-divider"></div>
   
     <div class="container-fluid">
        <div class="row">
            <asp:GridView ID="grddo" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover mygrid" CellPadding="0" GridLines="None" Width="100%" Caption="DELIVERY ORDER" CaptionAlign="Left" OnSelectedIndexChanged="grddo_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="NO DATA FOUND" OnRowEditing="grddo_RowEditing" OnRowUpdating="grddo_RowUpdating" OnRowCancelingEdit="grddo_RowCancelingEdit">
                <AlternatingRowStyle  />
                <Columns>
                    <asp:TemplateField HeaderText="DO No.">
                        <ItemTemplate>
                            <asp:Label ID="lbdono" runat="server" Text='<%# Eval("do_no") %>' Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%# Eval("do_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO No.">
                        <ItemTemplate>
                            <asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>' ForeColor="Red" Font-Bold="True"></asp:Label>
                           </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To.">
                        <ItemTemplate><%# Eval("to_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate><%# Eval("to_addr") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%# Eval("do_sta_nm") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="Detail" ShowEditButton="True" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        
        <div class="h-divider"></div>
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                      <asp:GridView ID="grddodtl" runat="server" AutoGenerateColumns="False" Caption="DETAIL DELIVERY ORDER" CaptionAlign="Top" CssClass="table table-striped table-hover mygrid" ShowHeaderWhenEmpty="True" EmptyDataText="NO DATA FOUND">
                <Columns>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate><%# Eval("size") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branded">
                        <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Already Delivered">
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty Delivering">
                        <ItemTemplate>
                            <%# Eval("qty_deliver") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grddo" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

         <div class="h-divider"></div>
         <div class="row">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grdpo" runat="server" AutoGenerateColumns="False" Caption="Original PO From Branch" CaptionAlign="Left" CssClass="table table-striped table-hover mygrid" EmptyDataText="NO DATA FOUND">
                <Columns>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <%# Eval("item_cd") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# Eval("item_nm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Order">
                        <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
  
</asp:Content>

