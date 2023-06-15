<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_accManualJournal.aspx.cs" Inherits="fm_accManualJournal" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script type = "text/javascript" >

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <style>
        .hidobject{
            display:none;
        }
        </style>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_tranStock.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function Selecteditem(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    
 <script>
     function ShowProgress() {
         $('#pnlmsg').show();
     }

     function HideProgress() {
         $("#pnlmsg").hide();
         return false;
     }
     $(document).ready(function () {
         $('#pnlmsg').hide();
     });

    </script>
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width: 50px;
        height: 45px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
  }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Manual Journal</div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">

<%--                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Ref</label>
                    <div class="col-sm-9 ">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txtrnstkNo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-sm btn-primary" OnClientClick="openwindow();return(false);"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                             </ContentTemplate>
                             <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                        </asp:UpdatePanel>
                    
                    </div>
                </div>--%>

                <label class="control-label col-md-1">Journal Transaction Type</label>
                <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbJournalTranType" runat="server" AutoPostBack="True" OnTextChanged="cbjournaltrantype_SelectedIndexChanged" CssClass="auto-style3 form-control input-sm">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-md-1">Journal ID</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txJournalId" runat="server" CssClass="form-control" Height="100%" Readonly="True"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="False">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Transaction Date</label>
                <div class="col-md-2 drop-down-date">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dttrnstkDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dttrnstkDate_CalendarExtender" runat="server" TargetControlID="dttrnstkDate" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <br/>
            <div class="clearfix">
                <label class="control-label col-md-1">Module</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txmodule" runat="server" CssClass="form-control" Height="100%" value="MANUALJOURNAL" Readonly="true"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Description</label>            
                <div class="col-md-5">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txtrnstkRemark" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

    <%--            <div class="clearfix form-group col-sm-6">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="clmLbl" runat="server" Text="Claimed" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:CheckBox CssClass="checkbox no-margin-top" ID="chclaim" runat="server" Text="Claim" />
                                </div>
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </div>--%>
                <label class="control-label col-md-1">Journal Status</label>
                <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txJournalStatus" runat="server" CssClass="form-control" Height="100%" Enabled="False"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

