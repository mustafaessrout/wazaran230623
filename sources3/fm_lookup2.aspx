<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookup2.aspx.cs" Inherits="fm_lookup2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
    <script>
        function closewin()
        {
            window.opener.updpnl();
            window.close();
            //return (false);
        }
        function TakeRefresh()
        {
            window.opener.document.location.reload(true);
        }
    </script>
    <style type="text/css">
        .myButton {
	-moz-box-shadow:inset 0px 1px 0px 0px #bbdaf7;
	-webkit-box-shadow:inset 0px 1px 0px 0px #bbdaf7;
	box-shadow:inset 0px 1px 0px 0px #bbdaf7;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #79bbff), color-stop(1, #378de5));
	background:-moz-linear-gradient(top, #79bbff 5%, #378de5 100%);
	background:-webkit-linear-gradient(top, #79bbff 5%, #378de5 100%);
	background:-o-linear-gradient(top, #79bbff 5%, #378de5 100%);
	background:-ms-linear-gradient(top, #79bbff 5%, #378de5 100%);
	background:linear-gradient(to bottom, #79bbff 5%, #378de5 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#79bbff', endColorstr='#378de5',GradientType=0);
	background-color:#79bbff;
	-moz-border-radius:6px;
	-webkit-border-radius:6px;
	border-radius:6px;
	border:1px solid #84bbf3;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:arial;
	font-size:15px;
	font-weight:bold;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #528ecc;
}
.myButton:hover {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #378de5), color-stop(1, #79bbff));
	background:-moz-linear-gradient(top, #378de5 5%, #79bbff 100%);
	background:-webkit-linear-gradient(top, #378de5 5%, #79bbff 100%);
	background:-o-linear-gradient(top, #378de5 5%, #79bbff 100%);
	background:-ms-linear-gradient(top, #378de5 5%, #79bbff 100%);
	background:linear-gradient(to bottom, #378de5 5%, #79bbff 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#378de5', endColorstr='#79bbff',GradientType=0);
	background-color:#378de5;
}
.myButton:active {
	position:relative;
	top:1px;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="tsmanager" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:#dedcdc;font-family:Verdana,Tahoma;font-size:small">
        <h3>Discount Schema</h3>
        <table>
            <tr>
                <td class="auto-style1">
                    Search (Item Code/Name)</td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="txsearch" runat="server"></asp:TextBox>
&nbsp;</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    
                    </asp:UpdatePanel>
                    
                </td>
                <td>
                    <asp:Button ID="btsearch" runat="server" Text="Search" OnClick="btsearch_Click" class="myButton" />
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                             <asp:GridView style="font-size:small" ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                                 <Columns>
                                    
                                     <asp:TemplateField HeaderText="Select">
                                         <ItemTemplate>
                                             <asp:CheckBox ID="chselect" runat="server" /></ItemTemplate>
                                     </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Item Code">
                                         <ItemTemplate>
                                             <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>' ></asp:Label>
                                         </ItemTemplate>
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
                                         <ItemTemplate>
                                             <asp:Label ID="lbsize" runat="server" Text='<%# Eval("size") %>'></asp:Label></ItemTemplate>
                                     </asp:TemplateField>
                                    
                                 </Columns>
                    </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </td>
            </tr>
            <tr>
                <td colspan="5" class="auto-style1">
                    <asp:Button ID="btadd" runat="server" Height="26px" Text="ADD" Width="68px" OnClick="btadd_Click" CssClass="myButton" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <hr style="color:blue" /></td>
            </tr>
            <tr>
                <td colspan="5">
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                 <ContentTemplate>
                                     <asp:GridView style="font-size:small" ID="grddtl" runat="server" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal" Width="100%">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Del">
                                                 <ItemTemplate>
                                                     <asp:CheckBox ID="chdel" runat="server" /></ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Item Code">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Item Name">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Item Arabic">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbarabic" runat="server" Text='<%# Eval("item_arabic") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Size">
                                                 <ItemTemplate>
                                                   
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                     </asp:GridView>
                                 </ContentTemplate>
                                 <Triggers><asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" /></Triggers>
                             </asp:UpdatePanel>
                    
                        </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Button ID="btdel" runat="server" OnClick="btdel_Click" Text="Delete" CssClass="myButton" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" class="auto-style1">
                    <asp:Button ID="btsubmit" runat="server" OnClientClick="javascript:closewin()" Text="SUBMIT" CssClass="myButton" OnClick="btsubmit_Click1" />
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
