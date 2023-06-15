<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acccndnBranchReport.aspx.cs" Inherits="fm_acccndnBranchReport" %>

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
        <h4 class="jajarangenjang">CN DN Adjustment View</h4>
        <div class="h-divider"></div>
        <div class="container">

            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbCNDNSearch" Checked="true" GroupName="search" runat="server" Text="Search By CNDN Date From" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                    <div class="col-md-2 drop-down-date">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtCNDNFromDate" runat="server" CssClass="form-control" onkeydown="return false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtCNDNFromDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">To </label>
                    <div class="col-md-2 drop-down-date">

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtCNDNToDate" runat="server" CssClass="form-control" onkeydown="return false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender2" runat="server" Format="d/M/yyyy" TargetControlID="dtCNDNToDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbPostSearch" GroupName="search" runat="server" Text="Search By Post Date From" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                    <div class="col-md-2 drop-down-date">

                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtPostFromDate" runat="server" CssClass="form-control" onkeydown="return false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender3" runat="server" Format="d/M/yyyy" TargetControlID="dtPostFromDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">To </label>
                    <div class="col-md-2 drop-down-date">

                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtPostToDate" runat="server" CssClass="form-control" onkeydown="return false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender4" runat="server" Format="d/M/yyyy" TargetControlID="dtPostToDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">
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
                    <div class="col-md-3">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbHOSearch" GroupName="search" runat="server" Text="Search By HO Ref Number" />
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
                    <asp:LinkButton ID="btnViewReportHO" runat="server" CssClass="btn btn-primary" OnClick="btnViewReportHO_Click">View Report HO</asp:LinkButton>
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
                                   
                                    <asp:BoundField DataField="cndn_cd" HeaderText="ID" />
                                    <asp:BoundField DataField="SNo" HeaderText="Tot Inv" />
                                    <asp:BoundField DataField="statusValue" HeaderText="Status" />
                                    <asp:BoundField DataField="refho_no" HeaderText="Ref HO" />
                                    <asp:BoundField DataField="rejectInfo" HeaderText="Reject Info" />
                                    <asp:BoundField DataField="cndnType" HeaderText="Type" />
                                    <asp:BoundField DataField="totAmtCN" HeaderText="Tot CN" />
                                    <asp:BoundField DataField="totAmtDN" HeaderText="Tot DN" />
                                    <asp:BoundField DataField="vatamt" HeaderText="Tot CNDN Vat" />
                                    <asp:BoundField DataField="post_dt" HeaderText="Post Date" DataFormatString="{0:d/M/yyyy}" />
                                    <asp:BoundField DataField="cndn_dt" HeaderText="CNDN Date" DataFormatString="{0:d/M/yyyy}" />
                                    <asp:BoundField DataField="CustName" HeaderText="Customer" />
                                    <asp:BoundField DataField="inv_no" HeaderText="Inv Number" />
                                    <asp:BoundField DataField="inv_CNAmount" HeaderText="Inv CN" />
                                    <asp:BoundField DataField="inv_DNAmount" HeaderText="Inv DN" />
                                    <asp:HyperLinkField DataNavigateUrlFields="uploadByUser" Target="_blank" HeaderText="Document" DataNavigateUrlFormatString="/images/CNDNAdj/{0}" Text="View Doc" />
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

