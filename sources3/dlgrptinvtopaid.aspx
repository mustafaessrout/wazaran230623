<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="dlgrptinvtopaid.aspx.cs" Inherits="dlgrptinvtopaid" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Invoice To Paid
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Salesman
            </td>
            <td>
                :
            </td>
            <td>
                <asp:DropDownList ID="cbsalesman" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Customer</td>
             <td>:<asp:HiddenField ID="hdcust" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txcustomer" runat="server" Width="300px"></asp:TextBox>
                <div id="divwidthc"></div>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionListElementID="divwidthc" OnClientItemSelected="CustSelected"> 
                </asp:AutoCompleteExtender>
            </td>
        </tr>
        <tr>
            <td>
                Period
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="dtstart" runat="server" CssClass="makeitreadonly"></asp:TextBox> 
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                </asp:CalendarExtender>
                To 
                <asp:TextBox ID="dtend" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
    </table>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
</asp:Content>

