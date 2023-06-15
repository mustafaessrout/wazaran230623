<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_subtakeorder.aspx.cs" Inherits="fm_subtakeorder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="container">
        <div class="form-horizontal">
        <h3>Copy Order from Take Order</h3>
        <div class="h-divider"></div>
        <div class="form-group" style="background-color:cadetblue">
            <label class="control-label col-md-1">No:</label>
            <div class="col-md-2">
                <asp:Label ID="lbtoparent" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
            </div>
            <label class="control-label col-md-1">Cust:</label>
            <div class="col-md-2">
                <asp:Label ID="lbcust" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
            </div>
             <label class="control-label col-md-1">Group:</label>
            <div class="col-md-2">
                <asp:Label ID="lbgroup" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
            </div>
             <label class="control-label col-md-1">Type:</label>
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
            </div>
        </div>
            <div class="form-group" style="background-color:cadetblue">
                <label class="control-label col-md-1">Date:</label>
                <div class="col-md-2">
                    <asp:Label ID="dtorder" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
                </div>
                 <label class="control-label col-md-1">Slsman:</label>
                <div class="col-md-2">
                    <asp:Label ID="lbsalesman" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
                </div>
                <label class="control-label col-md-1">Manual:</label>
                <div class="col-md-2">
                    <asp:Label ID="lbmanual" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
                </div>
                <label class="control-label col-md-1">Whs:</label>
                <div class="col-md-2">
                    <asp:Label ID="lbhws" runat="server" Text="Label" CssClass="form-control input-sm"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <asp:GridView ID="grddiscount" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="mygrid" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Disc Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Method">
                                <ItemTemplate><%# Eval("discount_mec") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate><%# Eval("free_qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate><%# Eval("free_cash") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="h-divider"></div>
             <h4>Order Details</h4>
            <div class="form-group">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="mygrid" Width="100%" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                 <asp:Label runat="server" ID="lbitemcode" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txitem" runat="server" Width="100%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txitem" ServiceMethod="GetCompletionList2" UseContextKey="true" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1"></asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hditem" runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branded">
                            <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Ship">
                            <ItemTemplate>
                                <asp:Label ID="lbqtyshipment" runat="server" Text='<%# Eval("qty_shipment") %>'></asp:Label></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotqtyshipment" runat="server" Text="0"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("unitprice","{0:F2}") %>'></asp:Label> </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotunitprice" runat="server" Text="Label"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock Cust">
                            <ItemTemplate><%# Eval("stock_cust") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock Avl">
                            <ItemTemplate><%# Eval("stock_amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        
        </div>
        
    </div>
    <div class="navi">
    </div>
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save btn-default" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" CssClass="btn-default button2 print" Text="Print" />
    </div>
</asp:Content>

