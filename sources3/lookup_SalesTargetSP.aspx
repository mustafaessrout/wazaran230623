<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="lookup_SalesTargetSP.aspx.cs" Inherits="lookup_SalesTargetSP" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Search Sales Target Branch</strong>
        <link href="css/anekabutton.css" rel="stylesheet" />
        <script>
            function closewin() {
                window.opener.updpnl();
                window.close();
            }
        </script>
        <div>Search :&nbsp;
            <asp:TextBox ID="txsearch" runat="server"></asp:TextBox>
            <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grd_SelectedIndexChanged" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="divgrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Target No">
                            <ItemTemplate>
                                <asp:Label ID="lbtarget_no" runat="server" Text='<%# Eval("target_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period">
                            <ItemTemplate>
                                <asp:Label ID="lbperiod" runat="server" Text='<%# Eval("period") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman CD">
                            <ItemTemplate>
                                <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman Name">
                            <ItemTemplate><%# Eval("emp_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalespointCD">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespointCD" runat="server" Text='<%# Eval("salespointCD")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
              <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="text-align:center;padding:10px">
        </div>
    </form>
</body>

