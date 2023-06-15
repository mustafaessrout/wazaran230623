<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cndnCustomerApproved.aspx.cs" Inherits="fm_cndnCustomerApproved" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
       <div class="alert alert-info text-bold">Approval DN Customer</div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Salespoint</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbsalespoint" AutoPostBack="true"  onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-1">Approval Name</label>
                    <div class="col-sm-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdcndnCust" runat="server" />
                                <asp:DropDownList ID="ddlApprove" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="ddlApprove_SelectedIndexChanged">
                                    <asp:ListItem Text="DN Customer" Value="CNDNCust" Selected="True"></asp:ListItem>
                                    <%--<asp:ListItem Text="CN DN Adjustment" Value="CNDNAdj"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server" OnSelectedIndexChanging="grd_SelectedIndexChanging" AutoGenerateColumns="False" CellPadding="2" Width="100%" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit" EmptyDataText="No CNDN need approve found !!" ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DN Customer">
                                <ItemTemplate>
                                    <asp:Label ID="lblCNDN_no" runat="server" Text='<%# Eval("cndn_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DN Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status_nm") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfstatus" runat="server" Value='<%# Eval("cndncust_sta_id") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("Customer") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DN Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <%#Eval("status_nm") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="drop-down">
                                          <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                                    </div>
                                  
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <asp:LinkButton ID="btnApproved" runat="server" CssClass="btn btn-success" Visible="false" style="display:none" OnClick="btnApproved_Click"><i class="fa fa-print">&nbsp;Approved</i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

