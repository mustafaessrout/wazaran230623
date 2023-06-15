<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstdiscount.aspx.cs" Inherits="fm_mstdiscount" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .bluewhite {
            color: #FFFFFF;
            background-color: #000;
            font-weight:bold;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
            margin-left: 40px;
        }
        .auto-style4 {
            width: 90px;
        }
        .hidbt{display:none;}
        .auto-style5 {
            height: 20px;
            font-weight: bold;
        }
        .auto-style6 {
            width: 10px;
        }
        .auto-style7 {
            height: 20px;
            font-weight: bold;
            width: 10px;
        }
        .auto-style8 {
            height: 20px;
            width: 10px;
        }
        </style>
    
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script type="text/javascript">
         $(function () {
             $("#<%=dtstart.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
             $("#<%=dtend.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });

      
     </script>
    <script>
        function updpnl()
        {
            document.getElementById('<%=Button2.ClientID%>').click();
            return(false);
           
        }
    </script>
    <script>
        function openwindow(url) {
            if (<%=cbschtyp.ClientID%>.value.toString() == 'A'){
                var oNewWindow = window.open("fm_lookup.aspx", "lookup", "height=400,width=700,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
               
            }else
            {
                var oNewWindow = window.open("fm_lookup2.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
               
            } 
        }

        function openwindow2() {
            var oNewWindow = window.open("fm_lookupcustomer.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
     </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
        <asp:Label ID="lbdiscount" runat="server" Text="Discount Scheme"></asp:Label>
  </h3><hr style="color:blue" />
    <table style="width:100%;">
        <tr>
            <td style="text-align:right;" class="auto-style2">Discount Code</td>
            <td class="auto-style6">:</td>
            <td style="width:30%;text-align:left">
                <asp:TextBox ID="txcode" runat="server" Width="215px"></asp:TextBox>    
            </td>
            <td>
                &nbsp;Type    
            </td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="cbschtyp" runat="server" Height="17px" Width="228px" AutoPostBack="true" OnSelectedIndexChanged="cbschtyp_SelectedIndexChanged"></asp:DropDownList>    
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">Proposal / Ref</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="txref" runat="server" Width="215px"></asp:TextBox>    
            </td>
            <td>
                Description</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txdesc" runat="server" Height="16px" TextMode="MultiLine" Width="214px"></asp:TextBox>    
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
            </td>
            <td class="auto-style6">:</td>
            <td>
                <asp:TextBox ID="dtstart" runat="server"></asp:TextBox>    
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="End Date"></asp:Label>
            </td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="dtend" runat="server"></asp:TextBox>    
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                <asp:Label ID="Label3" runat="server" Text="Program"></asp:Label>
            </td>
            <td class="auto-style6">:</td>
            <td>
                <asp:DropDownList ID="cbprogram" runat="server" Height="17px" Width="228px"></asp:DropDownList>    
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Payment Type"></asp:Label>
            </td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="cbpaymenttype" runat="server" Height="17px" Width="228px"></asp:DropDownList>    
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td class="bluewhite" colspan="4"><strong>CUSTOMER TYPE</strong></td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td colspan="2">
                <asp:RadioButtonList ID="rdcust" runat="server" Width="214px" AutoPostBack="true" OnSelectedIndexChanged="rdcust_SelectedIndexChanged" style="background-color:#0094ff;color:white">
                    <asp:ListItem Value="CT">Customer Type</asp:ListItem>
                    <asp:ListItem Value="CN">Customer Name</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                Customer Type</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                          <asp:DropDownList ID="cbcusttyp" runat="server" Height="17px" Width="228px"></asp:DropDownList>
                        <asp:Button ID="btselectcust" runat="server" Text="Select Customer" CssClass="button2 search" OnClientClick="javascript:openwindow2()" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
              </td>
            <td>
                <asp:Button ID="btcust" runat="server" Text="Add" OnClick="btcust_Click" CssClass="button2 add" Height="29px" Width="92px" />
  
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="* Select Customer Type !"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td colspan="4">
                <asp:UpdatePanel ID="updcust" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal">
                            <Columns>
                               
                                <asp:TemplateField HeaderText="Del">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkdel" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Customer Type Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtypecode" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Name">
                                    <ItemTemplate><%# Eval("typ_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                
                               
                            </Columns>
                        </asp:GridView>
                        
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btcust" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdbycust" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal">
                            <Columns>
                                <asp:TemplateField HeaderText="Del">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chDel" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcusttype" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcustname" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lbaddr" runat="server" Text='<%# Eval("addr") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcity" runat="server" Text='<%# Eval("city") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div style="text-align:left">
                    <asp:Button ID="btdel" runat="server" Text="Delete" OnClick="btdel_Click" CssClass="button2 delete" /></div>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td colspan="4" class="bluewhite">PRODUCT</td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                Branded</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:DropDownList ID="cbbranded" runat="server" Height="17px" Width="228px" OnSelectedIndexChanged="cbbranded_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            <td>Product</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="updprod" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbproduct" runat="server" Height="16px" Width="211px" AutoPostBack="true" OnSelectedIndexChanged="cbproduct_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="btaddtype" runat="server" Text="Add"  CssClass="button2 add"/>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="cbbranded" EventName="SelectedIndexChanged" /></Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                Item</td>
            <td class="auto-style6">:</td>
            <td>
                <asp:UpdatePanel ID="updbranded" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbitem" runat="server" AutoPostBack="true" Height="17px" Width="228px">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="cbproduct" EventName="SelectedIndexChanged" /></Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:Button ID="btitemadd" runat="server" CssClass="button2 add" Text="Add" OnClick="btitemadd_Click" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="* Select Item !"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td colspan="4">
                <asp:UpdatePanel ID="updpnlitem" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grditem" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal">
                            <Columns>
                                <asp:TemplateField HeaderText="Del">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkitemdel" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name"><ItemTemplate><%# Eval("item_nm") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Alias"><ItemTemplate><%# Eval("item_arabic") %></ItemTemplate></asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btitemadd" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                <asp:Button ID="btitemdel" runat="server" Text="Delete" OnClick="btitemdel_Click" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2" colspan="6">
              <hr style="color:blue" /></td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style5">
                Item Free</td>
            <td class="auto-style7">:</td>
            <td colspan="4" class="auto-style2">
                                                    <input id="bsearchfree" type="button" value="Select Free Item" onclick="openwindow('fm_lookupitem.aspx')" class="button2 search" /><asp:Button ID="Button2" runat="server" Text="negehek" OnClick="Button2_Click" CssClass="hidbt" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style8">&nbsp;</td>
            <td colspan="4" class="auto-style2">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdfreeitem" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate><%# Eval("item_cd")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Arabic">
                                                                <ItemTemplate><%# Eval("item_arabic") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size"></asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers><asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" /></Triggers>
                                            </asp:UpdatePanel>
                                        </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2" colspan="6">
               <hr style="color:blue" /></td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                Discount</td>
            <td class="auto-style8">:</td>
            <td colspan="4" class="auto-style2">
                        <asp:DropDownList ID="cbdiscscheme" runat="server" AutoPostBack="true" Height="17px" Width="321px" OnSelectedIndexChanged="cbdiscscheme_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                </td>
            <td class="auto-style8"></td>
            <td colspan="4" class="auto-style2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                         <table>
                            <tr>
                                <td>
                                    Min Qty :
                                </td>
                                <td>
                                    <asp:TextBox ID="txminqty" runat="server" Width="64px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbfree" runat="server" Text="Free :"></asp:Label>
                                    &nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txfree" runat="server" Width="63px"></asp:TextBox>
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="lbitemcode" runat="server" Text="Method : "></asp:Label>
                                </td>
                                <td class="auto-style4">
                                    <asp:DropDownList ID="cbscale" runat="server" Height="16px" Width="91px">
                                    </asp:DropDownList>
                                </td>
                                <td>

                                    &nbsp;</td>
                                <td>

                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <Triggers><asp:AsyncPostBackTrigger ControlID="cbschtyp" EventName="SelectedIndexChanged" /></Triggers>
                                    </asp:UpdatePanel>
                                   
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    </asp:UpdatePanel>
                                   
                                </td>
                                <td>
                                    <asp:Button ID="btdiscadd" runat="server" OnClick="btdiscadd_Click" Text="&gt;&gt;&gt;" />
                                </td>
                            </tr>
                        </table>

                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="cbdiscscheme" EventName="SelectedIndexChanged" /></Triggers>
                </asp:UpdatePanel>
               

            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                </td>
            <td class="auto-style8"></td>
            <td class="auto-style2" colspan="4">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal" Visible="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Del">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkdeldisc" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Minimum Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbmin" runat="server" Text='<%# Eval("min_qty") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Qty">
                                    <ItemTemplate><%# Eval("free_qty") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Method">
                                    <ItemTemplate><%# Eval("disc_method_nm") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btdiscadd" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                <asp:Button ID="btdiscmetdel" runat="server" OnClick="btdiscmetdel_Click" Text="Delete" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2" colspan="6">
                <hr style="color:blue" /></td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btsave" runat="server" OnClick="btsave_Click" Text="Save" Width="82px" CssClass="button2 save" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style2">
                &nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
     
</asp:Content>

