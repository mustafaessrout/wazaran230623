<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_proposal.aspx.cs" Inherits="fm_proposal" EnableViewState="true" EnableEventValidation="false"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }

        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function CustSelectedEx(sender, e) {
            $get('<%=hdcustex.ClientID%>').value = e.get_value();
        }
        function RefreshData(dt) {
            $get('<%=hdprop.ClientID%>').value = dt;
            $get('<%=btlookup.ClientID%>').click();
        }
        function DateDelivery(sender, args) {
            $get('<%=btdate.ClientID%>').click();
        }
        function DateClaim(sender, args) {
            $get('<%=btend.ClientID%>').click();
        }
        function SelectDiscount(sVal) {
            $get('<%=hdpromo.ClientID%>').value = sVal;
            $get('<%=btdiscount.ClientID%>').click();
        }
    </script>
    <script type="text/javascript">
        //Stop Form Submission of Enter Key Press
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
    
    <style type="text/css">
        .auto-style1 {
            height: 9px;
        }
        .auto-style2 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdpromo" runat="server" />
    <asp:Button ID="btdiscount" runat="server" Text="Button" Style="display: none" OnClick="btdiscount_Click" />

    <div class="divheader">Proposal Entry</div>
    <img src="div2.png" class="divid" />
    <div style="width:100%">
        <table>
            <tr>
                <td>
                    Proposal No.
                </td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txpropno" runat="server" Width="20em" CssClass="ro"></asp:TextBox>
                            <asp:HiddenField ID="hdprop" runat="server" />
                            <asp:Button ID="btsearchprop" runat="server" CssClass="button2 search" OnClick="btsearchprop_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dtprop" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtprop_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtprop">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Request Promotion from Branch
                </td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txreqdisc" runat="server" Width="20em" CssClass="ro"></asp:TextBox>
                            <asp:HiddenField ID="hdreqdiscount" runat="server" />
                            <asp:Button ID="btsearchdisc" runat="server" CssClass="button2 search" OnClick="btsearchdisc_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    Principal (Vendor)</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                    <ContentTemplate>
                    <asp:DropDownList ID="cbvendor" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged">
                    </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Valid For</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="rditem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rditem_SelectedIndexChanged" Width="20em">
                        <asp:ListItem Value="I">Item</asp:ListItem>
                        <asp:ListItem Value="G">Group Product</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="dtstart" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtstart" OnClientDateSelectionChanged="DateDelivery">
                    </asp:CalendarExtender>
                </td>
                <td>End Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dtend" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtend" OnClientDateSelectionChanged="DateClaim">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Delivery Date</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="dtdelivery" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtdelivery_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtdelivery">
                    </asp:CalendarExtender>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dtstart" EventName="TextChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>Max Claim Process </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dtclaim" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtclaim_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtclaim">
                    </asp:CalendarExtender>
                </td>
            </tr>

            <tr>
                <td>
                    Adjustment Start Date</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="dtadjuststart" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtadjuststart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtadjuststart" >
                    </asp:CalendarExtender>
                </td>
                <td>Adjustment End Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dtadjustend" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtadjustend_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtadjustend" >
                    </asp:CalendarExtender>
                </td>
            </tr>

            <tr>               
                <td>
                    Proposal Vendor No</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txpropvendor" runat="server" Width="20em"></asp:TextBox>
                </td>
                <%--  
                <td>
                    SBTC Approval</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="cbsbtcapp" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
                --%>
                <td>Reference No</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txrefno" runat="server" Width="20em"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Kinds of Promotion</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="cbpromokind" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbpromokind_SelectedIndexChanged">
                        <asp:ListItem Value="ATL">ATL</asp:ListItem>
                        <asp:ListItem Value="BTL">BTL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><%--Principal Approval--%></td>
                <td><%--:--%></td>
                <td><asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbappvendor" runat="server" Width="20em">
                    </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    Promotion Group</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cbpromogroup" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbpromogroup_SelectedIndexChanged">
                    </asp:DropDownList>
                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpromokind" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>Promotion Type</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbpromotype" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbpromotype_SelectedIndexChanged">
                    </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Marketing Cost</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="cbmarketingcost" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbmarketingcost_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>Cost SBTC | Principal</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txcostsbtc" runat="server" Width="4em"></asp:TextBox>%&nbsp;
                             <asp:TextBox ID="txprincipalcost" runat="server" Width="4em"></asp:TextBox>%&nbsp;
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbmarketingcost" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </td>
            </tr>
            <tr>
                <td>
                    Be claim or not ?</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:CheckBox ID="chclaim" runat="server"/>
                    &nbsp;Tick this, if this proposal can be claim.</td>
                <td>Automatic Discount(Scheme) or not ?</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:CheckBox ID="chdiscount" runat="server"/>
                    &nbsp;Tick this, if this proposal will be create discount(scheme) automatic.</td>
            </tr>
            <%-- Add Salespoint @31052016 By Nico --%>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                    

                        <asp:Table ID="tblSalespoint" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow style="background-color:silver">
                                <asp:TableCell>Salespoint</asp:TableCell>
                                <asp:TableCell>Add</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow>
                                <asp:TableCell> 
                                        
                                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                        <ContentTemplate>        
                                            <asp:DropDownList ID="cbregion" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbregion_SelectedIndexChanged">
                                        </asp:DropDownList>                          
                                        <asp:DropDownList ID="cbsalespoint" runat="server" Width="30em" >
                                        </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbregion" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        </asp:UpdatePanel>
                                </asp:TableCell>
                                <asp:TableCell>     
                                    <asp:Button ID="btaddsls" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddsls_Click" /> 
                                </asp:TableCell>
                            </asp:TableRow>  
                        </asp:Table>

                    <%--<table style="width:100%">
                        <tr style="background-color:silver"><td>Salespoint</td><td>Add</td></tr>
                        <tr><td>
                            <asp:updatepanel runat="server">
                               <ContentTemplate>                                   
                                <asp:DropDownList ID="cbsalespoint" runat="server" Width="30em" >
                                </asp:DropDownList>
                               </ContentTemplate>
                            </asp:updatepanel>                       
                            </td>
                            <td>
                                <asp:Button ID="btaddsls" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddsls_Click" />
                            </td></tr>
                    </table>--%>

                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>

                        <asp:GridView ID="grdslspoint" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdsalespoint_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Salespoint Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblslspointcd" runat="server" Text='<%# Eval("salespoint_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salespoint Name">
                                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddsls" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <img src="div2.png" class="divid" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <%-- End Salespoint--%>
            
            <tr>
                <td class="auto-style2">
                    Valid For Cust</td>
                <td class="auto-style2">:</td>
                <td style="margin-left: 40px" class="auto-style2">
                    <asp:DropDownList ID="rdcust" runat="server" AutoPostBack="True" Width="20em" OnSelectedIndexChanged="rdcust_SelectedIndexChanged">
                        <asp:ListItem Value="C">Customer</asp:ListItem>
                        <asp:ListItem Value="G">Customer Group</asp:ListItem>
                        <asp:ListItem Value="T">Customer Type</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2"></td>
                <td class="auto-style2"></td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Table ID="tblCustomer" runat="server" style="width:100%">
                        <asp:TableRow style="background-color:silver;font-weight:bolder">
                            <asp:TableCell>Customer Group Name</asp:TableCell>
                            <asp:TableCell>Action</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbcusgrcd" runat="server" AutoPostBack="True" Width="20em">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txsearchcust" runat="server" Width="20em"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txsearchcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txsearchcust" UseContextKey="True" CompletionInterval="1" CompletionSetCount="1" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" CompletionListElementID="divc">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hdcust" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btaddcust" runat="server" CssClass="button2 add" Text="Add" OnClick="btaddcust_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                <%--<table style="width:100%">
                    <tr style="background-color:silver;font-weight:bolder">
                        <td>Customer Group Name</td>
                        <td>
                            Action
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                     <asp:DropDownList ID="cbcusgrcd" runat="server" AutoPostBack="True" Width="20em">
                                    </asp:DropDownList>
                            <asp:TextBox ID="txsearchcust" runat="server" Width="20em"></asp:TextBox>
                                     <asp:AutoCompleteExtender ID="txsearchcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txsearchcust" UseContextKey="True" CompletionInterval="1" CompletionSetCount="1" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" CompletionListElementID="divc">
                                     </asp:AutoCompleteExtender>
                                     <asp:HiddenField ID="hdcust" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                           
                            </td>
                        <td>
                            <asp:Button ID="btaddcust" runat="server" CssClass="button2 add" Text="Add" OnClick="btaddcust_Click" />
                        </td>
                    </tr>
                </table>--%>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divgrid">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                             <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="100%" ForeColor="#333333" GridLines="None" BorderStyle="Solid" BorderWidth="2px" OnRowDeleting="grdcust_RowDeleting">
                                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name">
                                <ItemTemplate>
                                    <%# Eval("cust_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Type">
                                <ItemTemplate>
                                    <%# Eval("otlcd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
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
                    <asp:GridView ID="grdcusgrcd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" ForeColor="#333333" GridLines="None" BorderStyle="Solid" BorderWidth="1px" OnRowDeleting="grdcusgrcd_RowDeleting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Group Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusgrcd" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name">
                                <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
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
                        <asp:GridView ID="grdcusttype" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="100%" ForeColor="#333333" GridLines="None" OnRowDeleting="grdcusttype_RowDeleting">

                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcusttype" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Type">
                                    <ItemTemplate>
                                        <%# Eval("custtyp_nm") %>
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
                            <asp:AsyncPostBackTrigger ControlID="btaddcust" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                    <img src="div2.png" class="divid" />
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td colspan="6">
                    <asp:Table ID="Table1" runat="server" style="width:100%">
                        <asp:TableRow style="background-color:silver;font-weight:bolder">
                            <asp:TableCell>Exclude Customer</asp:TableCell>
                            <asp:TableCell>Action</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txsearchcustex" runat="server" Width="20em"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txsearchcustex_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txsearchcustex" UseContextKey="True" CompletionInterval="1" CompletionSetCount="1" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelectedEx" CompletionListElementID="divc">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hdcustex" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btaddcustex" runat="server" CssClass="button2 add" Text="Add" OnClick="btaddcustex_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divgrid">
                    <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                        <ContentTemplate>
                             <asp:GridView ID="grdcustex" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="100%" ForeColor="#333333" GridLines="None" BorderStyle="Solid" BorderWidth="2px" OnRowDeleting="grdcustex_RowDeleting">
                                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Cust Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Name">
                                        <ItemTemplate>
                                            <%# Eval("cust_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Type">
                                        <ItemTemplate>
                                            <%# Eval("otlcd") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salespoint Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
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
                            <asp:AsyncPostBackTrigger ControlID="btaddcustex" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                    <img src="div2.png" class="divid" />
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td colspan="6">

                    <asp:Table ID="tblItem" runat="server" style="width:100%">
                        <asp:TableRow style="background-color:silver">
                            <asp:TableCell>Item / Product Group</asp:TableCell>
                            <asp:TableCell>Background</asp:TableCell>
                            <asp:TableCell>Add</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:updatepanel runat="server">
                                   <ContentTemplate>                                  
                                       <asp:TextBox ID="txsearchitem" runat="server" Width="30em"></asp:TextBox>
                                       <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="1" MinimumPrefixLength="1" CompletionListElementID="divw" OnClientItemSelected="ItemSelected">
                                       </asp:AutoCompleteExtender>
                                       <div id="divw" style="font-family:Calibri;font-size:small"></div>
                                       <div id="divc" style="font-family:Calibri;font-size:small"></div>
                                <asp:DropDownList ID="cbgroup" runat="server" Width="30em" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged">
                                </asp:DropDownList>
                                   </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rditem" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:updatepanel>
                              
                                 <asp:HiddenField ID="hditem" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbbgitem" runat="server" AutoPostBack="True" Width="15em" OnSelectedIndexChanged="cbbgitem_SelectedIndexChanged">
                                            <asp:ListItem Value="A">Background 1</asp:ListItem>
                                            <asp:ListItem Value="B">Background 2</asp:ListItem>
                                            <asp:ListItem Value="C">Background 3</asp:ListItem>
                                            <asp:ListItem Value="D">Background 4</asp:ListItem>
                                            <asp:ListItem Value="E">Background 5</asp:ListItem>
                                        </asp:DropDownList>                                        
                                        <asp:TextBox ID="txbgitempercent" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblbgitem" runat="server">%</asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txbgitemx" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblbgitemmanual" runat="server">&nbsp;+&nbsp;</asp:Label>
                                        <asp:TextBox ID="txbgitemy" runat="server" Width="5em"></asp:TextBox>
                                      <asp:Label ID="lblbgtitleitem" runat="server">Target</asp:Label>&nbsp;
                                        <asp:TextBox ID="txbgitemtarget" runat="server" Width="5em"></asp:TextBox>
                                         <asp:DropDownList ID="cbbgitemtarget" runat="server" Width="5em" >
                                            <asp:ListItem Value="Q">QTY</asp:ListItem>
                                            <asp:ListItem Value="S">SAR</asp:ListItem>
                                        </asp:DropDownList> 
                                        <asp:Label ID="lblddirect" runat="server">Disc Item</asp:Label>
                                        <asp:TextBox ID="txbgdirect" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblbgdirect" runat="server">SAR</asp:Label>&nbsp;&nbsp;
                                         <asp:Label ID="lblmaxdirect" runat="server">Max</asp:Label>
                                        <asp:TextBox ID="txmaxdirect" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblmaxdirectq" runat="server">QTY</asp:Label>                                       
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbbgitem" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <%--<table style="width:100%">
                        <tr style="background-color:silver"><td>Item / Product Group</td><td>Background</td><td>Add</td></tr>
                        <tr><td>
                            <asp:updatepanel runat="server">
                               <ContentTemplate>
                                  
                                   <asp:TextBox ID="txsearchitem" runat="server" Width="30em"></asp:TextBox>
                                   <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="1" MinimumPrefixLength="1" CompletionListElementID="divw" OnClientItemSelected="ItemSelected">
                                   </asp:AutoCompleteExtender>
                                   <div id="divw" style="font-family:Calibri;font-size:small"></div>
                                   <div id="divc" style="font-family:Calibri;font-size:small"></div>
                            <asp:DropDownList ID="cbgroup" runat="server" Width="30em" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged">
                            </asp:DropDownList>
                               </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rditem" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:updatepanel>
                              
                             <asp:HiddenField ID="hditem" runat="server" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbbgitem" runat="server" AutoPostBack="True" Width="15em" OnSelectedIndexChanged="cbbgitem_SelectedIndexChanged">
                                            <asp:ListItem Value="A">Background 1</asp:ListItem>
                                            <asp:ListItem Value="B">Background 2</asp:ListItem>
                                        </asp:DropDownList>                                        
                                        <asp:TextBox ID="txbgitempercent" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblbgitem" runat="server">%</asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txbgitemx" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblbgitemmanual" runat="server">&nbsp;+&nbsp;</asp:Label>
                                        <asp:TextBox ID="txbgitemy" runat="server" Width="5em"></asp:TextBox>
                                        <asp:Label ID="lblbgtitleitem" runat="server">Target</asp:Label>&nbsp;
                                        <asp:TextBox ID="txbgitemtarget" runat="server" Width="5em"></asp:TextBox>
                                         <asp:DropDownList ID="cbbgitemtarget" runat="server" Width="5em" >
                                            <asp:ListItem Value="Q">QTY</asp:ListItem>
                                            <asp:ListItem Value="S">SAR</asp:ListItem>
                                        </asp:DropDownList> 
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbbgitem" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
                            </td></tr>
                    </table>--%>

                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divgrid">
                       
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                        <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grditem_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DBP">
                                    <ItemTemplate><%# Eval("price_dbp") %></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="BBP">
                                    <ItemTemplate><%# Eval("price_bbp") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - Before">
                                    <ItemTemplate><%# Eval("price_rbp_before") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - After">
                                    <ItemTemplate><%# Eval("price_rbp") %></ItemTemplate>
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
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>

                        <asp:GridView ID="grdgroup" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdgroup_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Group Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbgroupcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name">
                                    <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DBP">
                                    <ItemTemplate><%# Eval("dbp") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBP">
                                    <ItemTemplate><%# Eval("bbp") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - Before">
                                    <ItemTemplate><%# Eval("price_rbp_before") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - After">
                                    <ItemTemplate><%# Eval("price_rbp") %></ItemTemplate>
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
                       
                        <asp:GridView ID="grdviewitem" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grditem_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DBP">
                                    <ItemTemplate><%# Eval("price_dbp") %></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="BBP">
                                    <ItemTemplate><%# Eval("price_bbp") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - Before">
                                    <ItemTemplate><%# Eval("price_rbp_before") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - After">
                                    <ItemTemplate><%# Eval("price_rbp") %></ItemTemplate>
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

                        <asp:GridView ID="grdviewgroup" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdgroup_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Group Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbgroupcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name">
                                    <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DBP">
                                    <ItemTemplate><%# Eval("dbp") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBP">
                                    <ItemTemplate><%# Eval("bbp") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - Before">
                                    <ItemTemplate><%# Eval("price_rbp_before") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RBP - After">
                                    <ItemTemplate><%# Eval("price_rbp") %></ItemTemplate>
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
                    <img src="div2.png" class="divid" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                    Background Remark</td>
                <td style="vertical-align:top">:</td>
                <td style="margin-left: 40px" colspan="4">
                    <asp:TextBox ID="txbgremark" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <%-- 
                    Start_   Add Mechanism Feature @29052016 By Nico
            --%>
            
            
            <tr style="background-color:silver;font-weight:bolder">
                <td colspan="6">Mechanism</td>
            </tr>
             
            <tr style="aliceblue;font-weight:bolder">
                <td colspan="6">

                    <%-- divRebate --%>
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblRebate" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow runat="server" ID="tblheaderRebate">
                                <asp:TableCell>For Rebate</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>Rebate Condition</asp:TableCell>
                                <asp:TableCell>Target Sales Qty/Ctn</asp:TableCell>
                                <asp:TableCell>Target Cost Qty/Ctn</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell><asp:TextBox ID="txrbtcondition" runat="server" Width="20em"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="txtrgtsales" runat="server" Width="10em"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="txtrgtcost" runat="server" Width="10em"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddrebate" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddrebate_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdrebate" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdrebate_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Rebate Condition">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrbtcondition" runat="server" Text='<%# Eval("rbtcondition") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target Sales(Qty/Sr)">
                                    <ItemTemplate><%# Eval("trgtsales") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target Cost(Qty/Sr)">
                                    <ItemTemplate><%# Eval("trgtcost") %></ItemTemplate>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddrebate" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divRebate --%>

                    <%-- divOther --%>
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblOther" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Miscellaneous Promo (By Period)</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell ID="hdFromPromo">From</asp:TableCell>
                                <asp:TableCell ID="hdToPromo">To</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtotherpromofrm" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtotherpromofrm_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtotherpromofrm">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell ID="clToPromo">
                                    <asp:TextBox ID="dtotherpromoto" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtotherpromoto_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtotherpromoto">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddotherpromo" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddotherpromo_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdotherpromo" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdotherpromo_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="(Period) From">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtotherpromofrm" runat="server" Text='<%# Eval("dtfrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="(Period) To">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtotherpromoto" runat="server" Text='<%# Eval("dtto") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddotherpromo" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divOther --%>

                    <%-- divRent --%>
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblRent" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Display Rent</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>SIZE M2 (Floor, Shelves, Fridge, Rack, Stand, End)</asp:TableCell>
                                <asp:TableCell>From</asp:TableCell>
                                <asp:TableCell>To</asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txsizerent" runat="server" Width="20em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtrentfrm" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtrentfrm_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtrentfrm">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtrentto" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtrentto_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtrentto">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddrent" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddrent_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grddisplayrent" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grddisplayrent_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SIZE M2 (Floor, Shelves, Fridge, Rack, Stand, End)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsizerent" runat="server" Text='<%# Eval("size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="(Period) From">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtotherpromofrm" runat="server" Text='<%# Eval("dtfrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="(Period) To">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtotherpromoto" runat="server" Text='<%# Eval("dtto") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddotherpromo" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divRent --%>

                    <%-- divFee --%>
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblFee" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Opening Fee, Listing Fee, Promo Budget</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>Cost (Value)</asp:TableCell>
                                <asp:TableCell>Cost (Qtq)</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txcostfeesr" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txcostfeeqty" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddfee" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddfee_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdfee" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdfee_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Cost (value)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcostval" runat="server" Text='<%# Eval("costval") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost (Qty)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcostqty" runat="server" Text='<%# Eval("costqty") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddfee" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divFee --%>

                     <%-- divCar --%>
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblCar" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Car Branding</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>Size (M2)</asp:TableCell>
                                <asp:TableCell>Placed To</asp:TableCell>
                                <asp:TableCell>Car Type</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txsizecar" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txplacecar" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtypecar" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddcar" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddcar_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdcar" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdcar_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Size(M2)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcarsize" runat="server" Text='<%# Eval("size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Placed To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcarplace" runat="server" Text='<%# Eval("placed") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcartype" runat="server" Text='<%# Eval("type") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddcar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divCar --%>

                    <%-- divCook --%>
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblCook" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Cooking Demo, Sample Promo, Re-Packing</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>From</asp:TableCell>
                                <asp:TableCell>To</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtcookfrm" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtcookfrm_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtcookfrm">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtcookto" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtcookto_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtcookto">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddcook" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddcook_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdcook" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdcook_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="(Period) From">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtcookfrm" runat="server" Text='<%# Eval("dtfrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="(Period) To">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtcookto" runat="server" Text='<%# Eval("dtto") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddcook" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divCook --%>

                    
                    <%-- divSignBoard --%>
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblSignBoard" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Signboard</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>SIZE M2</asp:TableCell>
                                <asp:TableCell>Placed To</asp:TableCell>
                                <asp:TableCell>From</asp:TableCell>
                                <asp:TableCell>To</asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txsizesb" runat="server" Width="20em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="cbplacesb" runat="server" Width="10em" >
                                        <asp:ListItem Value="INDOOR">INDOOR</asp:ListItem>
                                        <asp:ListItem Value="OUTDOOR">OUTDOOR</asp:ListItem>
                                    </asp:DropDownList> 
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtsbfrm" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtsbfrm_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtsbfrm">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="dtsbto" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtsbto_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtsbto">
                                    </asp:CalendarExtender>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddsb" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddsb_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdsignboard" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdsignboard_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SIZE M2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsizesb" runat="server" Text='<%# Eval("size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Placed To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblplacesb" runat="server" Text='<%# Eval("place") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="(Period) From">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtsbfrm" runat="server" Text='<%# Eval("dtfrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="(Period) To">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldtsbto" runat="server" Text='<%# Eval("dtto") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddsb" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divSignBoard --%>
                    
                     <%-- divChep --%>
                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="tblChep" runat="server" Width="100%" >                           
                            <asp:TableHeaderRow>
                                <asp:TableCell>For Miscellaneous Activites</asp:TableCell>
                            </asp:TableHeaderRow> 
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell>Value (Sar)</asp:TableCell>
                                <asp:TableCell>Mechanism</asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txvaluechep" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txmechchep" runat="server" Width="10em"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell><asp:Button ID="btaddchep" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddchep_Click" /></asp:TableCell>
                            </asp:TableRow>          
                        </asp:Table>                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <div class="divgrid">                       
                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grdchep" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdchep_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Value(Sar)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvaluechep" runat="server" Text='<%# Eval("value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mechanism">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmechchep" runat="server" Text='<%# Eval("mechanism") %>'></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btaddchep" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%-- divCar --%>

                </td>
            </tr>
            <tr>
                <td colspan="6">

                </td>
            </tr>

            <tr><td colspan="6" class="auto-style1"><img src="div2.png" class="divid" /></td></tr>

            <%-- 
                    End_    
            --%>
            <tr>
                <td>
                    Has Agreement</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:CheckBox ID="chagreement" runat="server" AutoPostBack="True" OnCheckedChanged="chagreement_CheckedChanged" />
                &nbsp;Tick this, if this proposal has agreement</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Agreement Date</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtagree" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtagree_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtagree" CssClass="MyCalendar">
                    </asp:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chagreement" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                    
                </td>
                <td>Agreement No.</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txagreementno" runat="server" Width="20em"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chagreement" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </td>
            </tr>
            <tr>
                <td colspan="6"><img src="div2.png" class="divid" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Freegood for Budgeting !</td>
                <td>:</td>
                <td colspan="4" style="margin-left: 40px">
                    <asp:CheckBox ID="chfreegood" runat="server" AutoPostBack="true" OnCheckedChanged="chfreegood_CheckedChanged"/>
                    &nbsp;Tick this, if this need freegood for budgeting.
                </td>
            </tr>
            <tr runat="server" id="showFreegood">
                <td><asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblfreeitem" Text="Free Item"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel></td>
                <td><asp:UpdatePanel ID="UpdatePanel47" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lbldotfreeitem" Text=":"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                        <ContentTemplate>
                    <asp:RadioButtonList ID="rdfreeitem" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdfreeitem_SelectedIndexChanged">
                            <asp:ListItem Value="I">Item</asp:ListItem>
                            <asp:ListItem Value="G">Product Group</asp:ListItem>
                        </asp:RadioButtonList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chfreegood" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                    <ContentTemplate>
                    <asp:Table ID="tbaddfree" runat="server" Width="100%">
                        <asp:TableRow BackColor="Silver">
                            <asp:TableCell><asp:Label runat="server" ID="lblfgbranded" Text="Branded"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label runat="server" ID="lblfggroup" Text="Group Product"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label runat="server" ID="lblfgproduct" Text="Product"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label runat="server" ID="lblfgitem" Text="Item"></asp:Label></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:DropDownList ID="cbbrandedfree" runat="server" OnSelectedIndexChanged="cbbrandedfree_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="cbprodgroupfree" runat="server" Width="200px" OnSelectedIndexChanged="cbprodgroupfree_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="cbitemfree" runat="server" Width="300px" OnSelectedIndexChanged="cbitemfree_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                                <ContentTemplate>
                                <asp:DropDownList ID="cbitemfrees" runat="server" Width="100%" Visible="False">
                                </asp:DropDownList>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btadditemfree" runat="server" CssClass="button2 add" OnClick="btadditemfree_Click" style="left: 0px; top: 0px" Text="Add" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdfreeitem" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" PageSize="5" Width="75%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%# Eval("item_desc") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <%# Eval("size") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate>
                                    <%# Eval("branded_nm") %>
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
                            <asp:AsyncPostBackTrigger ControlID="btadditemfree" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="grdfreeproduct" runat="server" AllowPaging="True" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdfreeproduct_PageIndexChanging" OnRowDeleting="grdfreeproduct_RowDeleting1" PageSize="5" Width="75%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Prod Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbprodcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%# Eval("prod_nm") %>
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
                            <asp:AsyncPostBackTrigger ControlID="btadditemfree" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="6"><img src="div2.png" class="divid" /></td>
            </tr>
            <tr>
                <td>Price Budget Calculation ! (Choose The Price)</td>
                <td>:</td>
                <td colspan="4" style="margin-left: 40px">
                    <asp:DropDownList ID="cbprice" runat="server" Width="20em">
                        <asp:ListItem Value="DBP">DBP</asp:ListItem>
                        <asp:ListItem Value="BBP">BBP</asp:ListItem>
                        <asp:ListItem Value="RBP">RBP</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="cbpricetype" runat="server" Width="20em">
                        <asp:ListItem Value="C">Cheapest Price</asp:ListItem>
                        <asp:ListItem Value="E">Expensive Price</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Budget Calculation (Detail/Not Detail) !</td>
                <td>:</td>
                <td colspan="4" style="margin-left: 40px">
                    <asp:CheckBox ID="chbudget" runat="server" AutoPostBack="true" OnCheckedChanged="chbudget_CheckedChanged"/>
                    &nbsp;Tick this, if this need detail budget calculation.
                </td>
            </tr>
            <tr>
                <td>
                    Budget Limit</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <%--<asp:DropDownList ID="cbbudgettype" runat="server" AutoPostBack="True" Width="10em" OnSelectedIndexChanged="cbbudgettype_SelectedIndexChanged" CssClass="ddl">
                        </asp:DropDownList>&nbsp;&nbsp;--%>
                        <asp:TextBox ID="txbudget" runat="server" Width="10em"></asp:TextBox>&nbsp;
                        <asp:Label ID="lbsar" runat="server" Text="CTN"></asp:Label>&nbsp;&nbsp;&nbsp;
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chbudget" EventName="CheckedChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>Payment Type</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbpaymenttype" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbpromogroup" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                    <ContentTemplate>
                    <asp:Label ID="lblfreeproducttitle" runat="server" Text="Free Product"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpaymenttype" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txfreegood" runat="server" Width="10em"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblfreeproduct" runat="server" Text="QTY"></asp:Label>
                        <%--<asp:Button ID="btncalbudget" runat="server" Text="Calculate" CssClass="button2" OnClick="btncalculatebudget_Click" />--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpaymenttype" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td><%--UOM--%> Additional Budget</td>
                <td>:</td>
                <td><%--<asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbuom" runat="server" Width="20em" >
                            </asp:DropDownList>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbbudgettype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                            --%>
                    <asp:TextBox ID="txaddbudget" runat="server" Width="10em"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Budget</td>
                <td>:</td>
                <td style="margin-left: 40px" colspan="4">
                    
                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                        <ContentTemplate>

                        <asp:TextBox ID="txtotalbudget" runat="server" Width="10em"></asp:TextBox>&nbsp;
                        <asp:Label ID="lbltotalbudget" runat="server" Text="SAR"></asp:Label>

                    <asp:GridView ID="grdcost" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="90%" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="DBP">
                                <ItemTemplate><asp:Label ID="dbp" runat="server" Text='<%# Eval("dbp") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BBP"><ItemTemplate><%# Eval("bbp") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP"><ItemTemplate>
                                <asp:Label ID="rbp" runat="server" Text='<%# Eval("rbp") %>'></asp:Label>
                                                                </ItemTemplate></asp:TemplateField>
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
                    <asp:GridView ID="grdcostprodTB" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="90%" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Product Group">
                                <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DBP">
                                <ItemTemplate><asp:Label ID="dbp" runat="server" Text='<%# Eval("dbp") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BBP"><ItemTemplate><%# Eval("bbp") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP"><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("rbp") %>'></asp:Label></ItemTemplate></asp:TemplateField>
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
                    <asp:GridView ID="grdcostitemTB" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="90%" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DBP">
                                <ItemTemplate><asp:Label ID="dbp" runat="server" Text='<%# Eval("dbp") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BBP"><ItemTemplate><%# Eval("bbp") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP"><ItemTemplate><%# Eval("rbp") %></ItemTemplate></asp:TemplateField>
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
                            <asp:AsyncPostBackTrigger ControlID="cbpaymenttype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr><td colspan="6"></td></tr>
            <tr style="background-color:silver;font-weight:bolder">
                <td colspan="6">PAFL, F & A Dept. validation of Budget Balance</td>
            </tr>
            <tr>
                <td>
                    Month To Date</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                        <ContentTemplate>
                    <asp:TextBox ID="txbudgetmtd" runat="server" Width="10em"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel></td>
                <td>Year To Date</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                        <ContentTemplate>
                    <asp:TextBox ID="txbudgetytd" runat="server" Width="10em"></asp:TextBox>
                     </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
            </tr>
            <tr><td colspan="6"></td></tr>
            <tr style="background-color:silver;font-weight:bolder">
                <td colspan="6">Sales & Promotion validation of Budget Balance</td>
            </tr>
            <tr>
                
                <td colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                    <ContentTemplate>
                       <%-- <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>  --%>
                        <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" 
                            CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" 
                            EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True" 
                            OnRowCreated="GridView1_RowCreated" > 
                        </asp:GridView>
                        <asp:Button ID="btnTest" runat="server" Text="GetRowGrid" CssClass="button2 add" OnClick="btTest_Click" />--%>
                        <asp:Table ID="tblGroupBudget" runat="server" Width="100%" >  
                            <asp:TableRow BackColor="Silver">
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell ID="a1">a1</asp:TableCell>
                                <asp:TableCell ID="a2">a2</asp:TableCell>
                                <asp:TableCell ID="a3">a3</asp:TableCell>
                                <asp:TableCell ID="a4">a4</asp:TableCell>
                                <asp:TableCell ID="a5">a5</asp:TableCell>
                                <asp:TableCell ID="a6">a6</asp:TableCell>
                                <asp:TableCell ID="a7">a7</asp:TableCell>
                                <asp:TableCell ID="a8">a8</asp:TableCell>
                                <asp:TableCell ID="a9">a9</asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell>Key Account</asp:TableCell>
                                <asp:TableCell ID="cka1"><asp:TextBox ID="ka1" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka2"><asp:TextBox ID="ka2" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka3"><asp:TextBox ID="ka3" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka4"><asp:TextBox ID="ka4" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka5"><asp:TextBox ID="ka5" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka6"><asp:TextBox ID="ka6" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka7"><asp:TextBox ID="ka7" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka8"><asp:TextBox ID="ka8" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cka9"><asp:TextBox ID="ka9" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>Non Key Account</asp:TableCell>
                                <asp:TableCell ID="cnka1"><asp:TextBox ID="nka1" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka2"><asp:TextBox ID="nka2" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka3"><asp:TextBox ID="nka3" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka4"><asp:TextBox ID="nka4" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka5"><asp:TextBox ID="nka5" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka6"><asp:TextBox ID="nka6" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka7"><asp:TextBox ID="nka7" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka8"><asp:TextBox ID="nka8" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="cnka9"><asp:TextBox ID="nka9" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>   
                            <asp:TableRow>
                                <asp:TableCell>Total</asp:TableCell>
                                <asp:TableCell ID="tc1"><asp:TextBox ID="t1" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc2"><asp:TextBox ID="t2" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc3"><asp:TextBox ID="t3" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc4"><asp:TextBox ID="t4" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc5"><asp:TextBox ID="t5" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc6"><asp:TextBox ID="t6" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc7"><asp:TextBox ID="t7" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc8"><asp:TextBox ID="t8" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                                <asp:TableCell ID="tc9"><asp:TextBox ID="t9" runat="server" Width="10em" Text="0.0"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>       
                        </asp:Table>                   
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td colspan="6"><img src="div2.png" class="divid" />
                    &nbsp;</td>
            </tr>
            <tr runat="server" id="showPayment">
                <td><asp:UpdatePanel ID="UpdatePanel45" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblpayment" Text="Payment Term"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td><asp:UpdatePanel ID="UpdatePanel46" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lbldotpayment" Text=":"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel></td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cbpaymentterm" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbpaymentterm_SelectedIndexChanged">
                    </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="margin-left: 40px" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdpayment" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sequence No.">
                                <ItemTemplate>
                                    <%# Eval("sequenceno") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Of Payment">
                                <ItemTemplate>
                                    <asp:TextBox ID="dtpayment" runat="server" Text=' <%# Eval("payment_dt","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpayment" Format="dd/MM/yyyy" CssClass="MyCalendar"></asp:CalendarExtender>
                                   </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt To Be Paid">
                                <ItemTemplate>
                                    <asp:TextBox ID="txqty" runat="server" Text=' <%# Eval("qty") %>'></asp:TextBox>
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
                            <asp:AsyncPostBackTrigger ControlID="cbpaymentterm" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                    Remark</td>
                <td style="vertical-align:top">:</td>
                <td style="margin-left: 40px" colspan="4">
                    <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="background-color:silver;font-weight:bolder">
                    PROPOSAL SIGN BY SBTC</td>
            </tr>
            <tr>
                <td>
                    A &amp; P Coordinator</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="cbapcoordinator" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
                <td>Claim (A&amp;P) Dept Head</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbclaimdepthead" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Product Manager</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="cbprodman" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
                <td>Key Account Mgr</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbkamgr" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Ex-COM SBTC</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="cbgm" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
                <td>Marketing Manager</td>
                <td>:</td>
                <td><asp:DropDownList ID="cbmarketmgr" runat="server" Width="20em">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="6" style="font-weight:bolder;background-color:silver">
                    PROPOSAL SIGN BY PRINCIPAL</td>
            </tr>
            <tr>
                <td>
                    Marketing Mgr</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbmarketmgrpcp" runat="server" Width="20em">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>Finance Dept.</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbfinpcp" runat="server" Width="20em">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td>
                    NSPM</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbnspmpcp" runat="server" Width="20em">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
                <td>Marketing Director</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbmarketingdirpcp" runat="server" Width="20em">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td>
                    GM/Fin Director</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbgmpcp" runat="server" Width="20em">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6" style="font-weight:bolder;background-color:silver">
                    PROPOSAL SIGN TEMPLATE</td>
            </tr>
            <tr><td>Select Frame</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbsignframe" runat="server" Width="15em">
                        <asp:ListItem Value="F1">Indomie Cup Noodle</asp:ListItem>
                        <asp:ListItem Value="F2">Indomie Noodle</asp:ListItem>
                        <asp:ListItem Value="F3">Misfaco</asp:ListItem>
                        <asp:ListItem Value="F4">Toya Non Noodle</asp:ListItem>
                        <asp:ListItem Value="F5">Toya Noodle</asp:ListItem>
                        <asp:ListItem Value="F6">SBTC & Principal</asp:ListItem>
                        <asp:ListItem Value="F7">SBTC</asp:ListItem>
                        <asp:ListItem Value="F8">ATL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6" style="font-weight:bolder;background-color:silver">
                    PROPOSAL DOCUMENT UPLOAD</td>
            </tr>
            <%--<tr><td>Proposal Document</td>
                <td>:</td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                <ContentTemplate>
                <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                <asp:Label ID="lbfileloc" runat="server" Text='Proposal Document'></asp:Label></asp:HyperLink>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>--%>
            <tr>
            <td colspan="6" style="text-align: center">
                <asp:GridView ID="grdcate" runat="server" AutoGenerateColumns="False" Width="100%" ForeColor="#333333" GridLines="None" PageSize="5">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Document Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Upload">
                            <ItemTemplate>
                                <asp:FileUpload ID="upl" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BY">
                            <ItemTemplate>
                                <asp:Label ID="lbdic" runat="server" Text='<%# Eval("dic") %>'></asp:Label>
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
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center">
                <div class="navi">
                    <asp:Button ID="btnuploaddoc" runat="server" Text="Upload" CssClass="button2 save" OnClick="btnuploaddoc_Click" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center">
                <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Document Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdoc_cd" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lbdoc_nm" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File location">
                            <ItemTemplate>
                                <a class="example-image-link" href="/images/proposal_doc/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                    <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
            </td>
        </tr>
        <div runat="server" id="viewStatus">
        <tr>
        <td colspan="6" style="font-weight:bolder;background-color:silver">
                PROPOSAL STATUS</td>
        </tr>
        <tr><td>Status</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbstatus" runat="server" Width="15em">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr><td>Remark</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txstatus" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        </div>
        </table>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 add" OnClick="btnew_Click" />
        <asp:Button ID="btedit" runat="server" Text="Edit" CssClass="button2 edit" OnClick="btedit_Click" />
        <asp:Button ID="btapprove" runat="server" Text="Approve" CssClass="button2 save "  OnClick="btapprove_Click" />        
        <asp:Button ID="btcancel" runat="server" Text="Cancel / Reject" CssClass="button2 delete" OnClick="btcancel_Click" BackColor="Yellow" />
        <asp:Button ID="btdelete" runat="server" Text="Deactivated" CssClass="button2 delete" OnClick="btdelete_Click" BackColor="Red" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btupdate" runat="server" Text="Update" CssClass="button2 save" OnClick="btupdate_Click" />
        <asp:Button ID="btupload" runat="server" Text="Upload Document" CssClass="button2 add" OnClick="btupload_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print With DBP" CssClass="button2  print" OnClick="btprint_Click" />
        <asp:Button ID="btprint2" runat="server" Text="Print Without DBP" CssClass="button2  print" OnClick="btprint2_Click" />
        <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none"/>
        <asp:Button ID="btdate" runat="server" Text="Button" OnClick="btdate_Click" style="display:none"/>
        <asp:Button ID="btend" runat="server" Text="Button" OnClick="btend_Click" style="display:none"/>
    </div>
</asp:Content>