<%--    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="h-divider"></div>
                        <div style="padding-bottom:10px;padding-top:10px">
                            <strong>Document Related</strong>
                        </div>
                        <div class="overflow-x">
                            <asp:GridView ID="grddoc" CssClass="table table-hover table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" PageSize="5" Width="100%" ForeColor="#333333" ShowFooter="True" OnRowCancelingEdit="grddoc_RowCancelingEdit" OnRowEditing="grddoc_RowEditing" OnRowUpdating="grddoc_RowUpdating">
                                <AlternatingRowStyle  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name">
                                        <ItemTemplate>
                                            <%# Eval("doc_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UploadImage">
                                    <ItemTemplate>
                                        <asp:Image ImageUrl='<%# Eval("filename") %>' runat="server" ID="image" /> 
                                        <a class="example-image-link" data-lightbox='example-1<%# Eval("filename") %>' href='/images/<%# Eval("filename") %>'>
                                        <asp:Label ID="lbfilename" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                        </a> 
                                    </ItemTemplate>
                                    <ItemTemplate>
                                        <a class="example-image-link" href="/images/<%# Eval("filename") %>" data-lightbox="example-1<%# Eval("filename") %>">
                                        <asp:Label ID="lbfilename" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" /> 
                                    </EditItemTemplate>
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
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="grddoc" /> 
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>--%>

    <!--jd row-->

    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
                <ContentTemplate>
                    <%--<div class="overflow-y" style="max-height:350px; width:100%;">--%>
                    <div class="overflow-y" style="width:100%;">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating"  CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Journal Det No.">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("id_jd") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dimensions">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <span>Bank: </span><asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("bank_cd") %>' ></asp:Label><span> </span><asp:Label ID="Label10" runat="server" Text='<%# Eval("bank_nm") %>' ></asp:Label><span>, </span> </br>                                 
                                        <span>Supplier: </span><asp:Label ID="Label1" runat="server" Text='<%# Eval("sup_cd") %>'></asp:Label><span> </span><asp:Label ID="Label11" runat="server" Text='<%# Eval("vendor_nm") %>' ></asp:Label><span>, </span> </br>
                                        <span>Salesman: </span><asp:Label ID="Label2" runat="server" Text='<%# Eval("salesman_cd") %>'></asp:Label><span> </span><asp:Label ID="Label12" runat="server" Text='<%# Eval("emp_nm") %>' ></asp:Label><span>, </span></br>
                                        <span>Customer: </span><asp:Label ID="Label3" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label><span> </span><asp:Label ID="Label13" runat="server" Text='<%# Eval("cust_nm") %>' ></asp:Label><span>, </span></br> 
                                        <span>Department: </span><asp:Label ID="Label4" runat="server" Text='<%# Eval("department_cd") %>'></asp:Label><span> </span><asp:Label ID="Label14" runat="server" Text='<%# Eval("dept_nm") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <span>Item: </span><asp:Label ID="Label5" runat="server" Text='<%# Eval("item_cd") %>' ></asp:Label><span> </span><asp:Label ID="Label15" runat="server" Text='<%# Eval("item_nm") %>' ></asp:Label><span>, </span> </br>
                                        <span>Quantity: </span><asp:Label ID="Label6" runat="server" Text='<%# Eval("item_count") %>'></asp:Label><span>, </span></br>
                                        <span>Unit of Measurement: </span><asp:Label ID="Label7" runat="server" Text='<%# Eval("item_uom") %>'></asp:Label><span>, </span></br>
                                        <span>Unit Price: </span><asp:Label ID="Label8" runat="server" Text='<%# Eval("item_unit_price") %>'></asp:Label><span>, </span></br>
                                        <span>Total Price: </span><asp:Label ID="Label9" runat="server" Text='<%# Eval("item_tot_price") %>'></asp:Label></br>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description Detail">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <%# Eval("description_jd") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <%# Eval("acct_cd") %><span> </span><asp:Label ID="Label16" runat="server" Text='<%# Eval("coa_nm") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("debit","{0:n2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                        <div style="text-align: right;">
				                        <asp:Label ID="lblTotalDebit" runat="server" Text='<%# Eval("tot_debit","{0:n2}") %>' />
                                            <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("tot_debit","{0:n2}") %>' CssClass="form-control input-sm" Width="70px"></asp:TextBox>--%>
				                        </div>
			                        </FooterTemplate>
                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("tot_debit","{0:n2}") %>' CssClass="form-control input-sm" Width="70px"></asp:TextBox>
                                    </EditItemTemplate>--%>                                     
                                </asp:TemplateField>
        
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("credit","{0:n2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                        <div style="text-align: right;">
				                        <%--<asp:Label ID="lblTotalAmount" runat="server" />--%>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text='<%# Eval("tot_credit","{0:n2}") %>'></asp:Label>
				                        </div>
			                        </FooterTemplate>
                                </asp:TemplateField>
               <%--                 <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrnstkNo" runat="server" Text='<%# Eval("tot_credit","{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                <%--             <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblsalespointCD" runat="server" Text='<%# Eval("salespoint_sn_jd") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>--%>

                                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />--%>
                                <asp:CommandField ShowDeleteButton="True"/>
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="table-copy-page-fix" id="copy-fst"></div>
        </div>
    </div>

    </br>

    <!--end of jd row-->
    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
        <ContentTemplate>                                 
            <asp:Button ID="btNewDetailRow" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btaddNewDetailRow_Click" Text="Add New Journal Detail Row" />                                     
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="newDetailRow" runat="server">
                      <table class="table table-striped mygrid table-title-left row-no-padding" style="background-color:lightblue">
                            <tr >
                                <th>Dimensions</th> 
                                <th>Product</th>
                                <th>Description Detail</th>
                                <th>Account</th>
                                <th>Debit</th>
                                <th>Credit</th>
                                <th>Action</th>
                            </tr>
                            <tr>
                                <td>
                                    <label class="control-label col-md-3">Bank</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:TextBox ID="txsearchbank" runat="server" AutoPostBack="True" OnTextChanged="txsearchaccount_TextChanged" CssClass="form-control input-sm"></asp:TextBox>--%>
                                                <asp:TextBox ID="txsearchbank" Tooltip="Bank" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                                 <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchbank_AutoCompleteExtender" runat="server" TargetControlID="txsearchbank" ServiceMethod="GetCompletionBankList"  MinimumPrefixLength="1" 
                                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                                 <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">Supplier</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:TextBox ID="txsearchsupplier" runat="server" AutoPostBack="True" OnTextChanged="txsearchaccount_TextChanged" CssClass="form-control input-sm"></asp:TextBox>--%>
                                                <asp:TextBox ID="txsearchsupplier" Tooltip="Supplier" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchsupplier_AutoCompleteExtender" runat="server" TargetControlID="txsearchsupplier" ServiceMethod="GetCompletionSupplierList"  MinimumPrefixLength="1" 
                                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">Employee / Salesman</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:TextBox ID="txsearchsalesman" runat="server" AutoPostBack="True" OnTextChanged="txsearchaccount_TextChanged" CssClass="form-control input-sm"></asp:TextBox>--%>
                                                <asp:TextBox ID="txsearchsalesman" Tooltip="Salesman" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchsalesman_AutoCompleteExtender" runat="server" TargetControlID="txsearchsalesman" ServiceMethod="GetCompletionSalesmanList"  MinimumPrefixLength="1" 
                                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/><br/>
                                    <label class="control-label col-md-3">Customer</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:TextBox ID="txsearchcustomer" runat="server" AutoPostBack="True" OnTextChanged="txsearchaccount_TextChanged" CssClass="form-control input-sm"></asp:TextBox>--%>
                                                <asp:TextBox ID="txsearchcustomer" Tooltip="Customer" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchcustomer_AutoCompleteExtender" runat="server" TargetControlID="txsearchcustomer" ServiceMethod="GetCompletionCustomerList"  MinimumPrefixLength="1" 
                                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">Department</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:TextBox ID="txsearchdepartment" runat="server" AutoPostBack="True" OnTextChanged="txsearchaccount_TextChanged" CssClass="form-control input-sm"></asp:TextBox>--%>
                                                <asp:TextBox ID="txsearchdepartment" Tooltip="Department" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchdepartment_AutoCompleteExtender" runat="server" TargetControlID="txsearchdepartment" ServiceMethod="GetCompletionDepartmentList"  MinimumPrefixLength="1" 
                                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>                                    
                                    <br/><br/><br/><br/><br/><br/><br/><br/>
                                    <%--<div id="divhid"></div>--%>
                                </td>
                                <td>
                                    <label class="control-label col-md-3">Item</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txsearchitem" Tooltip="Product Item" runat="server" AutoPostBack="True" OnTextChanged="txsearchitem_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                                                 <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchitem_AutoCompleteExtender" runat="server" TargetControlID="txsearchitem" ServiceMethod="GetCompletionItemList"  MinimumPrefixLength="1" 
                                                 EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True" >
                                                </asp:AutoCompleteExtender>
                                                 <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">Quantity</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txqty" Tooltip="Product Quantity" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txsearchitem_TextChanged2" type="Double"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">UOM</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cbUOM" Tooltip="Unit of Measurement" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbUOM_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">Unit Price</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txunitprice" Tooltip="Unit Price" runat="server" CssClass="form-control input-sm" Enabled="True" AutoPostBack="True" OnTextChanged="txsearchitem_TextChanged2" type="Double"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/><br/>
                                    <label class="control-label col-md-3">Total Price</label>
                                    <div class="col-md-9">
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtotprice" Tooltip="Total Price" runat="server" CssClass="makeitreadonly ro form-control input-sm" Enabled="False"  type="Double" align="right"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txdescription" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txsearchaccount" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                             <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchaccount_AutoCompleteExtender" runat="server" TargetControlID="txsearchaccount" ServiceMethod="GetCompletionAccountList"  MinimumPrefixLength="1" 
                                            EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                             <asp:HiddenField ID="hditem" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txdebit" value="0.00" runat="server" AutoPostBack="True" CssClass="form-control input-sm" type="Double"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txcredit" value="0.00" runat="server" AutoPostBack="True" CssClass="form-control input-sm" type="Double"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width:50px;">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                 
                                            <asp:Button ID="btadd" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btadd_Click" Text="Save Add Row" /></br></br>
                                            <asp:Button ID="btCancelAddRow" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btCancelAddRow_Click" Text="Cancel" />
                                     
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td> 
                            </td>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    
    <div class="container-fluid top-devider">
        <div class="navi row margin-bottom margin-top">
            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                <ContentTemplate>
                    <%--<asp:Button ID="btnew" runat="server" CssClass="btn-success btn btn-add" OnClick="btnew_Click" Text="NEW" />--%>
                    <asp:Button ID="btsave" runat="server" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" Text="Save and Send"/>
                    <%--<asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />--%>
                    <%--<asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" OnClick="btprint_Click" Text="Print" />--%>
                     <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                    <asp:Button ID="btapprove" runat="server" CssClass="btn-warning btn btn-save" OnClick="btapprove_Click" Text="Approve" OnClientClick="javascript:ShowProgress();"/>
                    <asp:Button ID="btcancel" runat="server" CssClass="btn-warning btn btn-save" OnClick="btcancel_Click" Text="Back to List" OnClientClick="javascript:ShowProgress();"/>
                    <asp:Button ID="btdelete" runat="server" CssClass="btn-warning btn btn-save" OnClick="btdelete_Click" Text="Delete"/>
                    <asp:Button ID="btreturn" runat="server" CssClass="btn-warning btn btn-save" OnClick="btreturn_Click" Text="Return"/>
                 
                </ContentTemplate>
             </asp:UpdatePanel>
        </div>
    </div>
  <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>

