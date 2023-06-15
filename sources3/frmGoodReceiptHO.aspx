<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmGoodReceiptHO.aspx.cs" Inherits="frmGoodReceiptHO" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_GoodReceiptHO.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function ItemSelectedTo_po_branch(sender, e) {
            $get('<%=hdto_po_branch.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
        function ItemSelecteditem(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script type = "text/javascript">
        function Confirm() {
            //if (!IsPostBack) {
                //var confirm_value = document.createElement("INPUT");
                //confirm_value.type = "hidden";
                //confirm_value.name = "confirm_value";
                //confirm_value.value = "";
                //if (confirm("Do you want to save data?")) {
                //    confirm_value.value = "Yes";
                //} else {
                //    confirm_value.value = "No";
                //}
                //document.forms[0].appendChild(confirm_value);
            //}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  class="divheader clearfix">
        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
                <p class="col-xs-6 no-padding">
                    Good Receipt HO
                </p>
                <div class="col-xs-6 no-padding-right">
                    <asp:DropDownList ID="cbsta_id" runat="server" Enabled="False" Font-Overline="False" CssClass="form-control input-sm pull-right" Width="170" >
                    </asp:DropDownList>
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">Receipt No.</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txreceipt_no" runat="server" CssClass="ro form-control" Enabled="false"></asp:TextBox>
                            <div class="input-group-btn">
                                 <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                            </div>
                        </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">Warehouse From</label>
                <div class="col-sm-10 drop-down">
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs_vt_cd" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">Receipt Date</label>
                <div class="col-sm-10 drop-down">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txreceipt_dt" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txreceipt_dt_CalendarExtender" runat="server" TargetControlID="txreceipt_dt" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">Warehouse To</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs_cd" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">Ref No.</label>
                <div class="col-sm-10 ">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txref_no" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">WH Bin</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbin_cd" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">Sources</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsource_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Value="P">From PO Branches</asp:ListItem>
                        <asp:ListItem Value="V">From Vendor</asp:ListItem>
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="col-sm-6 form-group clearfix">
                <label class="col-sm-2 control-label">PO Branch</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" class=" input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txsearchTo_po_branch" runat="server" CssClass="form-control" OnTextChanged="txsearchTo_po_branch_TextChanged" onchange = "Confirm()" AutoPostBack="True"></asp:TextBox>
                            <%--<asp:TextBox ID="txsearchTo_po_branch" runat="server" Width="250px" OnTextChanged="txsearchTo_po_branch_TextChanged"  AutoPostBack="True"></asp:TextBox>--%>
                            <asp:AutoCompleteExtender ID="txsearchTo_po_branch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txsearchTo_po_branch" ServiceMethod="GetCompletionListTo_po_branch" MinimumPrefixLength="1" 
                            EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedTo_po_branch" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <asp:HiddenField ID="hdto_po_branch" runat="server" ClientIDMode="Static" />

                            <div class="input-group-btn">
                                <asp:Button ID="btserachpo" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btserachpo_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
            </div>

        </div>

        <div class="row padding-top padding-bottom">
            <div class="col-xs-12 ">
                <table class="table mygrid row-no-padding">
                    <tr >
                        <th>Item Name</th>
                        <th>Quantity</th>
                        <th>UOM</th>
                        <th>Action</th>
                    </tr>
                    <tr>
                        <td style="min-width:150px;">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txsearchitemPnl">
                                        <asp:TextBox ID="txsearchitem" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txsearchitem" ServiceMethod="GetCompletionListitem" MinimumPrefixLength="1" 
                                        EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelecteditem" UseContextKey="True" CompletionListElementID="divi">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hditem" runat="server" />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="divi" class="auto-complate-list " style="display:none;"></div>
                        </td>
                        <td style="width:150px;">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txqtyPnl">
                                        <asp:TextBox ID="txqty" runat="server"  CssClass="form-control input-sm" type="number"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:150px;">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbuom" runat="server"  CssClass="form-control input-sm"  AutoPostBack="True">
                                    </asp:DropDownList>
                                     
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:50px;">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <strong>
                                    <asp:Button ID="btadd" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btadd_Click" Text="Add" />
                                    </strong>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            
        </div>

        <div class="row">
            <div class="divgrid col-xs-12" >
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="table table-striped table-hover mygrid" runat="server" AutoGenerateColumns="False" GridLines="None" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDataBound="grd_RowDataBound" ShowFooter="True" CellPadding="0">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Item CD Vendor">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitem_vendor_cd" runat="server" Text='<%# Eval("item_vendor_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtitem_vendor_cd" runat="server" Text='<%# Eval("item_vendor_cd") %>' CssClass="form-control input-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitem_nm" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                        <div style="text-align: right;">
				                        <asp:Label ID="lblTotalqty" runat="server" />
				                        </div>
			                        </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lbuom" runat="server" Text='<%# Eval("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                    <%--<EditItemTemplate>
                                        <asp:DropDownList ID="cboUOM" runat="server"  Width="90px"></asp:DropDownList>
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbreceipt_no" runat="server" Text='<%# Eval("receipt_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit"/>
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
         

        <div class="row navi padding-bottom padding-top margin-bottom">   
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
                    <asp:Button ID="btsave" runat="server" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" Text="Save" />
                    <asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn btn-delete divhid" OnClick="btDelete_Click" Text="Delete" />
                    <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click"/>
                </ContentTemplate>
            </asp:UpdatePanel>                  
        </div>
    </div>

   
    
</asp:Content>

