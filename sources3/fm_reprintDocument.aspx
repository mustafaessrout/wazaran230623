<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reprintDocument.aspx.cs" Inherits="fm_reprintDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        function CustomerSelected(sender, e) {
            $get('<%=hdcustomer.ClientID%>').value = e.get_value();
            $get('<%=btlookup.ClientID%>').click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcustomer" runat="server" />
    <div class="alert alert-info text-bold">Reprint Document</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="clearfix">
                <div class="col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Salespoint</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  input-sm" AutoPostBack="true" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Salesman</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsalesman" runat="server" onchange="ShowProgress();" CssClass="form-control  input-sm" AutoPostBack="true" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
        <div class="row margin-bottom">
            <div class="clearfix">

                <div class="col-sm-6">
                    <label class="col-sm-4 control-label titik-dua">Customer</label>
                    <div class="col-sm-8">
                        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>--%>
                        <asp:TextBox ID="txcustomersearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txcustomersearch_AutoCompleteExtender" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionListCssClass="input-sm" CompletionInterval="10" CompletionSetCount="1" ServiceMethod="GetCustomerList" OnClientItemSelected="CustomerSelected" UseContextKey="true" ShowOnlyCurrentWordInCompletionListItem="true" runat="server" TargetControlID="txcustomersearch">
                        </asp:AutoCompleteExtender>

                        <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
                <div class="col-sm-6">
                    <label class="col-sm-4 control-label titik-dua">Salespoint</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtbranch" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="clearfix">
                <div class="col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Document Type</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbtype" runat="server" onchange="ShowProgress();" OnSelectedIndexChanged="cbtype_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True">
                            <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                            <asp:ListItem Text="Take Order" Value="TO"></asp:ListItem>
                            <asp:ListItem Text="Canvas Order" Value="CO"></asp:ListItem>
                            <asp:ListItem Text="Bonus / Free Order" Value="IB"></asp:ListItem>
                            <asp:ListItem Value="2">CASHOUT</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Transaction No.</label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <asp:TextBox ID="txt_txNo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btn_search" runat="server" CssClass="btn btn-primary btn-search" Text="Search" OnClick="btn_search_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="margin-bottom margin-top">
            <asp:GridView ID="grd_reprintDoc" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grd_reprintDoc_PageIndexChanging" OnRowDataBound="grd_reprintDoc_RowDataBound" CellPadding="0" GridLines="None" CssClass="table table-striped mygrid table-hover">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Transaction No.">
                        <ItemTemplate>

                            <asp:Label ID="so_cd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="so_dt" HeaderText="Transaction Date" ReadOnly="True" DataFormatString="{0:dd-MM-yyyy}" SortExpression="so_dt" />
                    <asp:TemplateField HeaderText="Invoice No.">
                        <ItemTemplate>
                            <asp:Label ID="lbinv_no" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate>
                            <asp:Label ID="txmanualno" runat="server" Text='<%# Eval("manual_inv_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cust_nm1" HeaderText="Customer" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="remark" HeaderText="Remark" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btprintloading" OnClientClick="ShowProgress();" CssClass="btn-primary btn btn-sm btn-print" OnClick="LinkButton1_Click" runat="server" CausesValidation="False" CommandName="Select" Text="Loading"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="btreprintbl" OnClientClick="ShowProgress();" CssClass="btn-primary btn btn-sm btn-print" OnClick="btreprintbl_Click" runat="server" CausesValidation="False" CommandName="Select" Text="BL"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="btprintinvoice" OnClientClick="ShowProgress();" CssClass="btn-info btn btn-sm btn-print" OnClick="LinkButton2_Click" runat="server" CausesValidation="False" CommandName="Select" Text="Invoice"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LinkButton3" OnClientClick="ShowProgress();" CssClass="btn-warning btn btn-sm btn-print" OnClick="LinkButton3_Click" runat="server" CausesValidation="False" CommandName="Select" style="display:none" Text="Invoice Free"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LinkButton6" OnClientClick="ShowProgress();" CssClass="btn-warning btn btn-sm btn-print" OnClick="LinkButton6_Click" runat="server" CausesValidation="False" CommandName="Select" Text="Invoice Receipt"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle CssClass="table-edit" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div class="margin-bottom">
            <asp:GridView ID="grdCashout" runat="server" CssClass="table table-striped mygrid table-hover" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grdCashout_PageIndexChanging" OnRowDataBound="grdCashout_RowDataBound" CellPadding="0" GridLines="None">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Transaction No.">
                        <ItemTemplate>

                            <asp:Label ID="lbCHO" runat="server" Text='<%# Eval("casregout_cd") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cashout_dt" HeaderText="Transaction Date" ReadOnly="True" DataFormatString="{0:dd-MM-yyyy}" SortExpression="cashout_dt" />
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate>
                            <asp:Label ID="lbmanualno" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="remark" HeaderText="Remark" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton4" CssClass="button2 print" OnClick="LinkButton4_Click" runat="server" CausesValidation="False" CommandName="Select" Text="CASHOUT"></asp:LinkButton>&nbsp;                  
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle CssClass="table-edit" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>

        <div class="margin-bottom">
            <asp:GridView ID="grdint" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grdint_PageIndexChanging" OnRowDataBound="grdint_RowDataBound" CellPadding="0" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Transaction No.">
                        <ItemTemplate>

                            <asp:Label ID="lbINT" runat="server" Text='<%# Eval("trf_no") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="trf_dt" HeaderText="Transaction Date" ReadOnly="True" DataFormatString="{0:dd-MM-yyyy}" SortExpression="cashout_dt" />
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate>
                            <asp:Label ID="lbmanualno" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="status" HeaderText="Status" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton5" CssClass="button2 print" OnClick="LinkButton5_Click" runat="server" CausesValidation="False" CommandName="Select" Text="Intenal Transfer"></asp:LinkButton>&nbsp;                  
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle CssClass="table-edit" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>

    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
    <div>
        <asp:Button ID="btlookup" style="display:none" OnClientClick="ShowProgress();" OnClick="btlookup_Click" runat="server" Text="Button" />
    </div>
</asp:Content>
