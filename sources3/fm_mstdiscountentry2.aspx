<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstdiscountentry2.aspx.cs" Inherits="fm_mstdiscountentry2" EnableViewState="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
    function openwindow(url) {
            if (<%=cbdisctype.ClientID%>.value.toString() == 'A'){
                var oNewWindow = window.open("fm_lookup.aspx", "lookup", "height=400,width=700,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
               
            }else
            {
                var oNewWindow = window.open("fm_lookup2.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
               
            } 
    }
    </script>
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=dtstart.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
            $("#<%=dtend.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });

</script>
    
      
    <style type="text/css">
        .auto-style1 {
            height: 27px;
        }
    </style>
    
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Discount Schema<asp:Image ID="Image1" runat="server" Height="61px" ImageUrl="~/dct.png" />
        :
        <asp:Label ID="lbsp" runat="server" Text="Label"></asp:Label>
    </h3>
    <img src="div2.png" class="divid"/>
    <div>
        <table>
            <tr>
                <td class="auto-style1">
                        Discount Code
                </td>
                <td class="auto-style1">
                    :
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txdiscountcode" runat="server" Height="17px" Width="168px" CssClass="makeitreadonly"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    Discount Name
                </td>
                <td class="auto-style1">
                    :    
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txdiscountname" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    </td>
            </tr>
            <tr>
                <td class="kiri1">
                        Effective Date</td>
                <td class="tengah1">
                    :</td>
                <td class="kanan1">
                    <asp:TextBox ID="dtstart" runat="server" Height="19px" Width="165px"></asp:TextBox>
                </td>
                <td class="kiri1">
                    Expired Date</td>
                <td>
                    :</td>
                <td class="kanan1">
                    <asp:TextBox ID="dtend" runat="server"></asp:TextBox>
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td class="kiri1">
                        Remark</td>
                <td class="tengah1">
                    :</td>
                <td colspan="2">
                    <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        Discount </td>
                <td class="tengah1">
                    :</td>
                <td colspan="2">
                    <asp:DropDownList ID="cbdisctype" runat="server" Width="303px">
                    </asp:DropDownList>
                </td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="7">
                        <img src="div2.png" class="divid" /></td>
            </tr>
            </table>
         <table style="width:100%">
             <tr>
                 <td>
                     Salespoint
                 </td>
                 <td>
                     :
                 </td>
                 <td>

                    <asp:DropDownList ID="cbsalespoint" runat="server" Width="217px">
                    </asp:DropDownList>

                    <asp:Button ID="btaddsp" runat="server" Text="Add" class="button2 add" OnClick="btaddsp_Click"/>

                 </td>
                 <td>

                     &nbsp;</td>
             </tr>
         </table>
        <table style="width:100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                              <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="grdsp_PageIndexChanging" OnRowDeleting="grdsp_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Salespoint Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespointcode" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:CommandField ShowDeleteButton="True" />
                          
                        </Columns>
                    </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddsp" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                            

                </td>
            </tr>
        </table>
        <table style="width:100%">
            <tr>
                <td>Customer Type</td>
                <td>:</td>
                <td>
                            <asp:DropDownList ID="cbcusttype" runat="server" Width="217px">
                            </asp:DropDownList>

                    <asp:Button ID="btaddcusttype" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddcusttype_Click" style="left: 0px; top: 1px" />

                </td>
                <td>

                    &nbsp;</td>
            </tr>
        </table>
        <table style="width:100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdcusttype" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="grdcusttype_RowDeleting">
                                <Columns>
                                   
                                    <asp:TemplateField HeaderText="Type Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcusttype" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Customer Type Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtypename" runat="server" Text='<%# Eval("typ_nm") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddcusttype" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                            

                </td>
            </tr>
        </table>
        <table style="width:100%">
            <tr>
                <td>
                    Customer Group
                </td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                             <asp:DropDownList ID="cbgroupcustomer" runat="server" Width="217px">
                            </asp:DropDownList>
                             <asp:Button ID="btaddgroup" runat="server" CssClass="button2 add" OnClick="btaddgroup_Click" style="left: 0px; top: 1px" Text="Add" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddcusttype" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
                <td>

                    &nbsp;</td>
            </tr>
        </table>
        <table style="width:100%">
            <tr>
                <td>

                       <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                           <ContentTemplate>
                                <asp:GridView ID="grdgroupcust" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="grdgroupcust_RowDeleting" AllowPaging="True" OnPageIndexChanging="grdgroupcust_PageIndexChanging" CssClass="auto-style1">
                                <Columns>
                                    <asp:TemplateField HeaderText="Group Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcusgrcd" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                           </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger  ControlID="btaddcusttype" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                           

                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="7">
                    <img src="div2.png" class="divid" />
                </td>
                
            </tr>
            <tr>
                <td class="kiri1">
                        Discount By</td>
                <td class="tengah1">
                    :</td>
                <td colspan="4">
                    <asp:DropDownList ID="cbdiscby" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbdiscby_SelectedIndexChanged" Width="300px">
                        <asp:ListItem Value="I">Item</asp:ListItem>
                        <asp:ListItem Value="P">Product</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr style="background-color:#808080">
                <td>
                        ITEM DISCOUNT</td>
                <td>
                    :</td>
                <td colspan="4">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div id="dvop1" runat="server">
                            <asp:TextBox ID="txsearchitem" runat="server" Width="511px"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" OnClientItemSelected="ItemSelected" EnableCaching="false">
                            </asp:AutoCompleteExtender>
                            <asp:Button ID="btaddit" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddit_Click" /><asp:HiddenField ID="hditem" runat="server" />
                            </div>
                            <div id="dvop2" runat="server">
                                <asp:DropDownList ID="cbbranded" runat="server" AutoPostBack="true" Height="23px" OnSelectedIndexChanged="cbgroupproduct_SelectedIndexChanged" Width="173px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="cbgroupproduct" runat="server" AutoPostBack="true" Height="23px" OnSelectedIndexChanged="cbgroupproduct_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="btaddprod" runat="server" Text="Add"  CssClass="button2 add" OnClick="btaddprod_Click"/>
                            </div>
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbdiscby" EventName="SelectedIndexChanged" />
                    </Triggers>    
                    </asp:UpdatePanel>
                    
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                        </td>
                <td>
                    </td>
                <td colspan="4">
                    </td>
                <td>
                    </td>
            </tr>
            </table>
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grditemdiscount" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grditemdiscount_PageIndexChanging" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arabic">
                                        <ItemTemplate><%# Eval("item_arabic") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM"></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Packing"></asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddprod" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btaddit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <img src="div2.png" class="divid" />
        <table>
            <tr>
                <td>
                    Discount Method
                </td>
                <td>
                    <asp:DropDownList ID="cb" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    Item Code
                </td>
                <td>
                    Item Name
                </td>
                <td>Minumum Qty</td>
                <td>
                    Amount
                </td>
                <td>
                    Percentage
                </td>
                <td>Free Qty</td>
                <td>
                    Add</td>
                </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txitem" runat="server" Width="400px"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" CompletionSetCount="1" CompletionInterval="10" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="txqty" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txamt" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txpct" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                     <asp:TextBox ID="txdiscqty" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
                </td>
                </tr>
        </table>
       <table>
           <tr>
               <td>
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                       <ContentTemplate>
                           <asp:GridView ID="grddiscount" runat="server"></asp:GridView>
                       </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                       </Triggers>
                   </asp:UpdatePanel>
                   
               </td>
           </tr>
       </table>
        <table>
            <tr>
                <td colspan="6">
                        &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td colspan="4">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        Branded</td>
                <td class="tengah1">
                    :</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    Group Product</td>
                <td class="tengah1">
                    :</td>
                <td class="kanan1">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbbranded" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        Product</td>
                <td class="tengah1">
                    :</td>
                <td colspan="2">
                    
                   
                </td>
                <td class="tengah1">
                    <asp:Button ID="btadditem" runat="server" Text="Add " CssClass="button2 add" />
                </td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="kiri1">
                        &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kiri1">
                    &nbsp;</td>
                <td class="tengah1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
                <td class="kanan1">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

