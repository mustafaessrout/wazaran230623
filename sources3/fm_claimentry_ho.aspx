<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimentry_ho.aspx.cs" Inherits="fm_claimentry_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>  

    <script>

        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btprop.ClientID%>').click();
        }

        function AgHOSelected(sender, e) {
            $get('<%=hdagho.ClientID%>').value = e.get_value();
        }
        
        function RefreshData(dt1,dt2) {
            $get('<%=hdclaim.ClientID%>').value = dt1;
            $get('<%=hdsalespoint.ClientID%>').value = dt2;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:HiddenField ID="hdedit" runat="server" />
    <asp:HiddenField ID="hdclaim" runat="server" />
    <asp:HiddenField ID="hdccnr" runat="server" />
    <asp:HiddenField ID="hdsalespoint" runat="server" />
    <asp:HiddenField ID="hdvendor" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdagho" runat="server" />

    <div class="container-fluid">
        <div class="page-header">
            <h3>Claim Entry HO
            </h3>
        </div>

        <div class="form-horizontal">
            
        <fieldset>
            <legend><%--Claim Headers--%></legend>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="claimNo" class="col-xs-4 col-form-label">Claim No</label>
                        <div class="col-xs-8">
                            <div class="input-group">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txClaimNo" runat="server" CssClass="form-control input-group-sm" Width="100%" Height="100%"></asp:TextBox>
                                        <%--<asp:Label ID="lbclaimno" runat="server" CssClass="form-control input-group-sm" Width="100%" Height="100%" BorderStyle="Solid" BorderWidth="1px">New</asp:Label>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearchclaim" runat="server" CssClass="btn btn-primary" OnClick="btsearchclaim_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <%--<div class="form-group">
                        <label for="claimNo" class="col-xs-4 col-form-label">Claim No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txClaimNo" runat="server" CssClass="" Width="50%"></asp:TextBox>
                        </div>
                    </div>--%>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="AgHONo" class="col-xs-4 col-form-label col-form-label-sm">AG HO No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txAgHo" runat="server" CssClass="" Width="100%"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txAgHo_AutoCompleteExtender" runat="server" ServiceMethod="GetListAgHO" TargetControlID="txAgHo" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AgHOSelected">
                            </asp:AutoCompleteExtender>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="ccnr" class="col-xs-4 col-form-label col-form-label-sm">Transaction Period</label>
                        <div class="col-xs-8">
                            <label for="ccnr" class="col-xs-2 col-form-label col-form-label-sm">Month</label>
                            <div class="col-xs-4">
                                <asp:DropDownList ID="cbMonth" runat="server" Width="100%">
                                    <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="ccnr" class="col-xs-2 col-form-label col-form-label-sm">Year</label>
                            <div class="col-xs-4">
                                <asp:DropDownList ID="cbYear" runat="server"  Width="100%">
                                    <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                    <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                    <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                    <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                    <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                    <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
									<asp:ListItem Text="2018" Value="2018" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="ccnr" class="col-xs-4 col-form-label col-form-label-sm">CCNR No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txBranchNo" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="propNo" class="col-xs-4 col-form-label col-form-label-sm">Proposal No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txPropNo" runat="server" CssClass="" Width="100%"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txPropNo_AutoCompleteExtender" runat="server" ServiceMethod="GetListProposal" TargetControlID="txPropNo" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="PropSelected">
                            </asp:AutoCompleteExtender>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="ccnr" class="col-xs-4 col-form-label col-form-label-sm">Old CCNR No</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txRefBranchNo" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="vendor" class="col-xs-4 col-form-label col-form-label-sm">Vendor</label>
                        <div class="col-xs-8">
                            <%--<asp:TextBox ID="txVendor" runat="server" CssClass="" Width="100%"></asp:TextBox>--%>
                            <asp:DropDownList ID="cbvendor" runat="server" CssClass="form-control-static input-sm" Height="100%" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="claimDate" class="col-xs-4 col-form-label col-form-label-sm">Claim Date</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="dtClaim" runat="server" CssClass="" Width="50%"></asp:TextBox>
                            <asp:CalendarExtender ID="dtClaim_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtClaim">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="promotion" class="col-xs-4 col-form-label col-form-label-sm">Promotion</label>
                        <div class="col-xs-8">
                            <%--<asp:TextBox ID="txPromotion" runat="server" CssClass="" Width="100%"></asp:TextBox>--%>
                            <asp:DropDownList ID="cbPromotion" CssClass="form-control-static input-sm" runat="server" Height="100%" Width="50%" ></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="branch" class="col-xs-4 col-form-label col-form-label-sm">Branch</label>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="cbbranch" runat="server" Width="50%" ></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="product" class="col-xs-2 col-form-label col-form-label-sm">Product</label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txProduct" runat="server" CssClass="" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="cost" class="col-xs-4 col-form-label col-form-label-sm">Budget</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txBudget" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="claimRemarks" class="col-xs-2 col-form-label col-form-label-sm">Remarks</label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txremark" runat="server" CssClass="" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>  

        <fieldset>
            <legend>Document Supported</legend>
            <div class="row">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grdcate" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdcate_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Document Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upload">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="upl" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grddoc" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Document Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File location">
                                    <ItemTemplate>
                                        <a class="example-image-link" href="/images/claim_doc/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                            <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </div>
            </div>
        </fieldset>

        <fieldset>
            <legend>Claim Details</legend>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="claimMethod" class="col-xs-3 col-form-label col-form-label-sm">Mechanism</label>
                        <div class="col-xs-9">
                        <asp:RadioButtonList ID="rdmethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdmethod_SelectedIndexChanged" CssClass="form-control form-control-sm" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="FG">Free Good</asp:ListItem>
                            <asp:ListItem Value="CH">Cash Amount</asp:ListItem>
                            <asp:ListItem Value="PC">Percentage</asp:ListItem>
                            <asp:ListItem Value="CSH">Cash Out</asp:ListItem>
                            <asp:ListItem Value="CNDN">CNDN</asp:ListItem>
                            <asp:ListItem Value="BA">Business Agreement</asp:ListItem>
                            <asp:ListItem Value="CMHO">Credit Memo</asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="col-md-4">
                            <label for="product" class="col-form-label col-form-label-sm">Product</label><br />
                            <asp:DropDownList ID="cbproduct" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="cbproduct_SelectedIndexChanged"></asp:DropDownList> 
                        </div>
                        <div class="col-md-1">
                            <label for="orderQty" class="col-form-label col-form-label-sm">OrderQty</label>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>                                
                            <asp:TextBox ID="txOrderQty" runat="server" CssClass="" OnTextChanged="txAll_TextChanged"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>  
                        </div>
                        <%--<div class="col-md-1">
                            <label for="orderValue" class="col-form-label col-form-label-sm">OrderValue</label>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>                                
                            <asp:TextBox ID="txOrderValue" runat="server" CssClass="form-control form-control-sm" OnTextChanged="txAll_TextChanged"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>  
                        </div>--%>
                        <div class="col-md-1">
                            <label for="freeQty" class="col-form-label col-form-label-sm">FreeQty</label>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txFreeQty" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <%--<div class="col-md-1">
                            <label for="freeValue" class="col-form-label col-form-label-sm">FreeValue</label>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txFreeValue" runat="server" CssClass="form-control form-control-sm" OnTextChanged="txAll_TextChanged"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>--%>
                        <div class="col-md-1">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                            <label for="discount" class="col-form-label col-form-label-sm">Discount</label>
                            <asp:TextBox ID="txDiscount" runat="server" CssClass="" OnTextChanged="txAll_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-1">
                            <label for="unitPrice" class="col-form-label col-form-label-sm">UnitPrice</label>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbPrice" runat="server" Width="100%" OnSelectedIndexChanged="txAll_TextChanged" AutoPostBack="true"></asp:DropDownList> 
                            <asp:TextBox ID="txPrice" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cbproduct" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-2">
                            <label for="Amount" class="col-form-label col-form-label-sm">Total Amount</label>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txAmount" runat="server" CssClass=""></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="txOrderValue" EventName="TextChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="txOrderQty" EventName="TextChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="txFreeValue" EventName="TextChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="txFreeQty" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txDiscount" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cbPrice" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-1">
                            <label for="btnAdd" class="col-form-label col-form-label-sm" >Action</label>
                            <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-defaul btn-sm" OnClick="btadd_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-md-12">
                        <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="grdproduct" OnRowDeleting="grdproduct_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                                <Columns>
                                    <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lbproduct" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lborderqty" runat="server" Text='<%# Eval("qtyfree").ToString() == "0.00" ? "0.00" : Eval("qtyorder") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbordervalue" runat="server" Text='<%# Eval("qtyfree").ToString() == "0.00" ? Eval("qtyorder") : "0.00" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Free Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbfreeqty" runat="server" Text='<%# Eval("qtyfree") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
<%--                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbdisc" runat="server" Text='<%# Eval("disc_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Free Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbfreevalue" runat="server" Text='<%# Eval("disc_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbamount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>                    
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:GridView ID="grdBA" runat="server"  CssClass="table table-striped table-bordered mygrid"  CellPadding="3" ShowFooter="True"  Caption="Credit Memo Claim (HO)" AutoGenerateColumns="False" OnRowEditing="grdBA_RowEditing" OnRowUpdating="grdBA_RowUpdating" OnRowCancelingEdit="grdBA_RowCancelingEdit">
                            <Columns>
                            <asp:TemplateField HeaderText="Contract No">
                                <ItemTemplate>
                                    <asp:Label ID="so_cd" runat="server" Text='<%#Eval("so_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contract Ag No">
                                <ItemTemplate>
                                    <asp:Label ID="inv_no" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contract Sbtc No">
                                <ItemTemplate>
                                    <asp:Label ID="manual_no" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created Date">
                                <ItemTemplate>
                                    <asp:Label ID="inv_dt" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}",Eval("inv_dt")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbamt" runat="server" Text='<%# Eval("freevalue") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txamt" runat="server" Text='<%#Eval("freevalue") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Change Amount" ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                            <RowStyle BackColor="#F7F6F3"  />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"  />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </fieldset>
        </div>

    </div>
    <div class="row">
      <div class="col-sm-12">
        <div class="text-center">
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btnew" onserverclick="btnew_Click" >
              <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btsave" onserverclick="btsave_Click" >
              <span class="glyphicon glyphicon-save" aria-hidden="true"></span> Save
            </button>
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btupdate" onserverclick="btupdate_ServerClick" >
              <span class="glyphicon glyphicon-save" aria-hidden="true"></span> Update
            </button>
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btedit" onserverclick="btedit_ServerClick" >
              <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Edit
            </button>            
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btprint" onserverclick="btprint_ServerClick" >
              <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print
            </button>
            <button type="submit" class="btn btn-danger btn-sm" runat="server" id="btdelete" onserverclick="btdelete_ServerClick" >
              <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Delete
            </button>
            <div id="button" style="display: none">
                <asp:Button ID="btprop" runat="server" Text="Button" OnClick="btprop_Click" />
            </div> 
            <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none"/>
        </div>
      </div>
    </div>

</asp:Content>

