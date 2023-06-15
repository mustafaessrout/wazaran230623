<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_stockOpnamecheck.aspx.cs" Inherits="fm_lookup_stockOpnamecheck" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
 
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
   
 

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
     <script src="js/index.js"></script>
   <script>
       function closewin() {
           window.opener.updpnl();
           window.close();
       }
        </script>
   
  


<body >
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>
            <div class="divheader">Search Stock Opname Check</div>
            <div class="h-divider"></div>
       
            <div class="clearfix margin-bottom">
                <label class="col-md-1 col-sm-2">Search</label>
                <div class="col-md-5 col-sm-4 input-group">
                    <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                    </div>
                </div>
            </div>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="Stk Opnm No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbstock_no" runat="server" Text='<%# Eval("stock_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbstock_dt" runat="server" Text='<%# Eval("stock_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Whs Type">
                                <ItemTemplate>
                                    <asp:label ID="lbwhs_typ" runat="server" Text='<%# Eval("whs_typ")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Whs CD">
                                <ItemTemplate>
                                    <asp:label ID="lbwhs_cd" runat="server" Text='<%# Eval("whs_cd")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bin CD">
                                <ItemTemplate>
                                    <asp:label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit"/>
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle   />
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

