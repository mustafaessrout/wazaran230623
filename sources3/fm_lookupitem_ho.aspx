<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupitem_ho.aspx.cs" Inherits="fm_lookupitem_ho  " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Item</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script>
        function SendData()
        {
                window.close();
                window.opener.btfreeclick();
                return;
        }
    </script>

</head>
<body style="font-family:Tahoma,Verdana;font-size:small">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="tsmgr" runat="server"></asp:ToolkitScriptManager>
    <div class="auto-style1">
        <strong>Free Item Available
    </strong>
    </div>
      <div style="padding:10px 10px 10px 10px">
          Warehouse : - | Bin : - | UOM FREE :<asp:DropDownList ID="cbuomfree" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbuomfree_SelectedIndexChanged" Width="10em">
          </asp:DropDownList>
<table><tr><td class="auto-style2"><p>Maximum Delivery : <strong><%=Request.QueryString["f"]%></strong> </p></td><td class="auto-style2"><asp:Label ID="lbuomfree" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr></table>
      </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grdsearch_SelectedIndexChanged" GridLines="None" OnPageIndexChanging="grdsearch_PageIndexChanging" PageSize="15" ForeColor="#333333" OnRowDataBound="grdsearch_RowDataBound" CellPadding="0" CssClass="mygrid">
                         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                         </ItemTemplate>   
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <asp:Label ID="lbarabic" runat="server" Text='<%# Eval("size") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branded">
                        <ItemTemplate>
                            <asp:Label ID="lbsize" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unitprice">
                        <ItemTemplate>
                            <asp:Label ID="lbunitprice" runat="server" Text="0"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty To Free">
                        <ItemTemplate>
                            <asp:TextBox ID="txqty" runat="server" Width="50px" Text="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                    <asp:AsyncPostBackTrigger ControlID="cbuomfree" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
           
        </div>
        <img src="div2.png" class="divid" />
        <div class="navi">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        </div>
    </form>
</body>
</html>
