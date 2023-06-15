<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_SalesOrderDisc.aspx.cs" Inherits="fm_lookup_SalesOrderDisc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Discount Calculation</strong>
        <link href="css/anekabutton.css" rel="stylesheet" />
        <script>
            function closewin() {
                window.opener.updpnl2();
                window.close();
            }
        </script>
       
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="disc cd">
                            <ItemTemplate>
                                <asp:Label ID="lbdisc_cd" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="discount mec">
                            <ItemTemplate>
                                <asp:Label ID="lbdiscount_mec" runat="server" Text='<%# Eval("discount_mec") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="item CD">
                            <ItemTemplate>
                                <asp:Label ID="lbitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free">
                            <ItemTemplate>
                                <asp:Label ID="lbfree" runat="server" Text='<%# Eval("free") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="discount use">
                            <ItemTemplate>
                                <asp:Label ID="lbdiscount_use" runat="server" Text='<%# Eval("discount_use") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Free">
                            <ItemTemplate>
                                <asp:label ID="lbQtyFree" runat="server" Text='<%# Eval("QtyFree","{0:n0}") %>'></asp:label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txQtyFree" Width="100px" runat="server" Text='<%# Eval("QtyFree","{0:n0}") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible=false>
                            <ItemTemplate>
                                <asp:label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible=false>
                            <ItemTemplate>
                                <asp:label ID="lbusr_id" runat="server" Text='<%# Eval("usr_id")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible=false>
                            <ItemTemplate>
                                <asp:label ID="lbSOID" runat="server" Text='<%# Eval("SOID")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible=false>
                            <ItemTemplate>
                                <asp:label ID="lbID" runat="server" Text='<%# Eval("ID")%>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            
        </asp:UpdatePanel>
        <div">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <ContentTemplate>
                    <asp:Label ID="lbTotal" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="text-align:left;padding:10px">

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btok" runat="server" CssClass="button2 save" OnClick="btok_Click" Text="OK" />
                </ContentTemplate>
                <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btok" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

        </div>
    </form>
</body>

