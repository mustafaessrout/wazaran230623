<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_pohoentry.aspx.cs" Inherits="fm_pohoentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    
   <style>
        .autocomp {
            font-family:Tahoma,Verdana;
            font-size:smaller;
            width:400px;
        }
       </style>
    <script>
        function GetValue()
        {
           return ('test');
        }

        function ClearControl()
        {
            document.getElementById('<%=txsearchitem.ClientID%>').value = "";
            document.getElementById('<%=txqty.ClientID%>').value = "";
            document.getElementById('<%=txsearchitem.ClientID%>').focus();
        }
    </script>

     <script type="text/javascript">
         $(function () {
             $("#<%=dtpo.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>PURCHASE ORDER - HEAD OFFICE</strong>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>PO No.</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txpono" runat="server" CssClass="makeitreadonly"></asp:TextBox>
            </td>
            <td class="auto-style2">Date </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtpo" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp; &nbsp;</td>
            <td>
                </td>
        </tr>
       
        <tr>
            <td class="auto-style6">Referens</td>
            <td class="auto-style7">:</td>
            <td class="auto-style6">
                <asp:TextBox ID="txref" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style8">Vendor Selected</td>
            <td class="auto-style7">:</td>
            <td class="auto-style6">
                <asp:DropDownList ID="cbvendor" runat="server" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td class="auto-style6">
                </td>
            <td class="auto-style6">
                </td>
        </tr>
       
        <tr>
            <td class="kiri1">Currency</td>
            <td class="tengah1">:</td>
            <td class="kanan1">
                <asp:DropDownList ID="cbcurrency" runat="server">
                </asp:DropDownList>
            </td>
            <td class="auto-style2"></td>
            <td class="kiri1" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                         <strong>
                         <asp:Label ID="lbaddr" runat="server" CssClass="auto-style1" Text=""></asp:Label>
                         </strong>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
               
            </td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
        </tr>
       
        <tr>
            <td></td>
            <td></td>
            <td>
                </td>
            <td class="auto-style2"></td>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbcity" runat="server" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbvendor" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                </td>
            <td>
                </td>
        </tr>
       
        <tr>
            <td>Remark</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txremark" runat="server"></asp:TextBox>
                </td>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
        <tr>
            <td></td>
            <td></td>
            <td colspan="4" style="background-color: #C0C0C0">
                ITEM</td>
            <td style="background-color: #C0C0C0">
                QTY</td>
            <td style="background-color: #C0C0C0">
                </td>
        </tr>
       
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="4">
                <asp:TextBox ID="txsearchitem" runat="server" Width="100%"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" TargetControlID="txsearchitem" ServiceMethod="GetListItem" MinimumPrefixLength="1" 
                    EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="GetValue">
                </asp:AutoCompleteExtender>
            </td>
            <td>
                <asp:TextBox ID="txqty" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
                </td>
        </tr>
       
        <tr>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1" colspan="5">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grditem" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal" Width="100%" OnRowDeleting="grditem_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arabic">
                                    <ItemTemplate>
                                        <%# Eval("item_arabic") %>
                                    </ItemTemplate>
                                 </asp:TemplateField>   
                                <asp:TemplateField HeaderText="Packing">
                                    <ItemTemplate><%# Eval("packing") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%# Eval("price_buy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gross">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("price_buy")) * Convert.ToDouble(Eval("qty")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="kanan1">
                &nbsp;</td>
        </tr>
       
        <tr>
            <td class="auto-style3"></td>
            <td class="auto-style4"></td>
            <td class="auto-style3">
                TOTAL QTY</td>
            <td class="auto-style5">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                         <strong>
                         <asp:Label ID="lbtotalqty" runat="server" Text="0"></asp:Label>
                         </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </td>
            <td class="auto-style3" colspan="2">
                TOTAL GROSS</td>
            <td class="auto-style3">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lbtotalgross" runat="server" Text="0"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style3">
                </td>
        </tr>
       
        <tr>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="kiri1" colspan="2">
                &nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
        </tr>
       
        <tr>
            <td class="kiri1">&nbsp;</td>
            <td class="tengah1">&nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="kiri1" colspan="2">
                &nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
            <td class="kanan1">
                &nbsp;</td>
        </tr>
       
    </table>
    <div>
        <img src= "div2.png" alt="xx" class="divid"/>
    </div>
    <div class="navi">
        <asp:Button ID="btsaved" runat="server" Text="SAVE" OnClick="btsaved_Click" />
    </div>
</asp:Content>

