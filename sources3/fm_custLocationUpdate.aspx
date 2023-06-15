<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_custLocationUpdate.aspx.cs" Inherits="fm_custLocationUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

    </script>

    <style>
        .chkBox label {
            margin-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <%--<div class="form-horizontal">--%>
        <h4 class="jajarangenjang">Customer Location Update</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">

                <label class="col-md-1 col-sm-2 control-label">Branch</label>
                <div class="col-md-3 col-sm-4">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSalespoint" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlSalespoint_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="col-md-1 col-sm-2 control-label">Salesman</label>
                <div class="col-md-3 col-sm-4">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSalesman" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesman_SelectedIndexChanged" Enabled="false">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="hdsalesman" runat="server" />
                </div>

                <div class="col-md-3 col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="checkbox-block-center">
                        <ContentTemplate>
                            <asp:CheckBox ID="chkCust" runat="server" AutoPostBack="True" Enabled="false" OnCheckedChanged="chkCust_CheckedChanged" CssClass="chkBox" Text="ALL Customers"></asp:CheckBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="col-md-1 col-sm-2 control-label">Customer</label>
                <div class="col-md-3 col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <%--<asp:DropDownList ID="cbreceive" runat="server" CssClass="form-control input-sm" Visible="False"></asp:DropDownList>--%>
                            <asp:TextBox ID="txCustomer" runat="server" CssClass="form-control input-sm" Enabled="false" OnTextChanged="txCustomer_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txCustomer_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txCustomer" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" CompletionInterval="1" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true">
                            </ajaxToolkit:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="hdcust" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align: center">
            <asp:LinkButton ID="btnShowData" runat="server" CssClass="btn btn-success" OnClick="btnShowData_Click"><i class="fa fa-print">&nbsp;Show Data</i></asp:LinkButton>
        </div>
    </div>


    <div class="container-fluid">
        <div class="row">
            <div class="form-group">
                <div id="tbl" runat="server" class="table">
                    <asp:GridView ID="grd" runat="server" OnSelectedIndexChanging="grd_SelectedIndexChanging" AutoGenerateColumns="False" CellPadding="2" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer No.">
                                <ItemTemplate>
                                    <asp:Label ID="cust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="cust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Group">
                                <ItemTemplate>
                                    <asp:Label ID="custGroup" runat="server" Text='<%# Eval("custGroup") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Type">
                                <ItemTemplate>
                                    <asp:Label ID="custType" runat="server" Text='<%# Eval("custType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Address">
                                <ItemTemplate>
                                    <asp:Label ID="custAddr" runat="server" Text='<%# Eval("Addr") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="latitude">
                                <ItemTemplate>
                                    <asp:Label ID="custlatitude" runat="server" Text='<%# Eval("latitude") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="longitude">
                                <ItemTemplate>
                                    <asp:Label ID="custlongitude" runat="server" Text='<%# Eval("longitude") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
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
                    <div class="col-md-12" style="text-align: center">
                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-success" Visible="false" OnClick="btnUpdate_Click"><i class="fa fa-print">&nbsp;Update</i></asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
    </div>

    

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
    <%--</div>--%>
</asp:Content>

