<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_receiptReturHO.aspx.cs" Inherits="fm_lookup_receiptReturHO" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script>
        function closewin() {
            window.opener.updpnl();
            window.close();
        }
    </script>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">Search Receipt Return HO</div>
            <div class="h-divider"></div>
            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>
        
            <div class="clearfix margin-bottom">
                <div class="col-md-6 col-sm-8">
                    <label class="control-label col-sm-2">Search</label>
                    <div class="col-sm-10 input-group">
                        <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="Rec Ret No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbrecRetHO_no" runat="server" Text='<%# Eval("recRetHO_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbrecRetHO_dt" runat="server" Text='<%# Eval("recRetHO_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual No.">
                                <ItemTemplate>
                                    <asp:label ID="lbrecRetHO_manual_no" runat="server" Text='<%# Eval("recRetHO_manual_no")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Return Brn No.">
                                <ItemTemplate>
                                    <asp:label ID="lbreturho_no" runat="server" Text='<%# Eval("returho_no")%>'></asp:label>
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

