<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_cashamountclosing.aspx.cs" Inherits="fm_lookup_cashamountclosing" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<link href="css/bootstrap.min.css" rel="stylesheet" />
<link href="css/custom/style.css" rel="stylesheet" />

<script src="js/jquery.min.js"></script>
<script src="js/jquery.scrollbar.js"></script>
<script src="js/jquery.floatThead.js"></script>
<script src="js/bootstrap.min.js"></script> 
<script src="js/index.js"></script>

<body >
    <form id="form1" runat="server">
    <div class="containers bg-white">
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>

        <div class="divheader">Search Cash Closing</div>
        <div class="h-divider"></div>
        

        <link href="css/anekabutton.css" rel="stylesheet" />
         <script>
            function closewin() {
                window.opener.updpnl();
                window.close();
            }
        </script>
        <div class="container">
            <div class="row">
                <label class="control-label col-sm-1 titik-dua">Search</label>
                <div class="col-sm-5 input-group" style="padding-left:15px;">
                    <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn btn-primary btn-search" Text="Search" OnClick="btsearch_Click" />
                    </div>
                </div>
            </div>
            <div class="row margin-top">
                <div class="overflow-y" style="height:470px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="table table-hover table-striped table-fix" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="CH Closing No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbchclosingno" runat="server" Text='<%# Eval("chclosingno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbchclosing_dt" runat="server" Text='<%# Eval("chclosing_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:label ID="lbAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:label ID="lbchclosing_description" runat="server" Text='<%# Eval("chclosing_description")%>'></asp:label>
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
                                <RowStyle />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        </div>
        
        <div style="text-align:center;padding:10px">
        </div>
    </form>
</body>

