<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_itempriceentry.aspx.cs" Inherits="fm_itempriceentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookupitem.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl()
        {
           
        }

        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value =e.get_value();
          //  alert(e.get_value());
        }

       
    </script>
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
<style type="text/css">

.autocomp
{
    font-size:smaller;
    font-family:Tahoma,Verdana;
}
div
{
   border:none;
}
.divtable
{
    display:table;
}
.divrow
{
    display:table-row;
}
.divcol
{
    display:table-cell;
    width:100px;
}
.headrow
{
    display:table-caption;
    background-color:#808080;
}
.content
{
    display:table-cell;
    width:100px;
}
    
</style>
<link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader" style="font-size:16px">
        PRICE LEVEL ENTRY
    </div>
     <img src="div2.png" class="divid" />
    <table style="width:100%">
        <tr>
            <td>
                Price Level Code
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="txpricelevelcode" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqpricecode" runat="server" ControlToValidate="txpricelevelcode" ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                Name
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="txpricename" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqpricename" runat="server" ControlToValidate="txpricename" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Start Date</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="dtstart" runat="server"></asp:TextBox>
            </td>
            <td>
                End Date</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="dtend" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                UOM</td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="cbuom" runat="server" Height="16px" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Customer Type</td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="cbcusttype" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        </table>
    &nbsp;<table>

        <tr style="background-color:gray>
            <td>
                <strong>ITEM CODE</strong></td>
            <td>
                </td>
            <td>
                I<strong>TEM NAME</strong></td>
            <td colspan="3">
                <strong>Price</strong></td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="txsearch" runat="server" Height="16px" Width="400px" CssClass="makeitupper"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" runat="server" TargetControlID="txsearch" CompletionInterval="10" CompletionSetCount="1" CompletionListCssClass="autocomp" 
                     FirstRowSelected="false" EnableCaching="false" ServiceMethod="GetListItem" MinimumPrefixLength="1" UseContextKey="True" OnClientItemSelected="ItemSelected">
                </asp:AutoCompleteExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="txqty" runat="server" Height="16px" Width="87px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
             
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btadd" runat="server" OnClick="btadd_Click" Text="ADD" CssClass="button2 add" />
                <asp:HiddenField ID="hditem" runat="server" />
            </td>
        </tr>
        </table>

            <div class="divgrid">
     
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                         <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="75%" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Arabic"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
               
   </div>
   
   <div class="navi">
        <img src="div2.png" class="divid" />
       <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="button2 add" /><asp:Button ID="btedit" runat="server" Text="EDIT" CssClass="button2 edit" /><asp:Button ID="btsave" runat="server" Text="SAVE" CssClass="button2 save" OnClick="btsave_Click" /></div>
</asp:Content>

