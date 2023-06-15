<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AccManualJournal_List.aspx.cs" Inherits="fm_accManualJournal_List" %>
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
    <div class="divheader">Manual Journal List</div>

    <div class="h-divider"></div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <asp:Button ID="btaddjournal" runat="server" CssClass="btn-warning btn btn-save" OnClick="btaddjournal_Click" Text="Add Journal" OnClientClick="javascript:ShowProgress();"/>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
                <ContentTemplate>
                    <div class="overflow-y" style="max-height:530px; width:100%;">
                    <%--<div class="overflow-y" style="width:100%;">--%>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating"  CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("no") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Journal Det No.">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lblidjournal" runat="server" Text='<%# Eval("id_journal") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Module">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        </span><asp:Label ID="id_module" runat="server" Text='<%# Eval("id_module") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="tran_date" runat="server" Text='<%# Eval("tran_date_f") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Period">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="period" runat="server" Text='<%# Eval("period") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="Description Detail">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <%# Eval("description") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Debit">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblTotalDebit" runat="server" Text='<%# Eval("tot_debit","{0:n2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
        
                                <asp:TemplateField HeaderText="Total Credit">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblTotalCredit" runat="server" Text='<%# Eval("tot_credit","{0:n2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="status" runat="server" Text='<%# Eval("journal_status") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <%--<asp:LinkButton ID="btview" OnClientClick="javascript:disp();" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btview_Click">View</asp:LinkButton>--%>
                                        <%--<a href='fm_AccManualJournal.aspx?idJournal='<%# Eval("id_journal") %>'&sta=A'>Approve</a>--%>
                                        <asp:Label ID="viewJournal" runat="server" >
                                            <a href='fm_AccManualJournal.aspx?idJournal=<%# Eval("id_journal") %>'>View</a>
                                        </asp:Label>
                                        </div>
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

    </br></br>

    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

