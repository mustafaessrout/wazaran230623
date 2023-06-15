<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_SalesTargetSP.aspx.cs" Inherits="fm_lookup_SalesTargetSP" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Search Sales Target Branch</strong>
        <link href="css/anekabutton.css" rel="stylesheet" />
        <script>
            function closewin2() {
                window.opener.updpnl2();
                window.close();
            }
        </script>
        <div>Search :&nbsp;
            <asp:TextBox ID="txsearch" runat="server"></asp:TextBox>
            <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbslsTargetSPID" runat="server" Text='<%# Eval("slsTargetSPID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalesSP Code">
                            <ItemTemplate>
                                <asp:Label ID="lbslsTargetSPCD" runat="server" Text='<%# Eval("slsTargetSPCD") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="RefID">
                            <ItemTemplate>
                                <asp:Label ID="lbrefSlsTargetDetID" runat="server" Text='<%# Eval("refSlsTargetDetID")%>'></asp:Label>
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

