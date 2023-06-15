<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupinvreturnApproval.aspx.cs" Inherits="lookupinvreturnApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Invoice Return</title>

    <link href="css/lightbox.css" rel="stylesheet" />
    <script src="css/lightbox-plus-jquery.min.js"></script>
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/styles.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <link rel="stylesheet" href="css/jquery.scrollbar.css" />
    <script src="js/jquery.scrollbar.js"></script>
    <script src="js/modernizr.custom.js"></script>

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/jquery.mCustomScrollbar.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ScriptManagerTK" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="js/AutoLostFocus.js" />
            </Scripts>
        </asp:ToolkitScriptManager>
        <div class="containers bg-white">
            <div class="divheader">List Of Invoice Can be Full Returned</div>
            <div class="h-divider"></div>

            <div class="row ">
                <div class="form-group">

                    <div class="col-md-3">
                        <asp:Label ID="rbInvoiceType" runat="server" Text="Search By Invoice Canceled Status"></asp:Label>

                    </div>
                    <div class="col-md-6  drop-down">


                        <asp:DropDownList ID="cbInvoiceType" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbInvoiceType_SelectedIndexChanged"></asp:DropDownList>

                    </div>

                </div>

                <div class="margin-bottom clearfix">
                    <div class="col-md-12">
                        <div class="overflow-y" style="max-height: 400px">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grd" EmptyDataText="New Records Found" CssClass="table table-striped table-fix mygrid" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="2" OnPageIndexChanging="grd_PageIndexChanging" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Inv No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Status">
                                                <ItemTemplate><%# Eval("invStatus") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Man No">
                                                <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approval Status">
                                                <ItemTemplate><%# Eval("AppStatus") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reject Info">
                                                <ItemTemplate><%# Eval("rejectInfo") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate><%#Eval("inv_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cust">
                                                <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salesman">
                                                <ItemTemplate><%# Eval("emp_desc") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amt">
                                                <ItemTemplate><%# Eval("balance") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate><%#Eval("so_cd") %></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle CssClass="table-edit" />
                                        <FooterStyle CssClass="table-footer" />
                                        <HeaderStyle CssClass="table-header" />
                                        <PagerStyle CssClass="table-page" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
