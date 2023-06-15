<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AccCashAdvanceApproval_List.aspx.cs" Inherits="fm_AccCashAdvanceApproval_List" %>
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
            <%--document.getElementById('<%=bttmp.ClientID%>').click();--%>
            return (false);
        }
    </script>
    <script>
        function Selecteditem(sender, e) {
            //$get('<//%=//hditem.ClientID%//>').value = e.get_value();
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
    <div class="divheader">Cash Advance Approval List</div>

    <div class="h-divider"></div>
    
    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
                <ContentTemplate>
                    <div class="overflow-y" style="max-height:530px; width:100%;">
                    <%--<div class="overflow-y" style="width:100%;">--%>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Document No.">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        <asp:Label ID="lblcashoutcd" runat="server" Text='<%# Eval("cashout_cd") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Cashout Date">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        <asp:Label ID="lblcashoutdt" runat="server" Text='<%# Eval("cashout_dt") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Employee">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblemp" runat="server" Text='<%# Eval("emp_nmX")+" - "+Eval("emp_cdX") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblapproval" runat="server" Text='<%# Eval("approval_cd") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblamt" runat="server" Text='<%# Eval("amt") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblvat" runat="server" Text='<%# Eval("vat_amt") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Advance Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lbltotamt" runat="server" Text='<%# Eval("tot_cash_advance") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Label ID="processPage" runat="server" >
                                            <a href='fm_AccCashAdvanceApproval.aspx?doc_no=<%# Eval("cashout_cd") %>'>Process To Approve Or Reject</a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />--%>
                                <%--<asp:CommandField ShowDeleteButton="True"/>--%>
                                <%--<asp:CommandField ShowSelectButton="True"/>--%>
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

    <br/><br/><br/>

    <div class="divheader">Cash Advance Settlement Approval List</div>

    <div class="h-divider"></div>
    
    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
                    <div class="overflow-y" style="max-height:530px; width:100%;">
                    <%--<div class="overflow-y" style="width:100%;">--%>
                        <asp:GridView ID="grd2" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Document No.">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        <asp:Label ID="lblcashoutcd" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Cashout Date">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        <asp:Label ID="lblcashoutdt" runat="server" Text='<%# Eval("transaction_dt") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Employee">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblemp" runat="server" Text='<%# Eval("emp_nmX")+" - "+Eval("emp_cdX") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblapproval" runat="server" Text='<%# Eval("approval_cd") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblamt" runat="server" Text='<%# Eval("amt") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblvat" runat="server" Text='<%# Eval("vat_amt") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount Returned">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblamtreturned" runat="server" Text='<%# Eval("amt_returned") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Advance Settlement Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lbltotamt" runat="server" Text='<%# Eval("tot_cash_advance_settlement") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Label ID="processPage" runat="server" >
                                            <a href='fm_AccCashAdvanceApproval.aspx?doc_no=<%# Eval("doc_no") %>'>Process To Approve Or Reject</a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />--%>
                                <%--<asp:CommandField ShowDeleteButton="True"/>--%>
                                <%--<asp:CommandField ShowSelectButton="True"/>--%>
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
            <div class="table-copy-page-fix" id="copy-fst2"></div>
        </div>
    </div>

    <br/><br/><br/>
    
    <div class="divheader">Cash Advance Settlement to be Claimed Approval List</div>

    <div class="h-divider"></div>
    
    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                <ContentTemplate>
                    <div class="overflow-y" style="max-height:530px; width:100%;">
                    <%--<div class="overflow-y" style="width:100%;">--%>
                        <asp:GridView ID="grd3" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Document No.">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        <asp:Label ID="lblcashoutcd" runat="server" Text='<%# Eval("claimco_cd") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Cashout Date">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        <asp:Label ID="lblcashoutdt" runat="server" Text='<%# Eval("transaction_dt") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Employee">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblemp" runat="server" Text='<%# Eval("emp_nmX")+" - "+Eval("emp_cdX") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblapproval" runat="server" Text='<%# Eval("approval_cd") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblamt" runat="server" Text='<%# Eval("amt") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lblvat" runat="server" Text='<%# Eval("vat_amt") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Advance Claimable Amount">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                        </span><asp:Label ID="lbltotamt" runat="server" Text='<%# Eval("tot_cash_advance_settlement_claim") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Label ID="processPage" runat="server" >
                                            <a href='fm_AccCashAdvanceApproval.aspx?doc_no=<%# Eval("claimco_cd") %>'>Process To Approve Or Reject</a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />--%>
                                <%--<asp:CommandField ShowDeleteButton="True"/>--%>
                                <%--<asp:CommandField ShowSelectButton="True"/>--%>
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
            <div class="table-copy-page-fix" id="copy-fst3"></div>
        </div>
    </div>

    <br/><br/><br/>
    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

