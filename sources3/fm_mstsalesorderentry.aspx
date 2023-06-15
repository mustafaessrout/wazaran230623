<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstsalesorderentry.aspx.cs" Inherits="fm_mstsalesorderentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
    <script type = "text/javascript">
        function SetContextKey() {
            $find('<%=txcustomer_AutoCompleteExtender.ClientID%>').set_contextKey($get('<%=cbsalesman.ClientID %>').value);
        }

        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }

        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btprice.ClientID%>').click();
            
        }

        function ItemClear()
        {
            $get('<%=txitemsearch.ClientID%>').value = "";
            $get('<%=txstockcust.ClientID%>').value = "";
            $get('<%=txqty.ClientID%>').value = "";
            
        }

        function openwindow(url)
        {
         
            window.open(url, url, "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=600px, height=400px, top=200px, left=400px", true);
        }

        function btfreeclick()
        {
            //alert('test');
            $get('<%=btfree.ClientID%>').click();
        }
    </script>
   
    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>
   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Sales Order Entry
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Salespoint 
            </td>
            <td>:</td>
            <td>
                <asp:Label ID="lbsalespoint" runat="server" Text="Label" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td>
                Order No.
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txorderno" runat="server" CssClass="makeitreadonly" Width="100%"></asp:TextBox></td>
        </tr>
       
        <tr>
            <td>
                Order Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtorder" runat="server" CssClass="makeitreadonly"></asp:TextBox>
            </td>
            <td>
                Source Order</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbsourceorder" runat="server" OnSelectedIndexChanged="cbsourceorder_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
       
        <tr>
            <td>
                Sales type</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbsotype" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
        <tr>
            <td>
                Remark</td>
            <td>:</td>
            <td colspan="4">
                <asp:TextBox ID="txremark" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td>
                Salesman</td>
            <td>:</td>
            <td colspan="4">
                <asp:DropDownList ID="cbsalesman" runat="server" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" Width="400px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
       
        <tr>
            <td>
                Customer</td>
            <td>:</td>
            <td colspan="4">
                <asp:TextBox ID="txcustomer" runat="server" Width="400px"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected">
                </asp:AutoCompleteExtender>
                <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click"/>
                <asp:BalloonPopupExtender ID="btsearch_BalloonPopupExtender" runat="server" DisplayOnMouseOver="True" TargetControlID="txcustomer" DisplayOnClick="False" BalloonPopupControlID="divmsg">
                </asp:BalloonPopupExtender>
                <div id="divwidth">

                    <asp:HiddenField ID="hdcust" runat="server" />

                </div>
            </td>
            
        </tr>
       
        <tr>
            <td>
                <strong><em>Customer File</em></strong></td>
            <td>:</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
    </table>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>

            <div style="border:1px 1px 1px 1px;border-color:black;width:90%;border-style:solid;padding:5px 5px 5px 5px;background-color:silver">
                <table style="width:100%">
                    <tr>
                        <td>Address</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbaddress" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td>&nbsp;Credit Limit </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbcredit" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td>&nbsp;Customer Type </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbcusttype" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>City</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbcity" runat="server" Font-Bold="True" ForeColor="Red">JEDDAH</asp:Label>
                        </td>
                        <td>&nbsp;Term Payment</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbterm" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td>Sales Block</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbsalesblock" runat="server" Font-Bold="True" ForeColor="Red">NO</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Contact</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lbcontact" runat="server"></asp:Label>
                        </td>
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <img src="div2.png" class="divid" />
    <div>
        <table>
            <tr style="background-color:silver">

            <td class="auto-style1">Item</td>

            <td class="auto-style1">Price</td>
            <td class="auto-style1">Stock on Customer</td>
            <td class="auto-style1">Qty order</td>
            <td class="auto-style1">UOM</td>
            <td class="auto-style1">Add</td>
            </tr>
            <tr>
            <td style="margin-left: 40px">
                <asp:TextBox ID="txitemsearch" runat="server" Width="400px"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" TargetControlID="txitemsearch" ServiceMethod="GetItemListX" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                </asp:AutoCompleteExtender>
                <asp:HiddenField ID="hditem" runat="server" />
                <div id="divwidthi">

                </div>
                </td>
            <td style="margin-left: 40px">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbprice" runat="server" Font-Bold="True" ForeColor="Red" Text="0"></asp:Label>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btprice" EventName="Click" /></Triggers>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="btprice" runat="server" OnClick="btprice_Click" Text="Button" style="display:none" />
                </td>
            <td>
                <asp:TextBox ID="txstockcust" runat="server" Width="70px"></asp:TextBox>
                </td>
            <td>
                <asp:TextBox ID="txqty" runat="server" Width="70px"></asp:TextBox>
                </td>
            <td>
                <asp:DropDownList ID="cbuom" runat="server" Width="100px">
                </asp:DropDownList>
                </td>
            <td>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" Width="90%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%# Eval("item_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock Customer">
                            <ItemTemplate>
                                <%# Eval("stock_cust","{0:G92}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Order">
                            <ItemTemplate>
                                <%# Eval("qty","{0:G92}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lbprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
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
                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divmsg" style="display:none">
        Please type code or name customer | 
يرجى كتابة كود أو اسم العميل
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btdisc" runat="server" Text=" DISCOUNT CALCULATION " OnClick="btdisc_Click" CssClass="button2 add"/>
       
    </div>
    <img src="div2.png" class="divid" />
    <div style="width:100%">
    <div style="float:left;width:40%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" Caption="Discount Applied" CaptionAlign="Top" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grddisc_SelectedIndexChanged" ShowHeaderWhenEmpty="True" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Discount Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mechanism">
                            <ItemTemplate>
                                <%# Eval("discount_mec_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Free">
                            <ItemTemplate>
                                <asp:Label ID="lbfreeqty" runat="server" Text='<%# Eval("free_qty") %>'></asp:Label>
                               
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
                <asp:AsyncPostBackTrigger ControlID="btdisc" EventName="Click" />

            </Triggers>
        </asp:UpdatePanel>
    </div>
         <div style="float:left;width:50%;position:relative;padding:10px 10px 10px 10px">
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <ContentTemplate>

                     <asp:GridView ID="grdfree" runat="server" AutoGenerateColumns="False" Caption="Item Free" ShowHeaderWhenEmpty="True" ForeColor="#333333" GridLines="None">
                         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                         <Columns>
                             <asp:TemplateField HeaderText="Item Code">
                                 <ItemTemplate>
                                     <%# Eval("item_cd") %>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Item Name">
                                 <ItemTemplate>
                                     <%# Eval("item_nm") %>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Branded">
                                 <ItemTemplate>
                                     <%# Eval("branded_nm") %>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Size">
                                 <ItemTemplate>
                                     <%# Eval("size") %>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Qty">
                                 <ItemTemplate><%# Eval("free_qty") %></ItemTemplate>
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
                     <asp:AsyncPostBackTrigger ControlID="grddisc" EventName="SelectedIndexChanged" />
                     <asp:AsyncPostBackTrigger ControlID="btfree" EventName="Click" />
                 </Triggers>
             </asp:UpdatePanel>
     </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btfree" runat="server" OnClick="btfree_Click" Text="Button" style="display:none" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click"/>
    </div>
    </div>
    
</asp:Content>

