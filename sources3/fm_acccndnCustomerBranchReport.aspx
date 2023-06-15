<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acccndnCustomerBranchReport.aspx.cs" Inherits="fm_acccndnCustomerBranchReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <style>
        .ContentPlaceHolder1_txcust_AutoCompleteExtender_completionListElem {
            overflow-x: scroll;
            overflow-y: scroll;
        }
    </style>
    <script>
        function openwindow() {
            var oNewWindow = window.open("lookup_acccndn.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();

        }

        function CNDNSelected(sender, e) {
            $get('<%=hdfcndn.ClientID%>').value = e.get_value();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">DN Customer View</h4>
        <div class="h-divider"></div>
        <div class="container">

            <div class="form-group">
                <div class="row">
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbCNDNSearch" Checked="true" GroupName="search" runat="server" Text="Search By DN Date From" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                   
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtCNDNFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtCNDNFromDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">To From </label>
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtCNDNToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender2" runat="server" Format="d/M/yyyy" TargetControlID="dtCNDNToDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="row">
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbCustomerSearch" GroupName="search" runat="server" Text="Search By Customer" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                    <div class="col-md-5">

                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdcust" runat="server" />
                                <div class="input-group full">
                                    <div>
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="row">
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbHOSearch" GroupName="search" runat="server" Text="Search By Ref Number" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                    <div class="col-md-5">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfcndn" runat="server" />
                                <div class="input-group full">
                                    <div>
                                        <asp:TextBox ID="txtHORef" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txtHORef" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CNDNSelected" ServiceMethod="GetCompletionCNDNList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <asp:LinkButton ID="btPrint" runat="server" CssClass="btn btn-primary" OnClick="btPrintCNDN_Click">Show Data</asp:LinkButton>
                    <asp:LinkButton ID="btnViewReport" runat="server" CssClass="btn btn-primary" OnClick="btnViewReport_Click">View Report</asp:LinkButton>
                    <asp:LinkButton ID="btnViewReportDetails" runat="server" CssClass="btn btn-primary" OnClick="btnViewReportDetails_Click">View Report Details</asp:LinkButton>
                </div>

            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                                ForeColor="#333333" EmptyDataText="No records Found">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                   <asp:BoundField DataField="SNo" HeaderText="Tot Inv" />
                                    <asp:BoundField DataField="cndn_no" HeaderText="ID" />
                                    <asp:BoundField DataField="CustName" HeaderText="Customer" />
                                    <asp:BoundField DataField="cndn_dt" HeaderText="DN Date" DataFormatString="{0:d/M/yyyy}" />
                                    <asp:BoundField DataField="statusValue" HeaderText="Status" />
                                    <asp:BoundField DataField="refno" HeaderText="Ref HO" />
                                    <asp:BoundField DataField="crdb" HeaderText="Type" />
                                    <asp:BoundField DataField="amt" HeaderText="Amount" />
                                    <asp:BoundField DataField="vat" HeaderText="Vat" />
                                    <asp:BoundField DataField="totAmt" HeaderText="Total Amount" />
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                    <asp:HyperLinkField DataNavigateUrlFields="filedoc" Target="_blank" HeaderText="Document" DataNavigateUrlFormatString="/images/CNDNCust/{0}" Text="View Doc" />
                                </Columns>
                                <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                                <EditRowStyle BackColor="#999999" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

