<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_custtransferconfirmation.aspx.cs" Inherits="fm_custtransferconfirmation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <h4 class="jajarangenjang">Customer Transfer - Confirmation</h4>
        <div class="h-divider"></div>

        <div id="listcash" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">                
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grd_PageIndexChanging" ShowFooter="False" CellPadding="0" OnRowCommand="grd_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salespoint">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> - <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtrfno" runat="server" Text='<%#Eval("trf_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtrfdt" runat="server" Text='<%#Eval("trf_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtype" runat="server" Text='<%#Eval("trf_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Old Salesman">
                                            <ItemTemplate>
                                                <asp:Label ID="lboldsalesmancd" runat="server" Text='<%#Eval("old_salesman") %>'></asp:Label> - <asp:Label ID="lboldsalesmannm" runat="server" Text='<%#Eval("old_salesman_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Salesman">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnewsalesmancd" runat="server" Text='<%#Eval("new_salesman") %>'></asp:Label> - <asp:Label ID="lbnewsalesmannm" runat="server" Text='<%#Eval("new_salesman_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbeffdt" runat="server" Text='<%#Eval("eff_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbenddt" runat="server" Text='<%#Eval("end_dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance to be transferred">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtotamt" runat="server" Text='<%#Eval("totamt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lbreason" runat="server" Text='<%#Eval("reason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcust" runat="server" Text='<%#Eval("cust_desc") %>'></asp:Label>
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

