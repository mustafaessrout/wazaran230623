<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_investigation.aspx.cs" Inherits="fm_lookup_investigation" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Search Investigation</strong>
        <link href="css/anekabutton.css" rel="stylesheet" />
        <script>
            function closewin() {
                window.opener.updpnl();
                window.close();
            }
        </script>
        <div>Salespoint CD : <asp:Label ID="lbsalespointcd" runat="server"></asp:Label> Search :&nbsp;
            <asp:TextBox ID="txsearch" runat="server"></asp:TextBox>
            <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tran No.">
                            <ItemTemplate>
                                <asp:Label ID="lbivgno" runat="server" Text='<%# Eval("ivgno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tran Type">
                            <ItemTemplate>
                                <asp:Label ID="lbinvName" runat="server" Text='<%# Eval("invName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbivgdate" runat="server" Text='<%# Eval("ivgdate","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:label ID="lbivgemp_cd" runat="server" Text='<%# Eval("ivgemp_cd")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:label ID="lbivgreason" runat="server" Text='<%# Eval("ivgreason")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:label ID="lbsta_nm" runat="server" Text='<%# Eval("sta_nm")%>'></asp:label>
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

