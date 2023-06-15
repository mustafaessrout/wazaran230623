<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_goodreceiptHO.aspx.cs" Inherits="fm_lookup_goodreceiptHO" %>

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
<body  >
    <form id="form1" runat="server" class="center">
        <div class="containers bg-white" style="max-width:760px;">
            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>
            <div class="divheader">Search Receipt Retur HO</div>
            <div class="h-divider"></div>
            <div class="row" >
                <div class="clearfix margin-bottom  ">
                    <div class="col-sm-6 clearfix">
                        <label class="control-label col-sm-2">Search </label>
                        <div class="input-group col-sm-10">
                            <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                            </div>
                        </div>
                   </div>
                </div>

                <div class="clearfix">
                    <div class="col-xs-12 center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="overflow-y" style="height:470px;width:700px;">
                            <ContentTemplate>
                                <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-fix mygrid"  AutoGenerateColumns="False"  OnSelectedIndexChanged="grd_SelectedIndexChanged" CellPadding="0" GridLines="None">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="GRHO No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbreceipt_no" runat="server" Text='<%# Eval("receipt_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbreceipt_dt" runat="server" Text='<%# Eval("receipt_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Warehouse">
                                            <ItemTemplate>
                                                <asp:label ID="lbwhs_nm" runat="server" Text='<%# Eval("whs_nm")%>'></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Branch">
                                            <ItemTemplate>
                                                <asp:label ID="lbto_po_branch" runat="server" Text='<%# Eval("to_po_branch")%>'></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:label ID="lbref_no" runat="server" Text='<%# Eval("ref_no")%>'></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit"/>
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
                </div>

                <div style="text-align:center;padding:10px">
                </div>
            </div>
        </div>
    </form>
</body>

