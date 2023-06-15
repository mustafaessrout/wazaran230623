<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_tranStock.aspx.cs" Inherits="fm_lookup_tranStock" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="css/bootstrap.min.css" rel="stylesheet" />
<link href="css/anekabutton.css" rel="stylesheet" />
<link href="css/font-face/khula.css" rel="stylesheet"/>
<link href="css/font-awesome.min.css" rel="stylesheet" />
<link href="css/custom/style.css" rel="stylesheet" />

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

<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            
            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>
            <div class="divheader">Search Cash Closing</div>
            <div class="h-divider"></div>
            <div class="container-fluid">
                <div class="row">
                    <label class="control-label col-sm-1 titik-dua">Search</label>
                    <div class="col-sm-5 input-group" style="padding-left:15px;">
                        <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:Button ID="btsearch" runat="server" CssClass="btn btn-primary btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>
                    </div>
                </div>
                <div class="margin-top row">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="Tran No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtrnstkNo" runat="server" Text='<%# Eval("trnstkNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tran Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbinvName" runat="server" Text='<%# Eval("invName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtrnstkDate" runat="server" Text='<%# Eval("trnstkDate","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Whs CD">
                                        <ItemTemplate>
                                            <asp:label ID="lbwhs_CD" runat="server" Text='<%# Eval("whs_CD")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bin CD">
                                        <ItemTemplate>
                                            <asp:label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:label ID="lbsta_nm" runat="server" Text='<%# Eval("sta_nm")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
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

