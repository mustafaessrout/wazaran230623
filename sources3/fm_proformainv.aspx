<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_proformainv.aspx.cs" Inherits="fm_proformainv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }
        function CustomerSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function InvoiceSelected(dt) {
            $get('<%=hdinv.ClientID%>').value = dt;
            $get('<%=btlookinv.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdinv" runat="server" />
            <asp:HiddenField ID="hditem" runat="server" />
            <asp:HiddenField ID="hdcust" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="form-horizontal" style="font-family: Calibri">

        <h4 class="jajarangenjang">Request Special Order</h4>
        <div class="h-divider"></div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="salespoint" class="col-xs-4 col-form-label col-form-label-sm">Branch</label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txSalespoint" runat="server" CssClass="form-control input-sm" Font-Bold="true" ReadOnly="true" Enabled="false" ></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="lbstatus" class="col-xs-4 col-form-label col-form-label-sm">Status</label>
                    <div class="col-xs-8">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbstatus" runat="server" Text="NEW" CssClass="well well-sm no-margin danger text-white badge"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">                        
                <div class="form-group">
                    <label for="invno" class="col-xs-4 col-form-label col-form-label-sm">RSO No</label>
                    <div class="col-xs-8">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:Label ID="lbinvno" runat="server" CssClass="form-control input-sm" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearchinv" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchinv_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>            
            <div class="col-md-6">
                <div class="form-group">
                    <label for="dtCreated" class="col-xs-4 col-form-label col-form-label-sm">Date</label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="dtCreated" runat="server" CssClass="form-control input-sm"  ReadOnly="true" Enabled="false" ></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">                       
            <div class="col-md-6">
                <div class="form-group">
                    <label for="lbwh" class="col-xs-4 col-form-label-sm col-form-label">Warehouse</label>
                    <div class="col-xs-8">
                        <asp:DropDownList ID="cbwh" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbwarehouse_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-6">                        
                <div class="form-group">
                    <label for="lbbin" class="col-xs-4 col-form-label col-form-label-sm">Bin</label>
                    <div class="col-xs-8">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbin" runat="server" CssClass="form-control" >
                        </asp:DropDownList>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbwh" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div> 
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txcustomer" class="col-xs-4 col-form-label-sm col-form-label">Customer</label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txcustomer" runat="server" CssClass="form-control input-sm" AutoPostBack="True"></asp:TextBox>
                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCustomerList" TargetControlID="txcustomer" UseContextKey="True" OnClientItemSelected="CustomerSelected" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" >
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
            </div>
        </div>

        <h5 class="jajarangenjang">Details</h5>
        <div class="h-divider"></div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <table class="mGrid">
                        <tr>
                            <th>Item</th>                            
                            <th>Uom</th>
                            <th>Stock</th>
                            <th>Current Price</th>
                            <th>Qty</th>
                            <th>Price</th>
                            <th>Expiry Date</th>
                            <th>Add</th>
                        </tr>
                        <tr>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txitemnamePnl">
                                            <asp:TextBox ID="txitemname" runat="server" CssClass="form-control input-sm" AutoPostBack="True"></asp:TextBox>
                                            <div id="divwidthi"></div>
                                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitemname_AutoCompleteExtender" runat="server" ServiceMethod="GetItemList" TargetControlID="txitemname" UseContextKey="True" OnClientItemSelected="ItemSelected" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthi">
                                            </asp:AutoCompleteExtender>
                                        </asp:Panel>                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="cbuom_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <strong>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txstock" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdstock" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbuom" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txcurrentprice" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdcurrentprice" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbuom" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </strong>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txqtyPnl">
                                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>                         
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txprice" runat="server" CssClass="form-control input-sm" Enabled="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="dtExpiry" runat="server" CssClass="form-control input-sm" Enabled="true"></asp:TextBox>
                                        <asp:CalendarExtender ID="dtExpiry_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtExpiry">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" Text="Add" class="btn btn-primary btn-sm" OnClick="btadd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False" OnRowDeleting="grditem_RowDeleting" CssClass="table table-striped mygrid table-hover" CellPadding="0" ShowFooter="True">
                                <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %>.</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hditemlist" runat="server" Value='<%# Eval("item_cd") %>' />
                                        <asp:Label ID="lbitem" runat="server" Text='<%# Eval("item") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expiry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdatexp" runat="server" Text='<%# Eval("expiry_dt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Uom">
                                    <ItemTemplate>
                                        <asp:Label ID="lbuom" runat="server" Text='<%# Eval("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lbstock" runat="server" Text='<%# Eval("stock") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" type="number" Text='<%# Eval("qty") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lbprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txprice" runat="server" CssClass="form-control input-sm" type="number" Text='<%# Eval("unitprice") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                                <HeaderStyle CssClass="table-header" />
                                <FooterStyle CssClass="table-footer" />
                                <EditRowStyle CssClass="table-edit" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="text-align: center">
                <asp:Button ID="btnew" runat="server" Text="New" class="btn btn-success btn-sm" OnClick="btnew_Click" />
                <asp:Button ID="btedit" runat="server" Text="Edit" class="btn btn-warning btn-sm" OnClick="btedit_Click" />
                <asp:Button ID="btsave" runat="server" Text="Save" class="btn btn-primary btn-sm" OnClick="btsave_Click" />
                <asp:Button ID="btapprove" runat="server" Text="Approve" class="btn btn-primary btn-sm" OnClick="btapprove_Click" />
                <asp:Button ID="btreject" runat="server" Text="Reject" class="btn btn-danger btn-sm" OnClick="btreject_Click" />
                <asp:Button ID="btprint" runat="server" Text="Print" class="btn btn-danger btn-sm" OnClick="btprint_Click" />
                <asp:Button ID="btlookinv" runat="server" Text="Button" OnClick="btlookinv_Click" style="display:none"/>
            </div>
        </div>

    </div>

</asp:Content>

