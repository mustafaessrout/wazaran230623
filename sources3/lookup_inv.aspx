<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_inv.aspx.cs" Inherits="lookup_inv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
<link href="css/anekabutton.css" rel="stylesheet" />
<link href="css/font-awesome.min.css" rel="stylesheet" />
<link href="css/custom/metro.css" rel="stylesheet" />
<link href="css/custom/style.css" rel="stylesheet" />
<link href="css/font-face/khula.css" rel="stylesheet"/>


<script src="js/jquery.min.js"></script>
<script src="js/bootstrap.min.js"></script> 
<script src="js/jquery.floatThead.js"></script>
<script src="js/index.js"></script>

    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">List Of Invoice are not received back</div>
            <div class="h-divider"></div>


            <div class="clearfix">
                <div class="col-sm-6 no-padding clearfix margin-bottom">
                    <div class="input-group">
                        <asp:TextBox ID="txinv" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <div class="input-group-btn">
                             <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn-sm btn btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>
                    </div>
                    
                </div>
                <div class="margin-top">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="Invoice No.">
                                    <ItemTemplate>
                                       <a href="javascript:window.opener.SearchInvoice('<%# Eval("inv_no")%>');window.close();"> <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manual No.">
                                    <ItemTemplate>
                                        <%# Eval("manual_no") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Inv">
                                    <ItemTemplate>
                                        <%# Eval("inv_dt","{0:d/M/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust Code">
                                    <ItemTemplate>
                                        <%# Eval("cust_cd") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust Name">
                                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Addr">
                                    <ItemTemplate><%# Eval("addr") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate><%# Eval("city_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Driver">
                                    <ItemTemplate><%# Eval("driver_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Driver Name">
                                    <ItemTemplate><%# Eval("driver_nm") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle  CssClass="table-edit"  />
                        </asp:GridView>
                    </div>
            </div>
        </div>
    </form>
</body>
</html>
