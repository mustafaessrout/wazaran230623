<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_excel.aspx.cs" Inherits="fm_excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-group">
            <div class="row">
                <label class="col-md-1 control-label">Type</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbtype" CssClass="form-control" runat="server">
                        <asp:ListItem>---- select ----</asp:ListItem>
                        <asp:ListItem Value="B">Customer Price Booking</asp:ListItem>
                        <asp:ListItem Value="C">Customer Adjustment Booking</asp:ListItem>
                        <asp:ListItem Value="G">Customer Group Adjustment</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <label class="col-md-1 control-label">File Excel</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="fup" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-fix">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusttype" runat="server" Text='<%#Eval("cust_typ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unitprice">
                                <ItemTemplate>
                                    <asp:Label ID="lbunitprice" runat="server" Text='<%#Eval("unitprice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start [M/D/YYYY]">
                                <ItemTemplate>
                                    <asp:Label ID="lbstartdate" runat="server" Text='<%#Eval("start_dt","{0:M/d/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End [M/D/YYYY]">
                                <ItemTemplate>
                                    <asp:Label ID="lbenddate" runat="server" Text='<%#Eval("end_dt","{0:M/d/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created">
                                <ItemTemplate>
                                    <asp:Label ID="lbcreated" runat="server" Text='<%#Eval("createdby") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Price"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdgroup" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-fix" OnRowDataBound="grdgroup_RowDataBound" OnRowDeleting="grdgroup_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusgrcd" runat="server" Text='<%#Eval("cusgrcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name">
                                <ItemTemplate><%#Eval("fld_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusttype" runat="server" Text='<%#Eval("cust_typ")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expect Price">
                                <ItemTemplate>
                                    <asp:Label ID="lbexpectprice" runat="server" Text='<%#Eval("expect_price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start">
                                <ItemTemplate>
                                    <asp:Label ID="lbstartdate" runat="server" Text='<%#Eval("start_dt","{0:M/d/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End">
                                <ItemTemplate>
                                    <asp:Label ID="lbenddate" runat="server" Text='<%#Eval("end_dt","{0:M/d/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created">
                                <ItemTemplate><%#Eval("createdby") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unitprice">
                                <ItemTemplate>
                                    <asp:Label ID="lbunitprice" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adjust(+/-)">
                                <ItemTemplate>
                                    <asp:Label ID="lbadjust" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-fix" OnRowDataBound="grdcust_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcode" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Cd">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespoint_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expect Price">
                                <ItemTemplate>
                                    <asp:Label ID="lbexpectprice" runat="server" Text='<%#Eval("expect_price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start">
                                <ItemTemplate>
                                    <asp:Label ID="lbstartdate" runat="server" Text='<%#Eval("start_dt","{0:M/d/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End">
                                <ItemTemplate>
                                    <asp:Label ID="lbenddate" runat="server" Text='<%#Eval("end_dt","{0:M/d/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created">
                                <ItemTemplate><%#Eval("createdby") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base Price">
                                <ItemTemplate>
                                    <asp:Label ID="lbbaseprice" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adjust (+/-)">
                                <ItemTemplate>
                                    <asp:Label ID="lbadjust" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12 center">
                    <asp:LinkButton ID="btexport" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-info" OnClick="btexport_Click">Export To Wazaran Price Structure</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-primary" OnClientClick="openreport('fm_report2.aspx?src=bookprice');">Print Booking Price By Today</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

