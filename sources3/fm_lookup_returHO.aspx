<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_returHO.aspx.cs" Inherits="fm_lookup_returHO" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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

        <script>
            function closewin() {
                window.opener.updpnl();
                window.close();
            }
        </script>
<body>
    <form id="form1" runat="server">
    <div class="containers bg-white">
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <div class="divheader">Search Retur HO</div>
        <div class="h-divider"></div>
        <div class="clearfix">
            <label class="col-sm-1 control-label">Search </label>
            <div class="col-sm-5 input-group">
                <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                <div class="input-group-btn">
                    <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn search" Text="Search" OnClick="btsearch_Click" />
                </div>
            </div>
            
        </div>

        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Return No.">
                            <ItemTemplate>
                                <asp:Label ID="lbreturho_no" runat="server" Text='<%# Eval("returho_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbreturho_dt" runat="server" Text='<%# Eval("returho_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supervisor">
                            <ItemTemplate>
                                <asp:label ID="lbproductspv_cd" runat="server" Text='<%# Eval("productspv_cd")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Status CD">
                            <ItemTemplate>
                                <asp:label ID="lbretho_sta_id" runat="server" Text='<%# Eval("retho_sta_id")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Point CD">
                            <ItemTemplate>
                                <asp:label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="text-align:center;padding:10px">
        </div>
        </div>
    </form>
</body>

