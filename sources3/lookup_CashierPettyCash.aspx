<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="lookup_CashierPettyCash.aspx.cs" Inherits="lookup_CashierPettyCash" %>

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
       
        function EmployeeSelected(sender, e) {
            $get('<%=hdfEmployee.ClientID%>').value = e.get_value();
            $get('<%=btShowItemCashout.ClientID%>').click();
        }
    </script>

</head>
<body style="font-family: Tahoma,Verdana; font-size: small">
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="clearfix margin-bottom">
                <asp:ToolkitScriptManager ID="tsmanager" runat="server">
                </asp:ToolkitScriptManager>
                <div class="divheader">Search Cash Out Request</div>
                <div class="h-divider"></div>

                <div class="clearfix">
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfEmployee" runat="server" />
                                <div class="input-group">
                                    <div>
                                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" CompletionListElementID="divwEmployee" runat="server"
                                            TargetControlID="txtEmployee" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="EmployeeSelected" ServiceMethod="GetCompletionListEmployee" UseContextKey="True"
                                            ContextKey="true" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover"
                                            CompletionListItemCssClass="auto-complate-item">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btShowItemCashout" runat="server" OnClick="btShowItemCashout_Click" Text="Button" Style="display: none" />

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
                <div class=" row">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            

                            <asp:GridView ID="grd" CssClass="mGrid " runat="server" AutoGenerateColumns="False" CellPadding="0"
                                ForeColor="#333333" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                     <asp:TemplateField HeaderText="Transaction No">
                                         <ItemTemplate>
                                            <asp:Label ID="lbldoc_cd" runat="server" Text='<%#Eval("doc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sys No">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfemp_cd" runat="server" Value='<%#Eval("emp_cd") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="hdfdoc_cd" runat="server" Value='<%#Eval("doc_cd") %>'></asp:HiddenField>
                                            <asp:Label ID="lblitemco_cd" runat="server" Text='<%#Eval("itemco_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldebit" runat="server" Text='<%#Eval("debit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Date">
                                        <ItemTemplate>
                                            <%# Eval("transactionDate","{0:d/M/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        </div>
    </form>
</body>

