<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_shipmentho.aspx.cs" Inherits="fm_shipmentho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>

     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        PURCHASE ORDER - HEAD OFFICE
    </div>
    <img src="div2.png" class="divid" />
    <div>
        PO Status : <asp:DropDownList ID="cbpostatus" runat="server"></asp:DropDownList>
        <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="button2 search1" OnClick="btsearch_Click"/>
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
        ADD SCHEDULES
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" Width="100%" OnRowEditing="grddtl_RowEditing" OnRowCancelingEdit="grddtl_RowCancelingEdit" OnRowUpdating="grddtl_RowUpdating">
                <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty Order">
                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty deliver">
                        <ItemTemplate>
                            <%# Eval("qty_deliver") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txqty" runat="server" Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Shipment Date">
                    <ItemTemplate></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="dtshipment" runat="server" Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Container">
                    <ItemTemplate>

                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txcontainer" runat="server" Width="200px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Destination">
                    <ItemTemplate>

                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="cbdestination" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" UpdateText="Add To Schedule" />
            </Columns>
        </asp:GridView>
            </ContentTemplate>
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="grdpo" EventName="SelectedIndexChanging" />
             </Triggers>
        </asp:UpdatePanel>
       
    </div>
    <div class="divheader" style="padding:10px">
        SHIPMENT SCHEDULING
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdshipment" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <%# Eval("item_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipment Date">
                            <ItemTemplate><%# Eval("shipment_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipment No.">
                            <ItemTemplate><%# Eval("shipment_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Shipment">
                            <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Container">
                            <ItemTemplate><%# Eval("container_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Destination">
                            <ItemTemplate><%# Eval("dest_nm") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

