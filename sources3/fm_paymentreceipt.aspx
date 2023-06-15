<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentreceipt.aspx.cs" Inherits="fm_paymentreceipt" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
    
<style type="text/css">
body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=1);
    opacity: 0.5;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 140px;
    height:140px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
</style>


    <script>
        function RefreshData(dt)
        {
            $get('<%=hdpaid.ClientID%>').value = dt;
            $get('<%=btlookup.ClientID%>').click();
          
        }

        function ItemSelected(sender, e)
        {
            
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btinv.ClientID%>').click();
           
        }

        function clicksearch(tabno)
        {
            $get('<%=txtabno.ClientID%>').value = tabno;
            $get('<%=bttab.ClientID%>').click();
        }
    </script>
   
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }
        </style>
        <style>
    .switch {
  position: relative;
  display: inline-block;
  width: 40px;
  height: 20px;
}

/* Hide default HTML checkbox */
.switch input {display:none;}

/* The slider */
.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 18px;
  width: 18px;
  left: 2px;
  bottom: 1px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}
 
input:checked + .slider {
  background-color: #2196F3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(16px);
  -ms-transform: translateX(16px);
  transform: translateX(16px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 25px;
}

.slider.round:before {
  border-radius: 50%;
}
 </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdpaid" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="divheader">
        Payment Receipt Entry
    </div>
    <img src="div2.png" class="divid" />
    <div>
    <table>
        <tr>
            <td style="width:15%;text-align:right">
                Source</td>
            <td style="width:3px">:</td>
            <td style="width:10%">
                <asp:DropDownList ID="cbsource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsource_SelectedIndexChanged" Width="15em">
                </asp:DropDownList>
            </td>
            
            <td style="width:20%;" class="auto-style1">
                Payment Type</td>
            
            <td>
                :</td>
            
            <td>
                <asp:DropDownList ID="cbpaymenttype" runat="server" Width="15em" AutoPostBack="True" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged">
                </asp:DropDownList>
               
            
                <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click"/>
               
            
            </td>
            
        </tr>
        <tr>
            <td style="text-align: right">
                Payment No</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txpaymentno" runat="server" CssClass="ro" Width="15em"></asp:TextBox>
                
            </td>
            
            <td class="auto-style1">
                Status</td>
            
            <td>
                :</td>
            
            <td>
                <asp:DropDownList ID="cbstatus" runat="server" Width="15em" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                </asp:DropDownList>
               
            
            </td>
            
        </tr>
      
        <tr>
            <td style="text-align: right">
                Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtpayment" runat="server" CssClass="makeitreadonly" Width="15em"></asp:TextBox>
            </td>
            
            <td class="auto-style1">
                Tab No.</td>
            
            <td>
                :</td>
            
            <td>
             <%--   <table>
                    <tr>
                        <td>--%>
                             <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                     <asp:TextBox ID="txtabno" runat="server" Width="15em"></asp:TextBox>
                                     <asp:Button ID="bttabsearch" runat="server" CssClass="button2 search" OnClick="bttabsearch_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
           <%--             </td>
                    
                    </tr>
                </table>--%>
               
            
            </td>
            
        </tr>
    
        <tr>
            <td style="text-align: right">
                HO Bank Account</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbbankHO" runat="server" Width="15em">
                </asp:DropDownList>
            </td>
            
            <td class="auto-style1">
                Bank Transf/Cheque No.</td>
            
            <td>
                :</td>
            
            <td>
                <asp:TextBox ID="txdocno" runat="server" Width="15em"></asp:TextBox>
            </td>
            
        </tr>
         <tr>
            <td style="text-align: right">
                Bank Cheque</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbbankcheq" runat="server" Width="15em">
                </asp:DropDownList>
            </td>
            
            <td class="auto-style1">
                Cheque Due Date</td>
            
            <td>
                :</td>
            
            <td>
                <asp:TextBox ID="dtcheqdue" runat="server" Width="15em"></asp:TextBox>
                <asp:CalendarExtender ID="dtcheqdue_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtcheqdue">
                </asp:CalendarExtender>
            </td>
            
        </tr>
 
        <tr>
            <td style="text-align: right">
                HO Record Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtho" runat="server" Width="15em"></asp:TextBox>
                <asp:CalendarExtender ID="dtho_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtho">
                </asp:CalendarExtender>
            </td>
            
            <td class="auto-style1">
                HO Voucher No.</td>
            
            <td>
                :</td>
            
            <td>
                <asp:TextBox ID="txhovoucher" runat="server" Width="15em"></asp:TextBox>
            </td>
            
        </tr>
    
        <tr>
            <td style="text-align: right">
                Bank Account</td>
            <td>:</td>
            <td>
                         <asp:DropDownList ID="cbbankacc" runat="server" Width="15em">
                </asp:DropDownList>
            </td>
            
            <td class="auto-style1">
                Manual No.</td>
            
            <td>
                :</td>
            
            <td>
                <asp:TextBox ID="txmanualno" runat="server" Width="15em"></asp:TextBox>
            </td>
            
        </tr>
  
        <tr>
            <td style="vertical-align:top" class="auto-style1">
               Remark</td>
            <td style="vertical-align:top">:</td>
            <td colspan="4">
                         <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" Width="100%" Rows="2"></asp:TextBox>
            </td>
           
            
        </tr>
  
        <tr>
            <td style="vertical-align:top" class="auto-style1">
                Paid For</td>
            <td style="vertical-align:top">:</td>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanelxx" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rdpaidfor" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rdpaidfor_SelectedIndexChanged" RepeatDirection="Horizontal" Width="25em">
                             <asp:ListItem Value="C">By CUSTOMER</asp:ListItem>
                             <asp:ListItem Value="G">By GROUP CUSTOMER</asp:ListItem>
                         </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                         
            </td>
            <td></td>
            
        </tr>
      
        <tr>
            <td class="auto-style1">
                Customer</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanelxxx" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="txcustomer" runat="server" Width="20em"></asp:TextBox>
                        <asp:DropDownList ID="cbgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged" Width="15em">
                        </asp:DropDownList>
                               <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="ItemSelected" CompletionListElementID="divwidth">
                        </asp:AutoCompleteExtender>
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdpaidfor" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
                 <div id="divwidth"></div>
             
            </td>
            <td class="auto-style1">
                Salesman</td>
            
            <td>
                :</td>
            
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" Width="300px" Enabled="False">
                </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                
            </td>
            
        </tr>
      
        <tr>
            <td class="auto-style1">
                Amount</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                           <asp:TextBox ID="txamount" runat="server" Width="7em"></asp:TextBox>
                           <asp:Button ID="btapply" runat="server" Text="Apply" CssClass="button2 add" OnClick="btapply_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
             
            </td>
            
            <td class="auto-style1">
                Attachment</td>
            
            <td>
                :</td>
            
            <td>
                <asp:FileUpload ID="uplpayment" runat="server" />
            </td>
            
        </tr>
     
      
        <tr>
            <td class="auto-style1">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            
            <td>
                &nbsp;</td>
            
            <td>
                &nbsp;</td>
            
            <td>
                &nbsp;</td>
            
        </tr>
     
      
    </table>

    <div class="divheader">
        Invoices has been paid already
    </div>
   
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdinvpaid" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid" ForeColor="#333333" GridLines="None" EmptyDataText="INVOICE NOT FOUND" ShowHeaderWhenEmpty="True" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Cust">
                            <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sys No">
                            <ItemTemplate><%# Eval("inv_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inv No">
                            <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt Paid">
                            <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc 1%">
                            <ItemTemplate><%# Eval("disconepct") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remain Paid">
                            <ItemTemplate><%# Eval("balance") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount">
                            <ItemTemplate><%# Eval("discount_amt") %></ItemTemplate>
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
        </asp:UpdatePanel>
    
    <img src="div2.png" class="divid" />

    <div class="divheader">Available Invoice To Be Paid</div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="INVOICE NOT FOUND" OnRowDataBound="grd_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Cust">
                            <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="System No.">
                            <ItemTemplate>
                                <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice No.">
                            <ItemTemplate>
                                <asp:Label ID="lbmanualno" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%# Eval("inv_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt Inv">
                            <ItemTemplate>
                                <%--<%# Eval("totamt") %>--%>
                                <asp:HiddenField ID="hdtotamt" runat="server" Value='<%# Eval("totamt") %>' />
                                <%--<%# Eval("balance") %>--%>
                                <asp:Label ID="lbtotamt" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid">
                            <ItemTemplate>
                                <asp:Label ID="lbpaid" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remain Inv">
                            <ItemTemplate>
                                <asp:Label ID="lbremain" runat="server" Text='<%# Eval("balance") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotremain" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc 1%">
                            <ItemTemplate>
                                <asp:Label ID="lbdisconepct" runat="server"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OFF-ON 1%">
                            <ItemTemplate>
                                 <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                               <ContentTemplate>
                                    <label class="switch">
                                        <asp:CheckBox ID="chdisc" runat="server" AutoPostBack="true" OnCheckedChanged="chdisc_CheckedChanged" Checked="true"/>
                                        <div class="slider round"></div>
                                        </label>
                               </ContentTemplate>
                           </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt Apply">
                            <ItemTemplate>
                                <asp:TextBox ID="txamount" runat="server" Width="5em" OnTextChanged="btrefresh_Click" AutoPostBack="True"></asp:TextBox>
                            </ItemTemplate>                                
                            <FooterTemplate>        
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                           <asp:Label ID="lbtotamtpaid" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>                        
                             
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount">
                            <ItemTemplate>
                                <asp:TextBox ID="txdiscount" runat="server" Text="0" Width="5em" OnTextChanged="btrefresh_Click" AutoPostBack="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><%# Eval("inv_sta_nm") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataRowStyle Font-Bold="False" Font-Overline="False" Font-Size="Large" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Top" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="Blue" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <div></div>
                <div class="divnormal">
                    <img src="div2.png" class="divid" />
                    Legend :<asp:Label ID="lblegend" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="4em"></asp:Label>
                    &nbsp;=Discount Payment 1 % has applied</div>
              
                 
                  </ContentTemplate>
             <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="cbgroup" EventName="SelectedIndexChanged" />
                    <%--  <asp:AsyncPostBackTrigger ControlID="rdpaidfor" EventName="SelectedIndexChanged" />--%>
                  </Triggers>
                 </asp:UpdatePanel>
            
                     
                   
                         <asp:GridView ID="grdtab" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowEditing="grdtab_RowEditing" AllowPaging="True" OnRowUpdating="grdtab_RowUpdating">
                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                     <Columns>
                         <asp:TemplateField HeaderText="Inv No.">
                             <ItemTemplate>
                                 <asp:Label ID="lbinvno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date">
                             <ItemTemplate><%# Eval("tab_dt","{0:d/M/yyyy}") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Amount">
                             <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Paid">
                             <ItemTemplate>
                                 <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                             </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:TextBox ID="txamt" runat="server" Width="70px"></asp:TextBox>
                             </EditItemTemplate>
                             <FooterTemplate>
                                 <asp:Label ID="lbtotpaid" runat="server" Text="0"></asp:Label>
                             </FooterTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Due Date">
                             <ItemTemplate><%# Eval("due_dt","{0:d/M/yyyy}") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:CommandField ShowEditButton="True" />
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
                  
  
    <div style="float:right;padding:10px 10px 10px 10px;border-top:1px solid black">
        <asp:label runat="server" text="Suspense Payment : "></asp:label>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbsuspense" runat="server" Font-Size="Medium" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
    </div>
        </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
         <asp:Button ID="btinv" runat="server" OnClick="btinv_Click" Text="Button" style="display:none" />
         <asp:Button ID="bttab" runat="server" OnClick="bttab_Click" Text="Button" style="display:none" />
         <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none"/>
         <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" CssClass="divhid" />
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 add" OnClick="btnew_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelxx">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
            <img alt="" src="load.gif" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelxxx">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
            <img alt="" src="load.gif" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>

