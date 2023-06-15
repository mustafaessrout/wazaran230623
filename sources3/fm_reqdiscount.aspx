<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqdiscount.aspx.cs" Inherits="fm_reqdiscount" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
<%-- <script>
       $(document).ready(function () {
           $(function () { $('#divremark').draggable(); });
           //$(function () { $("#divResize").draggable().resizable(); });
       });
    </script>--%>
   <style type="text/css">
 .divmsg{
   /*position:static;*/
   top:5%;
   right:5%;
   width:200px;
   height:90%;
   position:fixed;
   /*background-color:greenyellow;*/
   overflow-y:auto;
  }
    </style> 

    <script type = "text/javascript" >

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <style>
        .hidobject{
            display:none;
        }
        </style>
<%--<style>
       .showmessage {
           position: fixed;
           top: 50%;
           left: 50%;
           margin-top: -50px;
           margin-left: -50px;
           width: 170px;
           height: 170px;
           background: url(loader.gif) no-repeat center;
           display:normal;
       }

        .hidemessage {
           position: absolute;
           top: 50%;
           left: 50%;
           margin-top: -50px;
           margin-left: -50px;
           width: 150px;
           height: 150px;
           background: url(loader.gif) no-repeat center;
           display:none;
       }
       </style>--%>
     <script>
         function vEnableShow() {
            $get('showmessagex').className = "showmessage";
         }

         function vDisableShow() {
             $get('showmessagex').className = "hidemessage";
         }
    </script>
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }
        function SetContextKey() {
            $find('<%=txcust_AutoCompleteExtender.ClientID%>').set_contextKey($get("<%=cbcustsalespoint.ClientID %>").value);
        }
    </script>
  
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="divheader">
        Promotion request 
    </div>
    <img src="div2.png" class="divid"/>
    <div>
        Branch : 
        <asp:Label ID="lbsalespoint" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"></asp:Label>
    </div>
    <img src="div2.png" class="divid" />
    
    <table style="width:80%">
        <tr>
            <td>Discount No.</td>
            <td>:</td>
            <td>
                
                <asp:TextBox ID="txdiscno" runat="server" CssClass="makeitreadonly" Width="200px"></asp:TextBox>
            &nbsp;</td>
            <td>Date</td>
            <td>:</td>
            <td>
                
                <asp:TextBox ID="dtdisc" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtdisc_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdisc">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>Discount Type</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbdisctype" runat="server" OnSelectedIndexChanged="cbdisctype_SelectedIndexChanged" AutoPostBack="True" Width="200px"></asp:DropDownList>
            </td>
            <td>Status</td>
            <td>:</td>
            <td>
                
                <asp:Label ID="lbstatus" runat="server" Font-Bold="True" ForeColor="Red" Text="NEW"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Remark</td>
            <td>:</td>
            <td colspan="4">
                <asp:TextBox ID="txremark" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
       </table>
    <img src="div2.png" class="divid" />
    <div style="padding-bottom:10px;padding-top:10px">
        <strong>Document Related</strong>
    </div>
    
      <div class="divgrid">
        
                 <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="False" Width="80%" GridLines="None" CellPadding="0" PageSize="5" OnRowDataBound="grddoc_RowDataBound" ForeColor="#333333" OnRowEditing="grddoc_RowEditing" OnRowUpdating="grddoc_RowUpdating" OnRowCancelingEdit="grddoc_RowCancelingEdit" CssClass="mygrid">
                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Document Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate><%# Eval("doc_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DIC">
                            <ItemTemplate><%# Eval("dic_nm") %>
                                <asp:HiddenField ID="hddic" runat="server" Value='<%# Eval("dic") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upload ">
                            <ItemTemplate>
                                <asp:Label ID="lbfilename" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lbfilename" runat="server"></asp:Label>
                                <asp:FileUpload ID="uplfile" runat="server" />
                                
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select" Visible="False">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                     <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
             
                
            </div>
    <img src="div2.png" class="divid" />
       <table style="width:80%">
        <tr>
            <td>Vendor Ref. No.</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txvendorref" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>Proposal No.</td>
            <td>:</td>
            <td>
                
                <asp:TextBox ID="txrefno" runat="server" Width="20em"></asp:TextBox>
                
            </td>
        </tr>
       
        <tr>
            <td>Vendor</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbvendor" runat="server" Width="300px">
                </asp:DropDownList>
            </td>
            <td>Benefit Promotion</td>
            <td>:</td>
            <td>
                
                <asp:TextBox ID="txbenefit" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Discount Used To</td>
            <td>:</td>
            <td colspan="4">
                   <asp:RadioButtonList ID="rddiscusage" runat="server" BackColor="Gray" ForeColor="White" RepeatDirection="Horizontal" Width="100%" RepeatLayout="Flow" Height="22px">
                    <asp:ListItem Value="M">Manual</asp:ListItem>
                    <asp:ListItem Value="A">Automatic</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
      
        <tr>
            <td>Salespoint</td>
            <td>&nbsp;</td>
            <td colspan="4">
            <table style="width:100%;background-color:silver">
        <tr style="background-color:gray;color:white;font-weight:bold">
            <td>Salespoint</td>
            <td>Add</td>
            <td>Table</td>
        </tr>
        <tr>
            <td style="vertical-align:top">

                   <asp:DropDownList ID="cbsalespoint" runat="server" Width="100%">
                   </asp:DropDownList>

            </td>
            <td style="vertical-align:top">
                <asp:CheckBox ID="challsp" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="challsp_CheckedChanged" />
                <asp:Button ID="btaddsalespoint" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddsalespoint_Click" />
            </td>
            <td>
            <div class="divgrid">
            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                 <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="grdsp_PageIndexChanging" PageSize="5" Width="100%" OnRowDeleting="grdsp_RowDeleting" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CssClass="mygrid">
            <Columns>
                <asp:TemplateField HeaderText="Salespoint Code">
                    <ItemTemplate>
                        <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salespoint Name">
                    <ItemTemplate>
                        <%# Eval("salespoint_nm") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btaddsalespoint" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="challsp" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
    
    </div>
           
            </td>
        </tr>
    </table>
            </td>
        </tr>
      
        <tr>
            <td>Customer</td>
            <td>:</td>
            <td colspan="4">
                <asp:RadioButtonList ID="rdcustomer" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="80%" OnSelectedIndexChanged="rdcustomer_SelectedIndexChanged" BackColor="Gray" ForeColor="White" RepeatLayout="Flow" onClick="vEnableShow()" Height="22px" CellPadding="0" CellSpacing="0">
                    <asp:ListItem Value="C">By Customer</asp:ListItem>
                    <asp:ListItem Value="G">By Customer Group</asp:ListItem>
                    <asp:ListItem Value="T">By Customer Type</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
       
        <tr>
            <td>
                <asp:HiddenField ID="hdcust" runat="server" />
                <asp:HiddenField ID="hdsp" runat="server" />
            </td>
            <td></td>
            <td colspan="4" style="position:relative">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                         <asp:DropDownList ID="cbcustsalespoint" runat="server" OnSelectedIndexChanged="cbcustsalespoint_SelectedIndexChanged" Width="20em">
                             <asp:ListItem>SELECT SALESPOINT</asp:ListItem>
                         </asp:DropDownList>
                         <asp:TextBox ID="txcust" runat="server" Width="50%" onkeyup="SetContextKey()"></asp:TextBox>
                        <div id="divwidthc" style="font-family:Calibri;font-size:small"></div>
                         <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionCust" TargetControlID="txcust" UseContextKey="false" FirstRowSelected="false" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="CustSelected" MinimumPrefixLength="1" CompletionListElementID="divwidthc">
                         </asp:AutoCompleteExtender>
                         <asp:DropDownList ID="cbcust" runat="server" Width="50%">
                            </asp:DropDownList>
                        <asp:CheckBox ID="chall" runat="server" Text="All" />
                        
                <asp:Button ID="btaddcust" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddcust_Click"/>
                    </ContentTemplate>
                    
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdcustomer" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
               
            </td>
        </tr>
       
        </table>
    
    <div style="padding-left:110px">
         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
         <ContentTemplate>
                 <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdcust_PageIndexChanging" GridLines="Horizontal" CellPadding="0" Width="80%" OnRowDeleting="grdcust_RowDeleting" PageSize="5" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CssClass="mygrid">
                 <Columns>
                     <asp:TemplateField HeaderText="Salespoint">
                         <ItemTemplate>
                             <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust Code">
                    <ItemTemplate>
                        <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust Name">
                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate><%# Eval("addr") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact">
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate><%# Eval("phone_no") %></ItemTemplate>
                </asp:TemplateField>
                     <asp:CommandField ShowDeleteButton="True" />
            </Columns>
                     <FooterStyle BackColor="White" ForeColor="#333333" />
                     <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                     <RowStyle BackColor="White" ForeColor="#333333" />
                     <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                     <SortedAscendingCellStyle BackColor="#F7F7F7" />
                     <SortedAscendingHeaderStyle BackColor="#487575" />
                     <SortedDescendingCellStyle BackColor="#E5E5E5" />
                     <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
            <asp:GridView ID="grdcustgroup" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdcustgroup_RowDeleting" AllowPaging="True" OnPageIndexChanging="grdcustgroup_PageIndexChanging" GridLines="Horizontal" CellPadding="0" Width="80%" PageSize="5" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CssClass="mygrid">

                <Columns>
                    <asp:TemplateField HeaderText="Group Code">
                        <ItemTemplate>
                            <asp:Label ID="lbcustgroup" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Group Name">
                        <ItemTemplate>
                                 <%# Eval("cusgrnm") %>
                        </ItemTemplate>
                   
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>

                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />

            </asp:GridView>
                <asp:GridView ID="grdcusttype" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="0" OnRowDeleting="grdcusttype_RowDeleting" Width="80%" PageSize="5" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CssClass="mygrid">
                    <Columns>
                        <asp:TemplateField HeaderText="Cust Type Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcusttype" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Type Name">
                            <ItemTemplate><%# Eval("custtyp_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                 </asp:GridView>
                 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btaddcust" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
     
    </div>
 
    <div class="divheader">
        <img src="div5.png" class="divid" />
        Salespoint
    </div>
    
    
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Product / Group Product
            
            </td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rditem" runat="server" AutoPostBack="True" Height="23px" RepeatDirection="Horizontal" Width="400px" OnSelectedIndexChanged="rditem_SelectedIndexChanged" BackColor="Gray" ForeColor="White" Font-Bold="True">
                    <asp:ListItem Value="P">By Item</asp:ListItem>
                    <asp:ListItem Value="G">By Product</asp:ListItem>
                </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                

            </td>
            <td>

                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hditem" runat="server" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                            <asp:TextBox ID="txitemsearch" runat="server" Width="30em"></asp:TextBox>
                          
                            <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" OnClientItemSelected="ItemSelected" CompletionListElementID="divwidth" MinimumPrefixLength="1">
                            </asp:AutoCompleteExtender>
                      
                            <asp:DropDownList ID="cbitemproduct" runat="server" OnSelectedIndexChanged="cbitemproduct_SelectedIndexChanged" AutoPostBack="true" Width="30%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="cbgroupprod" runat="server" OnSelectedIndexChanged="cbgroupprod_SelectedIndexChanged" AutoPostBack="true" Width="30%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="cbprod" runat="server" Width="35%">
                            </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rditem" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

              
            </td>
            <td>

                <asp:Button ID="btadditem" runat="server" Text="Add" CssClass="button2 add" OnClick="btadditem_Click" />
            </td>
        </tr>
    </table>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                 <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False" Width="75%" AllowPaging="True" OnRowDeleting="grditem_RowDeleting" ForeColor="#333333" GridLines="None" CellPadding="0" PageSize="5" CssClass="mygrid">
                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                 <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <%# Eval("item_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Brand">
                    <ItemTemplate>
                        <asp:Label ID="lbbranded" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
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
                <asp:GridView ID="grdproduct" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" Width="75%" CellPadding="0" OnRowDeleting="grdproduct_RowDeleting" CssClass="mygrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbprodcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
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
                <asp:AsyncPostBackTrigger ControlID="btadditem" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
     </div>
    <img src="div2.png" class="divid">
    <table>
        <tr>
            <td>
                Regular Cost
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txregularcost" runat="server"></asp:TextBox>
            </td>
            <td>Net Cost</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txnetcost" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
                Start Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtstart" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
            <td>End Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtend" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>

        <tr>
            <td>
                Delivery Date</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtdelivery" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtdelivery_CalendarExtender" runat="server" TargetControlID="dtdelivery" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
            <td>Maximum Clam Process</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtmaxclaim" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtmaxclaim_CalendarExtender" runat="server" TargetControlID="dtmaxclaim" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>

        <tr>
            <td>
                Order Minumum</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txminqty" runat="server" OnTextChanged="txminqty_TextChanged" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>Maximum Order</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txmaxorder" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                Catalog Image</td>
            <td>:</td>
            <td colspan="4">
                <asp:FileUpload ID="uplcatalog" runat="server" Width="400px" />
            </td>
        </tr>
        </table>
    <img src="div2.png" class ="divid" />
    <div class="divheader">
        Discount Issued
    </div>
    <table style="width:80%">
        <tr>
            <td colspan="6">
             
        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
            <div style="float:left;width:80%;padding:10px">
                <asp:GridView ID="grdissued" runat="server" AutoGenerateColumns="False" Width="100%" ForeColor="#333333" GridLines="None" CellPadding="0" PageSize="5" CssClass="mygrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Issued Code">
                            <ItemTemplate>
                                <asp:Label ID="lbissuedcode" runat="server" Text='<%# Eval("fld_valu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issued Name">
                            <ItemTemplate><%# Eval("fld_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                             <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
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
            </div>
            
            </ContentTemplate>
           
            </asp:UpdatePanel>
       
  
            </td>
        </tr>
        </table>
       
        
  
   
     <img src="div2.png" class="divid" />
    <div style="padding-top:10px;padding-bottom:10px">
       <strong> Discount Mechanism : </strong>
                <asp:DropDownList ID="cbdiscountmech" runat="server" Width="200px" OnSelectedIndexChanged="cbdiscountmech_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
    </div>
    <div>
        <table>
            <tr style="background-color:gray;border-color:silver;color:white;font-weight:bolder">
                <td>
                    Order Minimum Limit
                </td>
                <td>UOM</td>
                <td>
                    Free
                    Qty</td>
                <td>
                    UOM Free</td>
                <td>
                    Cash Value
                </td>
                <td>
                    % Cash Value</td>
                <td>
                    Multiple /  Scale
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txordermin" runat="server" Width="50px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txminqty" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                     
                </td>
                <td>
                    <asp:DropDownList ID="cbuom" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txqty" runat="server" Width="4em"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbdiscountmech" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </td>
                <td>
                    <asp:DropDownList ID="cbuomfree" runat="server"></asp:DropDownList>
                   
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                              <asp:TextBox ID="txcash" runat="server" Width="4em"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbdiscountmech" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                  
                </td>
                <td>
                  
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txpercent" runat="server" Width="4em"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>                 
                   
                  
                                     
                </td>
                <td>
                    <asp:DropDownList ID="cbmethod" runat="server" Width="200px"></asp:DropDownList><asp:Button ID="btaddmethod" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddmethod_Click"/>
                </td>
         </tr>
        </table>
     </div>
    <img src="div2.png" class="divid" />
    <div style="padding-left:110px">
        
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
            <asp:GridView ID="grdformula" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" OnRowDeleting="grdformula_RowDeleting" Width="80%" CssClass="mygrid">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Order Min">
                    <ItemTemplate>
                        <asp:Label ID="lbminqty" runat="server" Text='<%# Eval("min_qty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Free Qty">
                    <ItemTemplate>
                        <%# Eval("qty") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM Free">
                    <ItemTemplate><%# Eval("uom_free") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Free Cash">
                    <ItemTemplate>
                        <%# Eval("amt") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Free Percent">
                    <ItemTemplate>
                        <%# Eval("percentage") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Multiple/Scale">
                    <ItemTemplate><%# Eval("disc_typ_nm") %></ItemTemplate>
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
                <asp:AsyncPostBackTrigger ControlID="btaddmethod" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
        
      
    </div>
    <img src="div2.png" class="divid" />
     <div style="font-size:medium;font-weight:bolder;color:white;background-color:GrayText">Free Item</div>
    
       <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
        <table runat="server" id="tbdiscount">
        <tr>
            <td>
               
                <table><tr style="background-color:silver">
                    <td>
                        Free Item/Product Group</td>
                    <td>
                        <asp:RadioButtonList ID="rdfreeitem" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdfreeitem_SelectedIndexChanged">
                            <asp:ListItem Value="I">Item</asp:ListItem>
                            <asp:ListItem Value="G">Product Group</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        Select product/Group</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                    <tr style="background-color:silver">
                        <td>Branded </td>
                        <td>Group Product </td>
                        <td>Product </td>
                        <td>UOM</td>
                        <td>Action</td>
                    </tr>
                <tr>
                <td>
                <asp:DropDownList ID="cbbrandedfree" runat="server" OnSelectedIndexChanged="cbbrandedfree_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                </asp:DropDownList></td><td>
                <asp:DropDownList ID="cbprodgroupfree" runat="server" Width="200px" OnSelectedIndexChanged="cbprodgroupfree_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList></td><td>
                <asp:DropDownList ID="cbitemfree" runat="server" Width="300px" OnSelectedIndexChanged="cbitemfree_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btadditemfree" runat="server" CssClass="button2 add" OnClick="btadditemfree_Click" style="left: 0px; top: 0px" Text="Add" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                </td></tr></table>
           
                <table style="width:100%">
                    <tr>
                        <td>
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbitemfrees" runat="server" Width="100%" Visible="False">
                        </asp:DropDownList></td><td>
                    <asp:Button ID="btaddfree" runat="server" CssClass="button2 add" OnClick="btaddfree_Click" style="left: 0px; top: 0px" Text="Add" Visible="False" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                 </td></tr></table>
                   
             </td>
            <td style="vertical-align:bottom">

                &nbsp;</td>
        </tr>
    </table>
           </ContentTemplate>
                 </asp:UpdatePanel>   
    <div class="divgrid">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>

                    <asp:GridView ID="grdfreeitem" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" PageSize="5" Width="75%" CssClass="mygrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name"><ItemTemplate><%# Eval("item_desc") %></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                           <ItemTemplate> <%# Eval("size") %></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Branded">
                            <ItemTemplate>
                                <asp:Label ID="lbbranded" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate><%# Eval("uom") %></ItemTemplate>
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
            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                <ContentTemplate>
                <asp:GridView ID="grdfreeproduct" runat="server" AllowPaging="True" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" PageSize="5" Width="75%" CssClass="mygrid">
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
            </asp:UpdatePanel>

     </div>
  
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 save" OnClick="btnew_Click" />
        <asp:Button ID="btedit" runat="server" Text="Edit" CssClass="button2 edit" OnClick="btedit_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
    
            <div id="showmessagex" class="hidemessage">
            </div> 
    <table>
        <tr>
            <td style="position:relative">
                <div id="divwidth"></div>
            </td>
        </tr>
    </table>
    <div id="divremark" class="divmsg" draggable="true">
        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" Font-Size="Smaller" GridLines="Horizontal" Height="100%" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Discount No.">
                            <ItemTemplate><a href="javascript:popupwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd")%>');"><%# Eval("disc_cd") %></a></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
       
    </div>
</asp:Content>

