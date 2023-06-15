<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupcustomer.aspx.cs" Inherits="fm_lookupcustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 483px;
        }
        .auto-style2 {
            text-decoration: underline;
        }
    </style>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body style="font-family:Verdana,Tahoma;font-size:small">
     <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="tsmanager" runat="server"></asp:ToolkitScriptManager>
    <div class="auto-style2">
        <strong>Customer Search</strong></div>
        <div>
            <table class="auto-style1">
                <tr>
                    <td>
                        Search Customer
                    </td>
                    <td>
                        <asp:TextBox ID="txsearch" runat="server" Height="16px" Width="289px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="button2 search" OnClick="btsearch_Click" style="left: 0px; top: 0px" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="background-color:#efebeb">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="grdsearch_SelectedIndexChanged" style="font-family:Verdana,Tahoma;font-size:small" AllowPaging="True" OnPageIndexChanging="grdsearch_PageIndexChanging" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lbcodecustomer" runat="server" Text='<%# Eval("custcd") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:Label ID="lbcustomername" runat="server" Text='<%# Eval("custnm") %>'></asp:Label>
                         </ItemTemplate>   
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Arabic">
                        <ItemTemplate>
                            <asp:Label ID="lbarabic" runat="server" Text='<%# Eval("custnmarabic") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label ID="lbaddr1" runat="server" Text='<%# Eval("addr1") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate><asp:Label runat="server" Text='<%# Eval("city") %>' ID="lbcity"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>

                </ContentTemplate>
                <Triggers><asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" /></Triggers>
            </asp:UpdatePanel>
           
        </div>
    </form>
</body>
</html>
