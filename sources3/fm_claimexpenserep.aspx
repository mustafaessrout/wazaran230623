<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimexpenserep.aspx.cs" Inherits="fm_claimexpenserep" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="page-header">
            <h3>Claim Expense Report <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label></h3> 
        </div>

        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="type" class="col-xs-2 col-form-label col-form-label-sm">Type</label>    
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cbType" runat="server" OnSelectedIndexChanged="cbType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%">
                                <asp:ListItem Value="CO">Claim Cashout</asp:ListItem>
                                <asp:ListItem Value="CN">CN & DN Claim</asp:ListItem>
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="ccnr" class="col-sm-2 col-md-1 control-label control-label-sm">Start Date</label>
                        <div class="col-md-4 col-sm-3">
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtstart" Format="M/d/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <label for="ccnr" class="col-sm-2 col-md-1 control-label control-label-sm">End Date</label>
                        <div class="col-md-4 col-sm-3">
                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtend_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtend" Format="M/d/yyyy">
                            </asp:CalendarExtender>
                        </div>                    
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="text-center navi">
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <button type="submit" class="btn btn-search btn-sm" runat="server" id="btsearch" onserverclick="btsearch_ServerClick" >
                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span> Search</button>    
                            <button type="submit" class="btn btn-info btn-sm" runat="server" id="btprint" onserverclick="btprint_ServerClick" >
                            <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print</button>     
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="grdcashout" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" AllowPaging="true" OnPageIndexChanging="grdcashout_PageIndexChanging" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Claim No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claimco_cd") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prop No">
                                        <ItemTemplate><%# Eval("prop_no") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate><%# Eval("paid_dt") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate><%# Eval("amt","{0:F2}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vat">
                                        <ItemTemplate><%# Eval("vat","{0:F2}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate><%# Eval("total","{0:F2}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="For">
                                        <ItemTemplate><%# Eval("rdinternal").ToString() == "I" ? "Internal" : "External" %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate><%# Eval("receiver") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <a class="example-image-link" href="/images/claim_cashout/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                                    <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><%# Eval("claimco_sta_id").ToString() == "A" ? "Approved" : Eval("claimco_sta_id").ToString() == "N" ? "New" : "Rejected" %></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="grdcn" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" AllowPaging="true" OnPageIndexChanging="grdcn_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sys No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcndncode" runat="server" Text='<%# Eval("cndn_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate><%# Eval("cndn_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust">
                                        <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salesman">
                                        <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate><%# Eval("amt","{0:F2}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vat">
                                        <ItemTemplate><%# Eval("vat","{0:F2}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate><%# Eval("total","{0:F2}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><%# Eval("cndn_sta_id").ToString() == "A" ? "Approved" : Eval("cndn_sta_id").ToString() == "N" ? "New" : "Rejected" %></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

