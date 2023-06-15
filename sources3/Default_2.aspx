<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Default_2.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <!--admin lte css-->
    <link href="<%# ResolveUrl("~/css/custom/adminlte.css") %>" rel="stylesheet" />
    <!-- Font Awesome Icons -->
    <link href="<%# ResolveUrl("~/css/fontawesome-free/css/all.min.css") %>" rel="stylesheet" />
    <!--admin lte js-->
    <link href="<%# ResolveUrl("~/js//adminlte.js") %>" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <%--<section class="content col-md-12">--%>
<%--    <div class="container-fluid">--%>

    <div id="container">
        <%--<h4 class="jajarangenjang">Dashboard</h4>
        <div class="h-divider"></div>--%>

        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="col-sm-3 col-md-3">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Branch</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
               </div>
            </div>
            <div class="col-sm-3 col-md-4">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Start Date</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="dtstart" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="dtstart_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date posBottom" PopupPosition="BottomLeft" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                        </asp:CalendarExtender>
                    </div>
                    <label class="control-label col-sm-2">End Date</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="dtend_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="dtend_CalendarExtender" CssClass="date posBottom" PopupPosition="BottomLeft" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                        </asp:CalendarExtender>
                    </div>
               </div>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Type</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">
                            <asp:ListItem Value="s1">Summary Sales</asp:ListItem>
                            <asp:ListItem Value="s2">Summary Stock</asp:ListItem>
                            <asp:ListItem Value="s3">Summary Cashier</asp:ListItem>
                        </asp:DropDownList>
                    </div>
               </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div class="col-sm-6 col-md-3">
                <div class="info-box">
                    <span class="info-box-icon bg-info elevation-1"><i class="fa fa-shopping-cart"></i></span>
                    <div class="info-box-content">
                    <span class="info-box-text">SALES by QTY</span>
                    <span class="info-box-number">
                        <asp:Label ID="lbltotsalesqty" runat="server" Text="100 CTN" CssClass="control-label col-sm-12"></asp:Label>
                    </span>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="info-box">
                    <span class="info-box-icon bg-info elevation-1"><i class="fa fa-money"></i></span>
                    <div class="info-box-content">
                    <span class="info-box-text">SALES REVENUE </span>
                    <span class="info-box-number">
                        <asp:Label ID="lbtotsalesamt" runat="server" Text="1000 L.E" CssClass="control-label col-sm-12"></asp:Label>
                    </span>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="info-box">
                    <span class="info-box-icon bg-info elevation-1"><i class="fa fa-money"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">EXPENSES / CASH OUT</span>
                        <span class="info-box-number">
                            <asp:Label ID="lbtotcashout" runat="server" Text="1000 L.E" CssClass="control-label col-sm-6"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="info-box">
                    <span class="info-box-icon bg-info elevation-1"><i class="fa fa-tag"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">STOCK IN</span>
                        <span class="info-box-number">
                            <asp:Label ID="lbtotstockin" runat="server" Text="100 CTN" CssClass="control-label col-sm-12"></asp:Label>
                        </span>
                    </div>
                    <div class="info-box-content">
                        <span class="info-box-text">STOCK OUT</span>
                        <span class="info-box-number">
                            <asp:Label ID="lbtotstockout" runat="server" Text="100 CTN" CssClass="control-label col-sm-12"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="row">
            <div class="col-md-9 col-sm-9">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>

                <%-- Grid Summary Sales --%>
                <div class="card" runat="server" id="vSalesSummary">
                    <div class="card-header border-transparent">
                    <h3 class="card-title">Summary Sales</h3>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="grdsales" runat="server" CellPadding="0" GridLines="None" CssClass="table m-0" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" OnRowDataBound="grdsales_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lborderqty" runat="server" Text='<%# String.Format("{0:n5} {1}", Eval("qtyorder"), " CTN")%>'></asp:Label>
                                            <asp:HiddenField ID="hdqtyorder" runat="server" Value='<%# Eval("qtyorder") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lbfreeqty" runat="server" Text='<%# String.Format("{0:n5} {1}", Eval("qtyfree"), " CTN")%>'></asp:Label>
                                            <asp:HiddenField ID="hdqtyfree" runat="server" Value='<%# Eval("qtyfree") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Revenue">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtotamount" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("amount"), " EGP")%>'></asp:Label>
                                            <asp:HiddenField ID="hdamount" runat="server" Value='<%# Eval("amount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>                 
                <%-- Grid Summary Sales --%>

                <%-- Grid Summary Stock --%>
                <div class="card" runat="server" id="vStockSummary">
                    <div class="card-header border-transparent">
                    <h3 class="card-title">Summary Branch Stock</h3>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="grdstock" runat="server" CellPadding="0" GridLines="None" CssClass="table m-0" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" OnRowDataBound="grdstock_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lbitemcd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label> - <asp:Label ID="lbitemnm" runat="server" Text='<%# Eval("item_shortname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock In">
                                        <ItemTemplate>
                                            <asp:Label ID="lbstockin" runat="server" Text='<%# String.Format("{0:n5} {1}", Eval("stock_in"), " CTN")%>'></asp:Label>
                                            <asp:HiddenField ID="hdstockin" runat="server" Value='<%# Eval("stock_in") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loading Van">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvanin" runat="server" Text='<%# String.Format("{0:n5} {1}", Eval("van_in"), " CTN")%>'></asp:Label>
                                            <asp:HiddenField ID="hdvanin" runat="server" Value='<%# Eval("van_in") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales + Free">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsales" runat="server" Text='<%# String.Format("{0:n5} {1}", Eval("salesfree"), " CTN")%>'></asp:Label>
                                            <asp:HiddenField ID="hdsalesfree" runat="server" Value='<%# Eval("salesfree") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unloading Van">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvanout" runat="server" Text='<%# String.Format("{0:n5} {1}", Eval("van_out"), " CTN")%>'></asp:Label>
                                            <asp:HiddenField ID="hdvanout" runat="server" Value='<%# Eval("van_out") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>                 
                <%-- Grid Summary Stock --%>

                <%-- Grid Summary Cashier --%>
                <div class="card" runat="server" id="vCashierSummary">
                    <div class="card-header border-transparent">
                    <h3 class="card-title">Summary Cashier (In / Out)</h3>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="grdcash" runat="server" CellPadding="0" GridLines="None" CssClass="table m-0" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" OnRowDataBound="grdcash_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash In">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcashin" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("cashin"), " EGP")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Deposit (out)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbankdeposit" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("bankdeposit"), " EGP")%>'></asp:Label>
                                            <asp:HiddenField ID="hdbank" runat="server" Value='<%# Eval("bankdeposit") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expense (out)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcashout" runat="server" Text='<%# String.Format("{0:n} {1}", Eval("cashout"), " EGP")%>'></asp:Label>
                                            <asp:HiddenField ID="hdcashout" runat="server" Value='<%# Eval("cashout") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>                 
                <%-- Grid Summary Sales --%>

            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="col-md-3 col-sm-3">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div id="appSalesSummary" runat="server">
                        <div class="info-box mb-3 bg-warning">
                          <span class="info-box-icon"><i class="fa fa-tag"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Sales Return</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_returnconfirmation.aspx" ForeColor="Black">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappsalesreturn" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                        <div class="info-box mb-3 bg-danger">
                          <span class="info-box-icon"><i class="fa fa-tag"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text">Sales Full Return</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_fullreturnconfirmation.aspx" ForeColor="White">
                                <span class="info-box-number">To Reivew : <asp:Label ID="lbtotappsalesfullreturn" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                        <div class="info-box mb-3 bg-info">
                          <span class="info-box-icon"><i class="fa fa-users"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text">Customer Transfer</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_custtransferconfirmation.aspx" ForeColor="White">
                                <span class="info-box-number">To Reveiw : <asp:Label ID="lbtotappcusttransfer" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                    </div>
                    <div id="appSummaryStock" runat="server">
                        <div class="info-box mb-3 bg-info">
                          <span class="info-box-icon"><i class="fa fa-tag"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Internal Transfer</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_app_internaltransfer.aspx" ForeColor="White">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappinternaltransfer" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                        <div class="info-box mb-3 bg-danger">
                          <span class="info-box-icon"><i class="fa fa-table"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Stock Add | Loss | Destroy</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_app_stockaddloss.aspx" ForeColor="White">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappstockaddloss" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                        <div class="info-box mb-3 bg-warning">
                          <span class="info-box-icon"><i class="fa fa-adjust"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Stock Adjusment</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_app_stockadj.aspx">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappstockadj" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                    </div>
                    <div id="appSummaryCashier" runat="server">
                        <div class="info-box mb-3 bg-info">
                          <span class="info-box-icon"><i class="fa fa-bank"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Bank Deposit </span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_cashconfirmation.aspx" ForeColor="White">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappbankdeposit" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                        <div class="info-box mb-3 bg-warning">
                          <span class="info-box-icon"><i class="fa fa-pound-sign"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Cashout Request</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_cashoutconfirmation.aspx">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappcashout" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                        <div class="info-box mb-3 bg-danger">
                          <span class="info-box-icon"><i class="fa fa-money"></i></span>
                          <div class="info-box-content">
                            <span class="info-box-text"></span>
                            <span class="info-box-text">Closing Cashier</span>
                            <asp:HyperLink runat="server" NavigateUrl="~/fm_cashierconfirmation.aspx" ForeColor="White">
                                <span class="info-box-number">To Review : <asp:Label ID="lbtotappclosingcashier" runat="server" Text="0" CssClass="control-label col-sm-12"></asp:Label></span>
                            </asp:HyperLink>
                          </div>
                        </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <%--</section>--%>
    
</asp:Content>

