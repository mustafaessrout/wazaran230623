<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_adjustmentprice.aspx.cs" Inherits="fm_adjustmentprice" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();                   
        }
        </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Adjustment Price</div>
    <img src="div2.png" class="divid" />
    <div>


        <table>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                    <asp:RadioButtonList ID="rdcust" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">Customer</asp:ListItem>
                        <asp:ListItem Value="1">Customer Group</asp:ListItem>
                    </asp:RadioButtonList>
</ContentTemplate></asp:UpdatePanel>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                     <asp:Label ID="lbcust" runat="server" Text="Customer" Visible="False"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
                <td>
                    
                <asp:TextBox ID="txcustomer" runat="server" Width="250px" Visible="False"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" TargetControlID="txcustomer" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList" UseContextKey="True" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthc">
                </asp:AutoCompleteExtender>
                    <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" Visible="False"/>
                          <asp:HiddenField ID="hdcust" runat="server" />
               

                    <asp:DropDownList ID="cbcust" runat="server" Width="250px" Visible="False">
                    </asp:DropDownList>
                     
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Item:</td>
                <td>
                <asp:TextBox ID="txitem" runat="server" Width="20em"></asp:TextBox>
                                               
                    <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" TargetControlID="txitem" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList1" ShowOnlyCurrentWordInCompletionListItem="true">
                    </asp:AutoCompleteExtender>
                                               
                <asp:HiddenField ID="hditem" runat="server" />
                <div id="divwidhti"></div>
                </td>
                <td>Adjust Value:</td>
                <td>
                    <asp:TextBox ID="txadjust_value" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btadd" runat="server" Text="ADD" CssClass="button2 add" OnClick="btadd_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="grditem" runat="server" OnRowDeleting="grditem_RowDeleting" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grditem_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate> <asp:Label ID="lbcusgr" runat="server" Text='<%# Eval("cusgrcd")%>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd")%>'></asp:Label>
                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
<ItemTemplate>
                        <%# Eval("item_nm")%>
                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                        <%# Eval("size")%>
                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                 <ItemTemplate>
                        <%# Eval("branded_nm")%>
                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adjust Value">
                                 <ItemTemplate>
                        <%# Eval("adjust_value")%>
                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Action" />
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
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>


    </div>
    <div class="navi">

        <asp:Button ID="btsave" runat="server" CssClass="button2 save" Text="Save" OnClick="btsave_Click" />

    </div>
</asp:Content>

