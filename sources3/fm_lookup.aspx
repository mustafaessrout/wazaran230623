<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookup.aspx.cs" Inherits="fm_lookup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 73px;
        }
        .auto-style2 {
            text-align: center;
        }
    </style>
    <script>
        function refreshopener()
        {
            window.opener.document.updpnl();
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="tsScriptManager" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:#dedcdc;font-family:Verdana,Tahoma;font-size:small">
        <h3>Discount Schema</h3>
        <table>
            <tr>
                <td class="auto-style1">
                    Brand : 
                </td>
                <td>
                    <asp:DropDownList ID="cbbranded" runat="server" Height="21px" Width="139px" OnSelectedIndexChanged="cbbranded_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>Product : </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate><asp:DropDownList ID="cbproduct" runat="server" Height="21px" Width="227px">
                    </asp:DropDownList></ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="cbbranded" EventName="SelectedIndexChanged" /></Triggers>
                    </asp:UpdatePanel>
                    
                </td>
                <td>
                    <asp:Button ID="btsearch" runat="server" Text="Search" OnClick="btsearch_Click" />
                    </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" OnUnload="UpdatePanel2_Unload">
                        <ContentTemplate>
                             <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal" CssClass="aspgrid" OnSelectedIndexChanged="grd_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" style="font-size: small">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Item Code">
                                         <ItemTemplate>
                                             <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Item Name">
                                         <ItemTemplate>
                                             <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Item Arabic">
                                         <ItemTemplate>
                                             <asp:Label ID="lbarabic" runat="server" Text='<%# Eval("item_arabic") %>'></asp:Label></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Size">
                                         <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:CommandField ShowSelectButton="True"/>
                                     
                                 </Columns>
                    </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" class="auto-style2">
                    <asp:Button ID="btsubmit" runat="server" OnClick="btsubmit_Click" OnClientClick="javascript:this.close();" Text="SUBMIT" />
                   
                    <input id="Button1" type="button" value="button" onclick="closewin()" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
