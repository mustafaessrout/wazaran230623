<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_paymentclaim2.aspx.cs" Inherits="fm_paymentclaim2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script>
       function openreport(url)
       {
           window.open(url, "myrep");
       }
   </script>
</head>
<body>

    <div class="container-fluid">
    <form id="form1" runat="server">

    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                    <ol class="breadcrumb">
                        <li><h3>Claim Payment (<asp:Label ID="lbclaim" runat="server"></asp:Label>)</h3></li>
                    </ol>
                </div>
            </div>

            <%-- List Claim to be paid --%>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <h4><label for="title" class="col-xs-12 col-form-label col-form-label-sm">List Claim Available to be Paid.</label></h4>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="grdclaim" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Detail">
                                <ItemTemplate>
                                    <asp:checkbox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect"></asp:checkbox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim No">
                                <ItemTemplate>
                                    <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCNR No">
                                <ItemTemplate>
                                    <asp:Label ID="lbccnr" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%# Eval("discount_mec") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Order">
                                <ItemTemplate>
                                    <%# Eval("discount_mec").ToString() == "FG" ? String.Format("{0:n} {1}", Eval("tot_order"), " Qty") : String.Format("{0:n} {1}", Eval("tot_order"), " Qty") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Free (SAR/Qty)">
                                <ItemTemplate>
                                    <%# Eval("discount_mec").ToString() == "FG" ? String.Format("{0:n} {1}", Eval("tot_free"), " Qty") : String.Format("{0:n} {1}", Eval("tot_free"), " SAR") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount (SAR)">
                                <ItemTemplate>
                                    <%# String.Format("{0:n} {1}", Eval("tot_payment"), " SAR")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Paid (SAR)">
                                <ItemTemplate>
                                    <%# String.Format("{0:n} {1}", Eval("tot_paid"), " SAR")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Different (SAR)">
                                <ItemTemplate>
                                    <%# String.Format("{0:n} {1}", Eval("tot_diff"), " SAR")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate><%# Eval("product") %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:DropDownList ID="status" runat="server">
                                    <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                    <asp:ListItem Value="UnPaid">UnPaid</asp:ListItem>
                                </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>  --%>              
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
            <div id="claimDetail" runat="server">
                <div class="row">
                    <div class="table-responsive">
                        <asp:GridView ID="grdclaimdtl" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Detail">
                                    <ItemTemplate>
                                        <asp:checkbox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectdtl"></asp:checkbox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate><asp:Label ID="product" runat="server" Text='<%# Eval("product") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("qtyorder"), "")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Qty">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("qtyfree"), "")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("disc_amt"), "")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unitprice">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("unitprice"), " SAR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Value">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("freevalue"), " SAR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Total Amount (SAR)">
                                <ItemTemplate>
                                    <%# String.Format("{0:n} {1}", Eval("tot_payment"), " SAR")%>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Paid (SAR)">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("tot_paid"), " SAR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Different (SAR)">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("tot_diff"), " SAR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>                                         
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="OnCheckedChanged" />
                </Triggers>--%>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
            <div id="claimDetailPay" runat="server">
                <div class="row">
                    <div class="table-responsive">
                        <asp:GridView ID="grdpayment" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Ref No">
                                    <ItemTemplate><asp:Label ID="lbref_no" runat="server" Text='<%# Eval("ref_no") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor Ref No">
                                    <ItemTemplate><asp:Label ID="lbvendor_ref_no" runat="server" Text='<%# Eval("vendor_ref_no") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate><%# Eval("pay_dt") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqtyorder" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("qtyorder"), "")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqtyfree" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("qtyfree"), "")%>'></asp:Label>     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdiscount" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("disc_amt"), "")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unitprice">
                                    <ItemTemplate>
                                        <asp:Label ID="lbunitprice" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("unitprice"), " SAR")%>'></asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lbfreevalue" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("freevalue"), " SAR")%>'></asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <%# String.Format("{0:n} {1}", Eval("amount"), " SAR")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField>
                                <ItemTemplate>          
                                    <%--<asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-default" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-default" OnClick="btnEdit_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>                             
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="col-md-2">
                                <label for="refNo" class="col-form-label col-form-label-sm">Vendor Ref No</label>
                                <asp:UpdatePanel ID="updPanelRefNoPay" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>                                
                                <asp:TextBox ID="txRefNoPay" runat="server" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>  
                            </div>
                            <div class="col-md-2">
                                <label for="refNo" class="col-form-label col-form-label-sm">Branch Ref No</label>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>                                
                                <asp:TextBox ID="txVendorRefNoPay" runat="server" CssClass="from-control"></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>  
                            </div>
                            <div class="col-md-2">
                                <label for="date" class="col-form-label col-form-label-sm">Payment Date</label>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>                                
                                <asp:TextBox ID="dtPay" runat="server" CssClass=""></asp:TextBox>
                                <asp:CalendarExtender ID="dtPay_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtPay">
                                </asp:CalendarExtender>
                                </ContentTemplate>
                                </asp:UpdatePanel>  
                            </div>
                            <div class="col-md-1">
                                <label for="btnAdd" class="col-form-label col-form-label-sm" >Action</label>
                                <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-defaul btn-sm" OnClick="btnadd_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="col-md-2">
                                <label for="orderQty" class="col-form-label col-form-label-sm">OrderQty</label>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>                                
                                <asp:TextBox ID="txOrderQty" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>                                    
                                </asp:UpdatePanel>  
                            </div>
                            <div class="col-md-2">
                                <label for="freeQty" class="col-form-label col-form-label-sm">FreeQty</label>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="txFreeQty" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                <label for="freeValue" class="col-form-label col-form-label-sm">FreeValue</label>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="txFreeValue" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                <label for="discount" class="col-form-label col-form-label-sm">Discount</label>
                                <asp:TextBox ID="txDiscount" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                <label for="unitPrice" class="col-form-label col-form-label-sm">UnitPrice</label>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbPrice" runat="server" Width="100%" OnSelectedIndexChanged="txAll_TextChanged" AutoPostBack="true"></asp:DropDownList> 
                                <%--<asp:TextBox ID="txPrice" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                <label for="Amount" class="col-form-label col-form-label-sm">Total Amount</label>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="txAmount" runat="server" CssClass=""></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txOrderQty" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txFreeValue" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txFreeQty" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="txDiscount" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cbPrice" EventName="SelectedIndexChanged" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="OnCheckedChanged" />
            </Triggers>--%>
            </asp:UpdatePanel>

            <%-- List Claim to be paid --%>

            <%--<hr style="width: 100%; color: black; height: 1px; background-color:black;" />

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="paymentDate" class="col-xs-4 col-form-label col-form-label-sm">Payment Date</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="dtpayment" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtpayment_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtpayment">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="date" class="col-xs-4 col-form-label col-form-label-sm">Date</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="dtcheque" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtcheque_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtcheque">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="bankHO" class="col-xs-4 col-form-label col-form-label-sm">BankHO</label>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="cbbankho" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="docNo" class="col-xs-4 col-form-label col-form-label-sm">Doc No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txdocno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="paymentType" class="col-xs-4 col-form-label col-form-label-sm">PaymentType</label>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="cbpaymnttype" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="dtdue" class="col-xs-4 col-form-label col-form-label-sm">Due Date</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="dtdue" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtdue_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtdue">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="bankCq" class="col-xs-4 col-form-label col-form-label-sm">BankCQ</label>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="cbbankcq" runat="server"></asp:DropDownList>
                        </div>
                    </div>                                                              
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="remarks" class="col-xs-2 col-form-label col-form-label-sm">Remarks</label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txremarks" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>--%>

            <hr style="width: 100%; color: black; height: 1px; background-color:black;" />
            
            <%-- List Payment --%>
            
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <h4><label for="title" class="col-xs-12 col-form-label col-form-label-sm">List Payment</label></h4>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="grddocument" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Claim Reference No">
                                <ItemTemplate>
                                    <asp:Label ID="lbclhno" runat="server" Text='<%# Eval("clh_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbdoctype" runat="server" Text='<%# Eval("doc_typ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbdate" runat="server" Text='<%# Eval("created_dt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document No">
                                <ItemTemplate>
                                    <asp:Label ID="lbdocno" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>
                                    <a class="example-image-link" href="/images/claim_doc/payment/<%# Eval("doc_file") %>" download data-lightbox="example-1<%# Eval("doc_file") %>">
                                    <asp:Label ID="lbfileloc" runat="server" Text='Preview'></asp:Label>
                                    </a></ItemTemplate>
                                <%--<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                            </asp:TemplateField>             
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <hr style="width: 100%; color: black; height: 1px; background-color:black;" />

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <h4><label for="title" class="col-xs-12 col-form-label col-form-label-sm">List VAT INVOICE</label></h4>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="grdvat" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowCommand="grdvat_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Inv No">
                                <ItemTemplate>
                                    <asp:Label ID="lbinvno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal No">
                                <ItemTemplate>
                                    <asp:Label ID="lbpropno" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbdate" runat="server" Text='<%# Eval("created_dt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate><%# Eval("total") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>                
                                    <asp:Button ID="btnPrint" runat="server" Text="Download" CssClass="btn btn-default" CommandName="print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <%--<ItemTemplate>
                                    <a class="example-image-link" href="/images/claim_doc/vat_inv/<%# Eval("fileinv") %>" download data-lightbox="example-1<%# Eval("doc_file") %>">
                                    <asp:Label ID="lbfileinv" runat="server" Text='Preview'></asp:Label>
                                    </a>
                                </ItemTemplate>--%>
                            </asp:TemplateField>             
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <hr style="width: 100%; color: black; height: 1px; background-color:black;" />

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <h4><label for="title" class="col-xs-12 col-form-label col-form-label-sm">Payment Information</label></h4>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="docType" class="col-xs-4 col-form-label col-form-label-sm">Bank List</label>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="cbbankcq" runat="server" ></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="docType" class="col-xs-4 col-form-label col-form-label-sm">Document Type</label>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="cbdoctype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbdoctype_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="file" class="col-xs-4 col-form-label col-form-label-sm">File</label>
                        <div class="col-xs-8">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <div class="form-group" runat="server" id="upl">
                            <asp:FileUpload ID="fup" runat="server" />
                            <p class="help-block"> Upload file GDN, CDV, Cheque </p>
                            </div>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbdoctype" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>                                                        
                </div>
            </div>


            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div class="row" runat="server" id="screenDN">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="debitNote" class="col-xs-4 col-form-label col-form-label-sm">Debit Note</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txdnno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                                                                
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="refNo" class="col-xs-4 col-form-label col-form-label-sm">Ref No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txrefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" runat="server" id="detailDN">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Account</label>
                        <asp:TextBox ID="txaccountDN" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Description</label>
                        <asp:TextBox ID="txdescriptionDN" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label>Qty</label>
                        <asp:TextBox ID="txqtyDN" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label>F.G. ByCash</label>
                        <asp:TextBox ID="txfgDN" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label>Value</label>
                        <asp:TextBox ID="txvalueDN" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label>Amount</label>
                        <asp:TextBox ID="txamountDN" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="col-md-1">
                    <label runat="server" id="lbAdd">Action</label>
                    <asp:LinkButton ID="btadditem" 
                                    runat="server" 
                                    CssClass="btn blue"    
                                    OnClick="btadd_Click">
                        <span aria-hidden="true" class="fa fa-plus-square"></span>Add
                    </asp:LinkButton>
                </div>--%>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbdoctype" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>


            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="remarks" class="col-xs-2 col-form-label col-form-label-sm">Remarks</label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txremarks" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <hr style="width: 100%; color: black; height: 1px; background-color:black;" />

            <div class="row">
                <div class="col-md-12">
                    <div class="form-actions text-center">                                
                        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn blue" OnClick="btsave_Click" />
                    </div>
                </div>
            </div>

        </div>

    </form>
    </div>
</body>
</html>
