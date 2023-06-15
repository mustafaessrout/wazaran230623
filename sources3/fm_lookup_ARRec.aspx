<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_ARRec.aspx.cs" Inherits="fm_lookup_ARRec" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Search Receipt</strong>
        <link href="css/anekabutton.css" rel="stylesheet" />
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Receipt No.">
                            <ItemTemplate>
                                <asp:Label ID="lbARRecCD" runat="server" Text='<%# Eval("ARRecCD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receipt Date">
                            <ItemTemplate>
                                <asp:Label ID="lbrecDate" runat="server" Text='<%# Eval("recDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate>
                                <asp:label ID="lbemp_nm" runat="server" Text='<%# Eval("emp_nm")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible=false>
                            <ItemTemplate>
                                <asp:label ID="lbsalespointCD" runat="server" Text='<%# Eval("salespointCD")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible=false>
                            <ItemTemplate>
                                <asp:label ID="lbARRecID" runat="server" Text='<%# Eval("ARRecID")%>'></asp:label>
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

