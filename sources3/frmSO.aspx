<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmSO.aspx.cs" Inherits="frmSO" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="css/anekabutton.css" rel="stylesheet" />
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
        function openwindow() {
            var strQS = '<%= Request.QueryString["src"] %>';
            if (strQS=="SO")
                {
                var oNewWindow = window.open("fm_lookup_SalesOrder.aspx?src=SO", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            }
            else
            {
                var oNewWindow = window.open("fm_lookup_SalesOrder.aspx?src=Canvass", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            }
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
  <script>
      function openwindow2() {
          var oNewWindow = window.open("fm_lookup_SalesOrderDisc.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");

      }

      function updpnl2() {
          document.getElementById('<%=bttmp2.ClientID%>').click();
            return (false);
        }
    </script>
     <%--<script type="text/javascript">
         $(function () {
             $("#<%=txsoDocDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txsoDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txsoDueDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>--%>

    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
        function ItemSelectedCust(sender, e) {
            $get('<%=hdcust_cd.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <script>
        function ItemSelectedsiteCD(sender, e) {
            $get('<%=hdsiteCD.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <script>
        function ItemSelectedsiteCDVan(sender, e) {
            $get('<%=hdsiteCDVan.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <style type="text/css">
                            
.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    padding: 7px 12px;
    position: relative;
    display: inline-block;
    text-shadow: 0 1px 0 #fff;
    -webkit-transition: border-color .218s;
    -moz-transition: border .218s;
    -o-transition: border-color .218s;
    transition: border-color .218s;
    background: #f3f3f3;
    background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    background: #f3f3f3;
    border: solid 1px #dcdcdc;
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
            top: 0px;
            left: 0px;
            height: 39px;
            width: 133px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            
            
            <td>
                <asp:UpdatePanel ID="UpdatePanel71" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbTittle" runat="server" Text="SALES ORDER PROCESS" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            
            
       <td>&nbsp;</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txstatus" runat="server"  ReadOnly="True" Width="98px" CssClass="makeitreadonly" Font-Bold="True" ForeColor="#CC0000" style="padding:5px" BorderColor="#CC0000" BorderWidth="3px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            
        </tr>
    </table>
    
    
    <table>
        
        <tr>
            <td >Sales Point </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:TextBox ID="txKey" runat="server"  Width="40px" style="display:none"></asp:TextBox>
                        </strong>
                        <asp:UpdatePanel ID="UpdatePanel64" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" Enabled="False" Height="20px" Width="195px" CssClass="makeitreadonly">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp2" EventName="Click" /></Triggers>
                </asp:UpdatePanel>

            </td>
            <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
            <asp:Button ID="bttmp2" runat="server" Text="Button" OnClick="bttmp2_Click" style="display:none" />
            <td >Customer </td>
            <td >
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsearchCust" runat="server" Width="356px" AutoPostBack="True" OnTextChanged="txsearchCust_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txsearchCust_AutoCompleteExtender" runat="server" TargetControlID="txsearchCust" ServiceMethod="GetCompletionListCust" MinimumPrefixLength="1" 
                    EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedCust" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdcust_cd" runat="server" ClientIDMode="Static" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txsearchCust" ErrorMessage="**" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Order No.</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                    <ContentTemplate>
                        
                        <asp:TextBox ID="txSOCD" runat="server" OnTextChanged="txSOCD_TextChanged" Width="130px" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                        <strong>
                        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 7px; width: 99px;" />
                        </strong>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Address</td>
            <td>
          
                    
                        <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txaddr" runat="server"  Width="356px" ReadOnly="True" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    
                
            </td>
        </tr>
        <tr>
            <td>Doc Date</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsoDocDate" runat="server"  Width="75px" OnTextChanged="txsoDocDate_TextChanged" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                        <asp:CalendarExtender ID="txsoDocDate_CalendarExtender" runat="server" TargetControlID="txsoDocDate">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td >Salesman Code</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesCD" runat="server" AutoPostBack="True"  Height="20px" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Order Date</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsoDate" runat="server"  Width="75px" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                        <asp:CalendarExtender ID="txsoDate_CalendarExtender" runat="server" TargetControlID="txsoDate">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Available Credit Limit</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txcreditLimit" runat="server"  Width="75px" ReadOnly="True" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Manual SO No.</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txreference" runat="server"  Width="150px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>customer type</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbotlcd" runat="server" AutoPostBack="True" CssClass="makeitreadonly" Enabled="False" Height="20px" Width="150px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cbpricelevel_cd" runat="server" AutoPostBack="True"  Height="20px" Width="150px" CssClass="makeitreadonly" Enabled="False" Visible="False">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                                    <ContentTemplate>
                                        Site Code&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
            <td>
                
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txsearchsiteCD" runat="server"  Width="250px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchsiteCD_AutoCompleteExtender" runat="server" TargetControlID="txsearchsiteCD" ServiceMethod="GetCompletionListsiteCD" MinimumPrefixLength="1" 
                    EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsiteCD" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hdsiteCD" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txsearchsiteCDVan" runat="server" Width="250px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchsiteCDVan_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedsiteCDVan" ServiceMethod="GetCompletionListsiteCDVan" TargetControlID="txsearchsiteCDVan" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hdsiteCDVan" runat="server" ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </td>
            <td>Payment Term</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbpayment_term" runat="server" AutoPostBack="True"  Height="20px" Width="150px" CssClass="makeitreadonly" Enabled="False">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel60" runat="server">
                    <ContentTemplate>
                        BIN Type
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbsiteType" runat="server" Height="20px" Width="195px"  AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td > Due Date
                    </td>
            <td >
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsoDueDate" runat="server" Width="75px" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                        <asp:CalendarExtender ID="txsoDueDate_CalendarExtender" runat="server" TargetControlID="txsoDueDate">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                    <ContentTemplate>
                        Order No. Device&nbsp;
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txSORefCD" runat="server"  Width="150px" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                    <ContentTemplate>
                        Order By
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                                <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cborderby" runat="server" AutoPostBack="True" Height="20px" Width="195px">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="cbtranType" runat="server" AutoPostBack="True"  Height="20px" Width="195px" Visible="False">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
        </tr>
        <tr>
            <td>
                        PO No.</td>
            <td>


                <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsoPO" runat="server" Width="150px"></asp:TextBox>
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
                
             </td>      
            <td>
                <asp:UpdatePanel ID="UpdatePanel76" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbDriver" runat="server" Text="Driver"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbdriver" runat="server" Height="20px" Width="195px"  AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                                <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                                    <ContentTemplate>
                                        PO Date
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsoPODate" runat="server"  Width="150px"></asp:TextBox>
                        <asp:CalendarExtender ID="txsoPODate_CalendarExtender" runat="server" TargetControlID="txsoPODate">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel77" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbInvoiceCD" runat="server" Text="Inv Sys No."></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txInvoiceCD" runat="server" Width="176px" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel72" runat="server">
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel78" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbreferenceInv" runat="server" Text="Manual Inv No."></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel74" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txreferenceInv" runat="server" OnTextChanged="txreferenceInv_TextChanged" Width="176px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        
        </table>
        <table style="width: 100%;">
            <tr  style="background-color:silver;border-color:yellow;border:none">
                <td>Itemem</td>
                <td>UOM</td>
                <td>Qty Stk</td>
                <td>Qty order</td>
                <td>Qty Ship</td>
                <td>Price</td>
                <td>
                        Disc</td>
                <td>Net Amount</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                
                <td>


                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txsearchitem" runat="server" AutoPostBack="True" Width="356px"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <asp:HiddenField ID="hditem" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbUnitCD" runat="server" Height="20px" Width="95px"  AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td >
                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsopQuantityStock" runat="server" Width="48px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
                <td>
                            <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsopQuantity" runat="server"  Width="49px" AutoPostBack="True" OnTextChanged="txsopQuantity_TextChanged"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
                <td>

                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txsopQuantityDelivery" runat="server" AutoPostBack="True"  OnTextChanged="txsopQuantityDelivery_TextChanged" Width="49px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
                
        

                <td>
                            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsopPrice" runat="server"  Width="56px" AutoPostBack="True" OnTextChanged="txsopPrice_TextChanged" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
                
                <td>
                            <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsopDiscount" runat="server"  Width="56px" AutoPostBack="True" OnTextChanged="txsopDiscount_TextChanged" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
                <td>
                            <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsopAmount" runat="server" Width="56px" ReadOnly="True" CssClass="makeitreadonly" Enabled="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btAdd" runat="server" CssClass="button2 add" OnClick="btAdd_Click" Text="Add" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
            </tr>
            </table>
            <table style="width: 130%;">
            <tr>
                <td colspan="11">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdSO" runat="server" AutoGenerateColumns="False" Width="1031px" OnRowDeleting="grdSO_RowDeleting" OnSelectedIndexChanging="grdSO_SelectedIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No"><ItemTemplate><asp:Label ID="lbseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="item Code"><ItemTemplate><asp:Label ID="lblitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="item name"><ItemTemplate><asp:Label ID="lblitem_nm" runat="server" Text='<%# Eval("itmDesc") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size"><ItemTemplate><asp:Label ID="lblsize" runat="server" Text='<%# Eval("size") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty"><ItemTemplate><asp:Label ID="lblsopQuantity" runat="server" Text='<%# Eval("sopQuantity","{0:n0}") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship Qty"><ItemTemplate><asp:Label ID="lblsopQuantityDelivery" runat="server" Text='<%# Eval("sopQuantityDelivery","{0:n0}") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Qty"><ItemTemplate><asp:Label ID="lblsopQuantityStock" runat="server" Text='<%# Eval("sopQuantityStock","{0:n0}") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="UoM"><ItemTemplate><asp:Label ID="lblUnitCD" runat="server" Text='<%# Eval("UnitCD") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField Visible='false'>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbsopSeqID" runat="server" Text='<%# Eval("sopSeqID") %>'></asp:Label>
                                                    </ItemTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price"><ItemTemplate><asp:Label ID="lblsopPrice" runat="server" Text='<%# Eval("sopPrice","{0:n2}") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                   <asp:TemplateField HeaderText="Disc"><ItemTemplate><asp:Label ID="lblsopDiscount" runat="server" Text='<%# Eval("sopDiscount","{0:n2}") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amt"><ItemTemplate><asp:Label ID="lblsopAmount" runat="server" Text='<%# Eval("sopAmount","{0:n2}") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                    <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowSelectButton="True" />
                                </Columns>
                                <SelectedRowStyle BackColor="#3399FF" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table>
    
    <table style="width: 30%";>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel69" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdProd" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grdProd_SelectedIndexChanging" Width="1031px">
                            <Columns>
                                <asp:TemplateField HeaderText="prod cd">
                                    <ItemTemplate>
                                        <asp:Label ID="lbprod_cd" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="prod nm">
                                    <ItemTemplate>
                                        <asp:Label ID="lbprod_nm" runat="server" Text='<%# Eval("prod_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbQty" runat="server" Text='<%# Eval("Qty","{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free">
                                    <ItemTemplate>
                                        <asp:Label ID="lbFree" runat="server" Text='<%# Eval("Free","{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="disc use">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdiscount_use" runat="server" Text='<%# Eval("discount_use") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Action" ShowSelectButton="True" ButtonType="Button" SelectText="Disc Calc" />
                            </Columns>
                            <SelectedRowStyle BackColor="#3399FF" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel70" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdDisc" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdDisc_RowDeleting" Width="1031px">
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
                                <asp:TemplateField HeaderText="item cd">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Pct">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsopDPct" runat="server" Text='<%# Eval("sopDPct", "{0:0.##%}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsopDQty" runat="server" Text='<%# Eval("sopDQty","{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsopDAmt" runat="server" Text='<%# Eval("sopDAmt","{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible='false'>
                                    <ItemTemplate>
                                        <asp:Label ID="lbSeqID" runat="server" Text='<%# Eval("SeqID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="#3399FF" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel63" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        <td colspan="4" style="text-align:center">
                <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btsave" runat="server" Text="Save" OnClick="btsave_Click" CssClass="button2 save" style="top: 0px; left: 0px; " />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btEdit" runat="server" CssClass="button2 edit" Text="Edit" OnClick="btEdit_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            <td>
                        <asp:Button ID="btDisCal" runat="server" CssClass="button2" OnClick="btDisCal_Click" Text="Disc Calc" />
                    </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btprint" runat="server" CssClass="button2 print" OnClick="btprint_Click" style="left: 0px; top: 0px" Text="Print SO" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btprintInv" runat="server" CssClass="button2 print" OnClick="btprintInv_Click" style="left: 0px; top: 0px" Text="Print Invoice" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            
        </tr>
    </table>
   
</asp:Content>

