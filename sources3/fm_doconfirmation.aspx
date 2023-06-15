<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_doconfirmation.aspx.cs" Inherits="fm_doconfirmation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <h4 class="jajarangenjang">DO Modification - Confirmation</h4>
        <div class="h-divider"></div>

        <div id="listcash" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">                
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grddo" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grddo_PageIndexChanging" ShowFooter="False" CellPadding="0" OnRowDataBound="grddo_RowDataBound" OnRowCommand="grddo_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salespoint">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> - <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DO No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbdono" runat="server" Text='<%#Eval("do_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbpono" runat="server" Text='<%#Eval("po_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manual No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbmanual" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbrefno" runat="server" Text='<%#Eval("docref_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbpayment_dt" runat="server" Text='<%#Eval("do_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                        <asp:TemplateField>
                                            <ItemTemplate>                
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-default" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>          
                                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

