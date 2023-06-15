<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcontractentry.aspx.cs" Inherits="fm_mstcontractentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
          
            }
        function ItemFreeSelected(sender, e) {
            $get('<%=hditemfree.ClientID%>').value = e.get_value();
             document.getElementById('<%=txitemfree.ClientID%>').className = "ro";
        }

        function ItemSelected(sender , e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            document.getElementById('<%=txitem.ClientID%>').className = "ro";
        }

    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Contract Entry
                <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red" style="padding:10px 10px 10px 10px"></asp:Label>
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>Salespoint</td>
            <td>:</td>
            <td>
                <asp:Label ID="lbsp" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td colspan="2">Contract No.</td>
            <td colspan="2">:</td>
            <td>
                <asp:TextBox ID="txcontractno" runat="server" Width="20em"></asp:TextBox></td>
            <td rowspan="25">
                &nbsp;</td>
        </tr>
        <tr>
            <td>Manual No.</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txmanualno" runat="server" Width="15em"></asp:TextBox>
            </td>
            <td colspan="2">Contract Date</td>
            <td colspan="2">:</td>
            <td>
                <asp:TextBox ID="dtcontract" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtcontract_CalendarExtender" runat="server" TargetControlID="dtcontract" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>Start Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtstart" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
            <td colspan="2">End Date</td>
            <td colspan="2">:</td>
            <td>
                <asp:TextBox ID="dtend" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>Type</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbcontractype" runat="server" Width="15em" AutoPostBack="True" OnSelectedIndexChanged="cbcontractype_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td colspan="2">&nbsp;</td>
            <td colspan="2">&nbsp;</td>
            <td>
               
                &nbsp;</td>
        </tr>
        <tr>
            <td>Contract For</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbind" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbind_SelectedIndexChanged" Width="15em">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbamt" runat="server" Text="Label" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbcontractpayment" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
            <td colspan="2">:</td>
            <td>
                <asp:TextBox ID="txamount" runat="server"></asp:TextBox></td>
        </tr>
        <tr style="background-color: silver">
            <td>Customer Group</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                         <asp:DropDownList ID="cbcusgrcd" runat="server" Width="15em">
                </asp:DropDownList>
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
               
            </td>
            <td colspan="2">Customer</td>
            <td colspan="2">:</td>
            <td>
               
               <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdcust" runat="server" />
                        <asp:TextBox ID="txcust" runat="server" AutoPostBack="True" Width="20em"></asp:TextBox>
                        <div id="divwidths"></div>
                <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="CustSelected">
                </asp:AutoCompleteExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>                
                </td>
        </tr>
        <tr>
            <td>Product / Item</td>
            <td>:</td>
            <td>
                <asp:RadioButtonList ID="rditem" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" Width="10em" OnSelectedIndexChanged="rditem_SelectedIndexChanged">
                    <asp:ListItem Value="I">Item</asp:ListItem>
                    <asp:ListItem Value="P">Product</asp:ListItem>
                </asp:RadioButtonList>
               
            </td>
            <td colspan="5"><strong>&lt;-- Select item or group product for contract</strong></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="6">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbprod" runat="server" Visible="False" Width="30em">
                        </asp:DropDownList>
                        <asp:TextBox ID="txitem" runat="server" Visible="False" Width="30em"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitem" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                        </asp:AutoCompleteExtender>
                        <asp:Button ID="btaddprod" runat="server" Text="Add" CssClass="button2 add" Visible="False" OnClick="btaddprod_Click" />
                        <asp:HiddenField ID="hditem" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rditem" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="6"  style="border-top:2px solid blue">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                         <asp:GridView ID="grdprod" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdprod_RowDeleting">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("itemcode") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item / Product Name">
                            <ItemTemplate><%# Eval("itemname") %></ItemTemplate>
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
                </asp:UpdatePanel>
               
                </td>
        </tr>
        <tr>
            <td colspan="8"><img src="div2.png" class="divid" /></td>
        </tr>
        <tr>
            <td colspan="8"><div class="divheader">Agreement Type Gondola</div></td>
        </tr>
        <tr>
            <td>The First party</td>
            <td>:</td>
            <td>
               
                SALIM BAWAZIR TRADING CORP.</td>
            <td colspan="2">The Second Party</td><td colspan="2">:</td><td>
               
              <asp:Label ID="lbcust" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Agreement</td>
            <td>:</td>
            <td colspan="6">
               
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdagreement" runat="server"></asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Payment Schedule</td>
            <td>:</td>
            <td>
               
                <asp:DropDownList ID="cbcontractterm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcontractterm_SelectedIndexChanged" Width="20em">
                </asp:DropDownList>
            </td>
            <td colspan="2">Payment Type</td><td colspan="2">:</td><td>
               
              <asp:DropDownList ID="cbcontractpayment" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbcontractpayment_SelectedIndexChanged">
                </asp:DropDownList>  
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td colspan="6" style="border:1px solid red">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdschedule" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="grdschedule_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sequence No.">
                            <ItemTemplate>
                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Date">
                            <ItemTemplate>
                                <asp:TextBox ID="dtpayment" runat="server" Text='<%# Eval("payment_dt","{0:d/M/yyyy}") %>'></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpayment" Format="d/M/yyyy"></asp:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txamt" runat="server" Text="0"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txqty" runat="server" Text="0"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:DropDownList ID="cbuom" runat="server"></asp:DropDownList></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><%# Eval("paycont_sta_id") %></ItemTemplate>
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
                        <asp:AsyncPostBackTrigger ControlID="cbcontractterm" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbcontractpayment" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
               
            </td>
        </tr>
        <tr>
            <td>Paid Free Prod/Item</td>
            <td>:</td>
            <td colspan="6">
                
                
                <asp:RadioButtonList ID="rdfreeitem" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" Width="10em" OnSelectedIndexChanged="rdfreeitem_SelectedIndexChanged">
                    <asp:ListItem Value="I">Item</asp:ListItem>
                    <asp:ListItem Value="P">Product</asp:ListItem>
                </asp:RadioButtonList>
               
                
                </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hditemfree" runat="server" />
            </td>
            <td>&nbsp;</td>
            <td colspan="6">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbfreeprod" runat="server" Visible="False" Width="30em">
                        </asp:DropDownList>
                        
                        <asp:TextBox ID="txitemfree" runat="server" style="margin-bottom: 0px" Visible="False" Width="30em"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txitemfree_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemFreeSelected" ServiceMethod="GetCompletionList2" TargetControlID="txitemfree" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:Button ID="btaddfree" runat="server" CssClass="button2 add" OnClick="btaddfree_Click" Text="Add" />
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdfreeitem"  EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="6">
                
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                           <asp:GridView ID="grdfreeprod" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate><%# Eval("itemcode") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item / Product Name">
                            <ItemTemplate><%# Eval("itemname") %></ItemTemplate>
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
                </asp:UpdatePanel>
                 
                
                </td>
        </tr>
        <tr>
            <td colspan="8"><img src="div2.png" class="divid" /></td>
        </tr>
        <tr>
            <td colspan="8"><div class="divheader">Tactical Bonus</div></td>
        </tr>
        <tr>
            <td>Previous Year Sold</td>
            <td>:</td>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                          <asp:TextBox ID="txsold" runat="server"></asp:TextBox>
                          &nbsp;
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbcontractype" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
          
                
                
                </td>
            <td>
                UOM</td>
            <td>
                :</td>
            <td colspan="2">
                <asp:DropDownList ID="cbuom" runat="server" Width="20em">
                </asp:DropDownList>
          
                
                
                </td>
        </tr>
        <tr>
            <td>Percentage Bonus</td>
            <td>:</td>
            <td colspan="2">
                
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txpctbonus" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                
                
                </td>
            <td>
                
                
                Pct
                
                
                Increasing Target
                
                
                </td>
            <td>
                
                
                :</td>
            <td colspan="2">
                
                
                <asp:TextBox ID="txtarget" runat="server"></asp:TextBox>
                
                
                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="6">
                
                    &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="2">
                
                
                &nbsp;</td>
            <td colspan="2">
                
                
                &nbsp;</td>
            <td colspan="2">
                
                
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="8">&nbsp;</td>
        </tr>
        </table>
    <img src="div2.png" class="divid"/>
    <table>
        <tr><td>
            <asp:DropDownList ID="cbdocument" runat="server" Width="20em">
            </asp:DropDownList>
            </td><td>
                <asp:FileUpload ID="upl" runat="server" Width="20em" />
            </td><td>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
            </td></tr>
    </table>
    <div class="divgrid">
        <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="80%">
            <Columns>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="lbdoc_cd" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                         
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lbdoc_nm" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                  
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Upload">
                    <ItemTemplate>
                        <asp:Label ID="lbimage_path" runat="server" Text='<%# Eval("image_path") %>'></asp:Label>                            
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click"/>
    </div>
</asp:Content>

