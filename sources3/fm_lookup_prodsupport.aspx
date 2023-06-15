<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_prodsupport.aspx.cs" Inherits="fm_lookup_prodsupport" %>

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
 
            <script>
                function closewin() {
                    window.opener.updpnl();
                    window.close();
                }
            </script>

<body  >
    <form id="form1" runat="server" class="center">
        <div class="containers bg-white" style="width:800px;">
            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>
            <div class="divheader">Search Prod Support</div>
            <div class="h-divider"></div>
            
            <div class="clearfix margin-bottom">
                <label class="control-label col-md-1 col-sm-2">Search</label>
                <div class="input-group col-md-5 col-sm-4">
                    <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                    </div>
                </div>
            </div>
            
            <div class="clearfix">
                <div class="overflow-y" style="width:750px; height:480px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-fix table-striped mygrid" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="Support No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsup_no" runat="server" Text='<%# Eval("sup_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsup_dt" runat="server" Text='<%# Eval("sup_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Tran No.">
                                        <ItemTemplate>
                                            <asp:label ID="lbtran_no" runat="server" Text='<%# Eval("tran_no")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:label ID="lbdescription" runat="server" Text='<%# Eval("description")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:label ID="lbsta_nm" runat="server" Text='<%# Eval("sta_nm")%>'></asp:label>
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
                </div>
                
            </div>
            
            <div style="text-align:center;padding:10px">
            </div>

        </div>
    </form>
</body>

