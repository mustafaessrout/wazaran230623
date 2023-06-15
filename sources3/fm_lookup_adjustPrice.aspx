<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_adjustPrice.aspx.cs" Inherits="fm_lookup_adjustPrice" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="css/bootstrap.min.css" rel="stylesheet" />
<link href="css/custom/style.css" rel="stylesheet" />
<link href="css/font-face/khula.css" rel="stylesheet"/>
<link href="css/anekabutton.css" rel="stylesheet" />
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
        <div class="divheader">Search Adjust Price</div>
        <div class="h-divider"></div>
      
        <div class="clearfix">
            <div class="input-group col-sm-6">
                <asp:TextBox ID="txsearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                <div class="input-group-btn">
                    <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn-sm btn btn-search" Text="Search" OnClick="btsearch_Click" />
                </div>
            </div>
        </div>
    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" CssClass="table table-striped mygrid margin-top text-small" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="Adj No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbadjp_cd" runat="server" Text='<%# Eval("adjp_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbstart_dt" runat="server" Text='<%# Eval("start_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warehouse">
                                <ItemTemplate>
                                    <asp:label ID="lbadjp_type" runat="server" Text='<%# Eval("adjp_type")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
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

