<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_balanceconfirm.aspx.cs" Inherits="fm_balanceconfirm" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btnrefresh.ClientID%>').click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Balance Confirmation
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
         <td>Date Of Confirmation</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtconfirm" runat="server"></asp:TextBox></td>
            <td>Printed By</td>
            <td>:</td>
            <td style="margin-left: 40px">
                <asp:Label ID="lbprintedby" runat="server" Text='<%# Request.Cookies["usr_id"].Value.ToString() %>'></asp:Label></td>
        </tr>
        <tr>
         <td class="auto-style1">Customer</td>
            <td class="auto-style1">:</td>
            <td class="auto-style1">
                <asp:TextBox ID="txcust" runat="server" Width="20em"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" EnableCaching="false" MinimumPrefixLength="1" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" CompletionListElementID="divwidth" OnClientItemSelected="CustSelected">
                </asp:AutoCompleteExtender>
            </td>
            <td class="auto-style1">
                <asp:Button ID="btnrefresh" runat="server" Text="Button" OnClick="btnrefresh_Click" />
            </td>
            <td class="auto-style1"></td>
            <td style="margin-left: 40px" class="auto-style1">
                <asp:HiddenField ID="hdcust" runat="server" />
                </td>
        </tr>
        <tr>
         <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="4" rowspan="2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

            <table>
                <tr><td>Address</td><td>:</td><td>
                    <asp:Label ID="lbaddress" runat="server" Text=""></asp:Label></td>
                    <td>City</td><td>:</td><td>
                        <asp:Label ID="lbcity" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Salesman</td><td>:</td><td>
                        <asp:Label ID="lbsalesmancode" runat="server" Text=""></asp:Label></td>
                    <td>Merchandiser</td><td>:</td><td>
                        <asp:Label ID="lbmerchan" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
                        
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
         <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
         <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="margin-left: 40px">
                &nbsp;</td>
        </tr>
        <tr>
         <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="margin-left: 40px">
                &nbsp;</td>
        </tr>
    </table>
    <div id="divwidth">

    </div>
</asp:Content>

