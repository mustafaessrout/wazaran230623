<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimagrep.aspx.cs" EnableEventValidation="true" Inherits="fm_claimagrep" %> 

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 

    <style>
        .s150{
            width: 250px;
        }
        .ttl{
            background: #5D7B9D;
            padding:8px 5px;
            color: #fff;
        }
        .cnt{
            padding:5px;
            min-height:25px;
        }
        .brd{
            border: 1px solid rgba(0,0,0,.5);
            border-radius:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="lhStatus" runat="server" />
    <div class="divheader">
        Business Agreement Report<asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label>
    </div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label col-form-label-sm">Type</label>    
                        <div class="col-sm-4 drop-down">
                            <asp:DropDownList ID="cbtype" runat="server" OnSelectedIndexChanged="cbtype_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm" Width="100%">
                                <asp:ListItem Value="ALL">-- All Type --</asp:ListItem>
                                <asp:ListItem Value="FG">Free Good</asp:ListItem>
                                <asp:ListItem Value="CH">Cashout</asp:ListItem>
                                <asp:ListItem Value="CNDN">CNDN</asp:ListItem>
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="ccnr" class="col-sm-2 control-label control-label-sm">Start Date</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <label for="ccnr" class="col-sm-2 control-label control-label-sm">End Date</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtend_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
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
                <div class="col-md-12 margin-bottom" >
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                         <div class="table-responsive">
                            <asp:GridView ID="grdpayment" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" AllowPaging="true" OnPageIndexChanging="grdpayment_PageIndexChanging" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Contract No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbagreecode" runat="server" Text='<%# Eval("contract_no") %>' CssClass="data-table-popup"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposal No">
                                        <ItemTemplate>
                                            <div class="data-table-popup">
                                                <%# Eval("prop_no") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <div class="data-table-popup">
                                                <%# Eval("paymenttype") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <div class="data-table-popup">
                                                <%# Eval("customer") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="s150" HeaderText="Item">
                                        <ItemTemplate>
                                            <div class="data-ellapsis data-table-popup" data-ell-length="30" data-del-space="true">
                                                <%# Eval("item") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="s150" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Item">
                                        <ItemTemplate>
                                            <div class="data-ellapsis data-popup data-table-popup" data-ell-length="30" data-del-space="true" data-pop-up-title="Free Item" data-pop-up-content="true-data" >
                                                <%# Eval("freeitem") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Date">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <%# Eval("due_dt") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <%# String.Format("{0:n} {1}", Eval("amount"), Eval("uom") )%>
                                            </div>        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document">
                                        <ItemTemplate>
                                            <div  class="data-table-popup">
                                                <a class="example-image-link" href="/images/contract_doc/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                                    <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate><%# Eval("remark") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><%# Eval("status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  CssClass="row-pop-up" />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                        </div>
                        <div class="table-copy-page-fix" id="copytable"></div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

