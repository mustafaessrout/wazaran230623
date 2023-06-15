<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_po.aspx.cs" Inherits="fm_lookup_po" %>

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
                window.opener.updpnl3();
                window.close();
            }
        </script>

<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <asp:ToolkitScriptManager ID="tsmanager" runat="server">
            </asp:ToolkitScriptManager>
   
            <div class="divheader">Search PO</div>
            <div class="h-divider"></div>
       
            <div class="clearfix margin-bottom">
                <table>
                    <tr>
                        <th>Salespoint</th>
                        <th>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </th>
                    </tr>
                </table>
            </div>

            <div class="clearfix margin-bottom">
                <table>
                    <tr>
                        <th>Search</th>
                        <th>
                            <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>   
                        </th>
                        <th>
                            <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </th>
                        <th>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                            </div>
                        </th>
                    </tr>
                </table>
            </div>

            <div class="clearfix center">
                <div class="overflow-y" style="width:700px; height:480px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-fix mygrid" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="PO No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpo_no" runat="server" Text='<%# Eval("po_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpo_dt" runat="server" Text='<%# Eval("po_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Nm">
                                        <ItemTemplate>
                                            <asp:label ID="lbvendor_nm" runat="server" Text='<%# Eval("vendor_nm")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:label ID="lbpo_sta_nm" runat="server" Text='<%# Eval("po_sta_nm")%>'></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <HeaderStyle CssClass="table-header"/>
                                <FooterStyle CssClass="table-footer" />
                                <EditRowStyle CssClass="table-edit" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  />
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

