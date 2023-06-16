<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_internalTranfer.aspx.cs" Inherits="fm_lookup_internalTranfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script src="js/jquery.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.floatThead.js"></script>
<script src="js/index.js"></script>
<link href="css/bootstrap.min.css" rel="stylesheet" />
<link href="css/anekabutton.css" rel="stylesheet" />
<link href="css/font-awesome.min.css" rel="stylesheet" />
<link href="css/custom/metro.css" rel="stylesheet" />
<link href="css/custom/style.css" rel="stylesheet" />
<link href="css/font-face/khula.css" rel="stylesheet" />
<link href="css/font-awesome.min.css" rel="stylesheet" />
<script>
    function closewin() { 
        window.opener.updpnl();
        window.close();
    }
</script>
<script>
    function ShowProgress() {
        $('#pnlmsg').show();
    }

    function HideProgress() {
        $("#pnlmsg").hide();
        return false;
    }


</script>


<body>
    <form id="form1" runat="server" class="no-margin-bottom center">
        <div class="containers bg-white" style="width: 800px;">
            <asp:ToolkitScriptManager ID="tsmanager" runat="server"></asp:ToolkitScriptManager>
            <div class="divheader">Search Internal Tranfer</div>
            <div class="h-divider"></div>

            <div class="clearfix margin-bottom">
                <table>
                    <tr>
                        <th>Search</th>
                        <th>
                            <asp:TextBox ID="txsearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        </th>
                        <th class="drop-down">
                            <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </th>
                        <th>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" OnClientClick="ShowProgress();" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                            </div>
                        </th>
                    </tr>
                </table>
            </div>

            <div class="">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="overflow-y" style="height: 100%; width: 100%;">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grd_SelectedIndexChanged" CellPadding="0" CssClass="table table-striped table-fix mygrid" AllowPaging="True" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" PageSize="20">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Ldn No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldn_seq" runat="server" Text='<%# Eval("ldn_seq") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tranfer No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtrf_no" runat="server" Text='<%# Eval("trf_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtrf_dt" runat="server" Text='<%# Eval("trf_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Whs From">
                                    <ItemTemplate>
                                        <asp:Label ID="lbwhs_cd_from" runat="server" Text='<%# Eval("whs_cd_from")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bin From">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbin_from" runat="server" Text='<%# Eval("bin_from")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Whs To">
                                    <ItemTemplate>
                                        <asp:Label ID="lbwhs_cd_to" runat="server" Text='<%# Eval("whs_cd_to")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bin To">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbin_to" runat="server" Text='<%# Eval("bin_to")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsta_id" runat="server" Text='<%# Eval("sta_id")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbstatus_nm" runat="server" Text='<%# Eval("status_nm")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tab No.">
                                    <ItemTemplate>
                                        <%# Eval("tab_no") %>
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

            <div style="text-align: center; padding: 10px">
            </div>
        </div>
        <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
    </form>
</body>

