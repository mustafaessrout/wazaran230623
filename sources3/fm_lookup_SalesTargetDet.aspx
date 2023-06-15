<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_SalesTargetDet.aspx.cs" Inherits="fm_lookup_SalesTargetDet" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

        <link href="css/anekabutton.css" rel="stylesheet" />
        
<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Search Sales Target Detail</strong>
        <script>
            function closewin() {
                window.opener.updpnl();
                window.close();
            }
        </script>
        <div>Search :&nbsp;
            <asp:TextBox ID="txsearch" runat="server"></asp:TextBox>
            <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
        </div>
        <table>
            <tr>
            <td colspan="2">
                   <asp:AutoCompleteExtender ID="acextend" runat="server" TargetControlID="txsearch" ServiceMethod="GetItemList" CompletionSetCount="1" 
                        MinimumPrefixLength="1" CompletionInterval="10" FirstRowSelected="false" EnableCaching="false" CompletionListCssClass="autocomp" UseContextKey="True">
                   </asp:AutoCompleteExtender>
               </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbslstargetDetID" runat="server" Text='<%# Eval("slstargetDetID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalesT Code">
                            <ItemTemplate>
                                <asp:Label ID="lbslsTargetCD" runat="server" Text='<%# Eval("slsTargetCD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period">
                            <ItemTemplate>
                                <asp:Label ID="lbmonthCD" runat="server" Text='<%# Eval("monthCD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalespointCD">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespointCD" runat="server" Text='<%# Eval("salespointCD")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand CD">
                            <ItemTemplate>
                                <asp:Label ID="lbprod_cd" runat="server" Text='<%# Eval("prod_cd")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand Name">
                            <ItemTemplate>
                                <asp:Label ID="lbprod_nm" runat="server" Text='<%# Eval("prod_nm")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Brand CD">
                            <ItemTemplate>
                                <asp:Label ID="lbprod_cd2" runat="server" Text='<%# Eval("prod_cd2")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Sub Brand Name">
                            <ItemTemplate>
                                <asp:Label ID="lbprod_nm2" runat="server" Text='<%# Eval("prod_nm2")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="qty">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:Label ID="lbUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="remark">
                            <ItemTemplate>
                                <asp:Label ID="lbremark" runat="server" Text='<%# Eval("remark")%>'></asp:Label>
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
        <div style="text-align:center;padding:10px">
        </div>
    </form>
</body>

