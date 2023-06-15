<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_return1.aspx.cs" Inherits="fm_lookup_return1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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

    <script>
        function closewin(sRetNo) {
            window.opener.SelectReturn(sRetNo);
            window.close();
        }
            </script>
</head>
<body >
    <form id="form1" runat="server">
        <div class="containers bg-white">

            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>

            <div class="divheader">Search Return</div>
            <div class="h-divider"></div>
            <div class="margin-bottom clearfix">
                <label class="col-md-1 col-sm-2">Search</label>
                <div class="col-md-5 col-sm-4 drop-down">
                    <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="">
                    <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                </div>
            </div>
            <div class="divheader margin-bottom">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="table table-striped mygrid"  AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15"  GridLines="None">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="RTV No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbreturno" runat="server" Text='<%# Eval("retur_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual No">
                                <ItemTemplate>
                                    <%# Eval("manual_no") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RTV Tab No.">
                                <ItemTemplate><%# Eval("tab_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return Date">
                                <ItemTemplate><%# Eval("retur_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Cd">
                                <ItemTemplate><%# Eval("cust_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbcust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate>
                                    <asp:label ID="lbemp_nm" runat="server" Text='<%# Eval("emp_nm") %>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer"/>
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit"/>
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                </div>
            <div style="text-align:center;padding:10px">
            </div>
            
        </div>
    </form>
</body>
</html>
