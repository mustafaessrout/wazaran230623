<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_ARRecAddDtl.aspx.cs" Inherits="fm_lookup_ARRecAddDtl" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <strong>Receipt Add Detail</strong>
        <link href="css/anekabutton.css" rel="stylesheet" />
        <script>
            function closewin() {
                window.opener.updpnl();
                window.close();
            }
        </script>
       
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="362px" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="SO No.">
                            <ItemTemplate>
                                <asp:Label ID="lbsocd" runat="server" Text='<%# Eval("socd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:label ID="lbsoNetAmt" runat="server" Text='<%# Eval("soNetAmt","{0:n2}") %>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Remain">
                            <ItemTemplate>
                                <asp:label ID="lbBalAmt" runat="server" Text='<%# Eval("BalAmt","{0:n2}") %>'></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rec Amt">
                            <ItemTemplate>
                                <asp:label ID="lbARCAmt" runat="server" Text='<%# Eval("ARCAmt","{0:n2}") %>'></asp:label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txARCAmt" Width="100px" runat="server" Text='<%# Eval("ARCAmt","{0:n2}") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField Visible='false'>
                            <ItemTemplate>
                              <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        
                        
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            
        </asp:UpdatePanel>
        <div>

            

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbTotal" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>

            

        </div>
        <div style="text-align:left;padding:10px">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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

