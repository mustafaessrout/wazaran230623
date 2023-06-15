<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_shipmentho_received.aspx.cs" Inherits="fm_shipmentho_received" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        SHIPMENT RECEIVED
    </div>
    <img src="div2.png" class="divid" />
    <div>
        Search PO Status : <asp:DropDownList ID="cbpostatus" runat="server"></asp:DropDownList><asp:Button ID="btsearch" runat="server" Text="Search" />
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdpo" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanging="grdpo_SelectedIndexChanging" GridLines="Horizontal">
            <Columns>
                <asp:TemplateField HeaderText="PO NO">
                  <ItemTemplate>
                      <asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DATE">
                    <ItemTemplate><%# Eval("po_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VENDOR">
                    <ItemTemplate><%# Eval("vendor_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <%# Eval("po_sta_nm") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <SelectedRowStyle BackColor="#009999" />
        </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
                
    </div>
    <div class="divheader" style="padding:10px">
        SCHEDULING AND SHIPMENT RECEIVED
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdshipment" runat="server" AutoGenerateColumns="False" Width="100%" OnRowEditing="grdshipment_RowEditing">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <%# Eval("item_cd") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <%# Eval("item_nm") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Scheduled Date">
                    <ItemTemplate><%# Eval("shipment_dt") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty Order">
                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Received Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="dtreceived" runat="server" Width="75px"></asp:TextBox></EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Destination"></asp:TemplateField>
                <asp:CommandField EditText="ARRIVED" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdpo" EventName="SelectedIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
        
    </div>
</asp:Content>

