<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmReceiptReturHO.aspx.cs" Inherits="frmReceiptReturHO" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
<link href="css/jquery-ui.css" rel="stylesheet" />
<script src="css/jquery-1.9.1.js"></script>
<script src="css/jquery-ui.js"></script>
<script>
       function Left(str, n){
    	if (n <= 0)
    	    return "";
    	else if (n > String(str).length)
    	    return str;
    	else
    	    return String(str).substring(0,n);
    }
     
    function Right(str, n){
        if (n <= 0)
           return "";
        else if (n > String(str).length)
           return str;
        else {
           var iLen = String(str).length;
           return String(str).substring(iLen, iLen - n);
        }
    }
    function Len(string_variable) {

        return string_variable.length;

    }
    function Search(str,p) {
        return str.search(p);
    }
</script>
<script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_receiptReturHO.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function Selectedreturho_no(sender, e) {
            $get('<%=hdreturho_no.ClientID%>').value=Left(e.get_value(),Search(e.get_value(),'#'));
            $get('<%=hdsalespointcd.ClientID%>').value = Right(e.get_value(), Len(e.get_value()) - Search(e.get_value(), '#')-1);
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  class="divheader">Receipt Retur HO</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix ">
                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Rec Retur No.</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txrecRetHO_no" runat="server" CssClass="makeitreadonly ro form-control" ReadOnly="True" Enabled="false"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                                </div>
                            </ContentTemplate>
                            <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                        </asp:UpdatePanel>
                        <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                    </div>
                </div>
                
                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Return Branch No.</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txreturho_no" runat="server" AutoPostBack="true" OnTextChanged="txreturho_no_TextChanged" CssClass="form-control"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txreturho_no_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txreturho_no" ServiceMethod="GetCompletionListreturho_no" MinimumPrefixLength="1" 
                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selectedreturho_no" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hdreturho_no" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdsalespointcd" runat="server" ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Manual No.</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txrecRetHO_manual_no" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">SalesPointCD</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control" Enabled="False" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Date</label>
                    <div class="col-sm-10 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txrecRetHO_dt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="txrecRetHO_dt_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="txrecRetHO_dt" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Warehouse IN</label>
                    <div class="col-sm-10 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbwhs_cd" runat="server" OnSelectedIndexChanged="cbwhs_cd_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Return Type</label>
                    <div class="col-sm-10 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbrecRetHO_type" runat="server" OnSelectedIndexChanged="cbreturho_type_SelectedIndexChanged" CssClass="makeitreadonly ro form-control" Enabled="False">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Bin</label>
                    <div class="col-sm-10 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbin_cd" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>

                <div class="margin-bottom clearfix col-sm-6">
                    <label class="control-label col-sm-2">Remark</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txrecRetHO_note" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
   
                    </div>
                </div>

            </div>

        </div>
    </div>

    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row margin-bottom padding-bottom">
            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-hover mygrid" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" CellPadding="0"  GridLines="None" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="txseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                <%# Eval("item_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate>
                                        <asp:Label ID="txbranded_nm" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="size">
                                    <ItemTemplate>
                                        <asp:Label ID="txsize" runat="server" Text='<%# Eval("size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label ID="lblTotalqty" runat="server" />
				                    </div>
			                        </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                <%# Eval("uom") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                <%# Eval("RetHO_price") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                         <asp:Label ID="lblRetHO_Amount" runat="server" Text='<%# Eval("RetHO_Amount","{0:n2}") %>'></asp:Label>
                                         </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label ID="lblTotalAmount" runat="server" />
				                    </div>
			                        </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expire Date">
                                    <ItemTemplate>
                                <%# Eval("exp_dt","{0:d/M/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                <%# Eval("item_status") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="qty ship">
                                <ItemTemplate>
                                    <div style="text-align: right;">
                                         <%--<asp:Label ID="lbqty_ship" runat="server" Text='<%# Eval("qty_ship","{0:n2}") %>'></asp:Label>--%>
                                        <asp:Label ID="lbqty_ship" runat="server" Text='<%# Eval("qty_ship") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqty_ship" runat="server" Text='<%# Eval("qty_ship") %>' CssClass="form-control input-sm" ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label ID="lblTotalqty_ship" runat="server" />
				                    </div>
			                        </FooterTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbIDS" runat="server" Text='<%# Eval("IDS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle  />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>  
        
        <div class="row navi margin-bottom">
            <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btsave" runat="server" CssClass="btn-warning btn bnt-save" OnClick="btsave_Click" Text="Save" />           
            <asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
            
    </div>


    
</asp:Content>

