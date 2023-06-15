<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookuppo.aspx.cs" Inherits="fm_lookuppo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Searching PO</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/styles.css" rel="stylesheet" />
 <script>
     function closewin()
     {
         window.opener.updpnl();
         window.close();
     }
 </script>
 </head>
 <body style="font-family:Verdana,Tahoma;font-size:small">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="tsmanager" runat="server"></asp:ToolkitScriptManager>
    <div>
        Search Purchase Order :
    </div>
        <div style="background-color:#e8e3e3">
           <table style="width:100%">
               <tr>
                   <td>
                       PO Status</td>
                   <td>:</td>
                   <td>
                       <asp:DropDownList ID="cbstatus" runat="server" Height="16px" Width="231px">
                       </asp:DropDownList>
                   </td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td>
                       Search
                   </td>
                   <td>:</td>
                   <td>
                       <asp:TextBox ID="txSearch" runat="server" Height="21px" Width="225px"></asp:TextBox>
                       <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="button2 search" OnClick="btsearch_Click" />
                   </td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td>
                       &nbsp;</td>
                   <td>&nbsp;</td>
                   <td>
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td colspan="3">
                       <hr style="color:blue" /></td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td colspan="4">
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                               <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grdsearch_SelectedIndexChanged" BorderStyle="None" GridLines="Horizontal">
                                   <Columns>
                                       <asp:TemplateField HeaderText="PO Number">
                                           <ItemTemplate>
                                               <asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>'></asp:Label>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Sales Point">
                                           <ItemTemplate>
                                               <asp:Label ID="lbsp" runat="server" Text='<%# Eval("salespoint_nm") %>'></asp:Label>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Date">
                                           <ItemTemplate><%# Eval("po_dt","{0:dd-MM-yyyy}") %></ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Due Date"></asp:TemplateField>
                                       <asp:CommandField ShowSelectButton="True" />
                                   </Columns>
                                   <SelectedRowStyle BackColor="#66CCFF" />
                               </asp:GridView>
                           </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                           </Triggers>
                       </asp:UpdatePanel>
                   </td>
               </tr>
               <tr>
                   <td>
                       &nbsp;</td>
                   <td>&nbsp;</td>
                   <td>
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td colspan="4">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                               <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal">
                                   <Columns>
                                       <asp:TemplateField HeaderText="Item Code">
                                           <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Name">
                                           <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Arabic"></asp:TemplateField>
                                       <asp:TemplateField HeaderText="Qty Order">
                                           <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="UOM"></asp:TemplateField>
                                       <asp:TemplateField HeaderText="Unit Price"></asp:TemplateField>
                                   </Columns>
                               </asp:GridView>
                           </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="grdsearch" EventName="SelectedIndexChanged" />
                           </Triggers>
                       </asp:UpdatePanel>
                   </td>
               </tr>
               <tr>
                   <td>
                       &nbsp;</td>
                   <td>&nbsp;</td>
                   <td>
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
              
           </table>
        </div>
        <div style="text-align:center;padding:10px">
            <asp:Button ID="btok" runat="server" Text="OK" CssClass="button2 save" OnClick="btok_Click" /> </div>
    </form>
</body>
</html>
