<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashoutconfirmation.aspx.cs" Inherits="fm_cashoutconfirmation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <h4 class="jajarangenjang">Cashout (Non Routine)- Confirmation</h4>
        <div class="h-divider"></div>

        <div id="listcash" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">                
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdcash" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdcash_PageIndexChanging" ShowFooter="False" CellPadding="0" OnRowDataBound="grdcash_RowDataBound" OnRowCommand="grdcash_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salespoint">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> - <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sys No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcashout_cd" runat="server" Text='<%#Eval("cashout_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcashout_dt" runat="server" Text='<%#Eval("cashout_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lbempnm" runat="server" Text='<%#Eval("empName") %>'></asp:Label> - <asp:Label ID="lbdept_cd" runat="server" Text='<%#Eval("dept_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lbremark" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtotamt" runat="server" Text='<%#Eval("total") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cashout Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitemco_nm" runat="server" Text='<%#Eval("itemco_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manual No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbmanual_no" runat="server" Text='<%#Eval("manualno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lbreason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Document">
                                            <ItemTemplate>
                                                <div  class="data-table-popup">
                                                    <a class="example-image-link" href="/images/cashout/<%# Eval("cashoutfile") %>" data-lightbox="example-1<%# Eval("cashoutfile") %>">
                                                        <asp:Label ID="lbfileloc" runat="server" Text='Download'></asp:Label>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

