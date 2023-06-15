<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="lookup_acccndn.aspx.cs" Inherits="lookup_acccndn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<head>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />

    <style>
        .table-header {
            font-weight: normal;
        }
    </style>

    <script>
        function closewin() {
            window.opener.updpnl();
            window.close();
        }
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>

</head>
<body style="font-family: Tahoma,Verdana; font-size: small">
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="clearfix margin-bottom">
                <asp:ToolkitScriptManager ID="tsmanager" runat="server">
                </asp:ToolkitScriptManager>
                <div class="divheader">Search CN/DN</div>
                <div class="h-divider"></div>

                <div class="clearfix">
                    <label class="control-label col-md-1">HO Ref Number</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtRefNo" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Invoice Number</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtInvoice" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Customer</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtCustomer" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txtCustomer" EnableCaching="false"
                            FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="CustSelected"
                            ServiceMethod="GetCompletionList" UseContextKey="True" ContextKey="true">
                        </asp:AutoCompleteExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn-primary btn search" OnClick="btnSearch_Click">Search</asp:LinkButton>
                    </div>
                </div>
                <div class=" row">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdcust" runat="server" />

                            <asp:GridView ID="grd" CssClass="mGrid " runat="server" AutoGenerateColumns="False" CellPadding="0"
                                ForeColor="#333333" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sys No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCNDN_no" runat="server" Text='<%#Eval("cndn_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_no" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HO Ref No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefho_no" runat="server" Text='<%#Eval("refho_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manual Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanual_no" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Man">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsalesMan" runat="server" Text='<%#Eval("salesMan") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatusVal" runat="server" Text='<%#Eval("statusVal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("totamt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CN DN Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotVat" runat="server" Text='<%#Eval("inv_CNDNAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Inv Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("inv_Balance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="navi row margin-bottom">
                    <asp:LinkButton ID="btClose" runat="server" CssClass="btn btn-primary" OnClick="btClose_Click">Close</asp:LinkButton>
                </div>

            </div>
    </form>
</body>

