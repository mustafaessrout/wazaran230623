<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_listcustomerbyitemovedue.aspx.cs" Inherits="fm_listcustomerbyitemovedue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 110px;
        }

        .auto-style3 {
            width: 523px;
        }

        .auto-style4 {
            width: 83px;
        }
    </style>
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        List Customer With Item overdue
    </div>
    <img src="div2.png" class="divid" />


    <div>

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Salespoint:</td>
                <td class="auto-style3">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cbsalespoint" runat="server" Width="300px">
                    </asp:DropDownList>
                            </ContentTemplate></asp:UpdatePanel>
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Salesman:</td>
                <td class="auto-style3">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cbsalesman" runat="server" Width="300px">
                    </asp:DropDownList>
                            <asp:CheckBox ID="chsls" runat="server" Text="ALL" AutoPostBack="True" OnCheckedChanged="chsls_CheckedChanged" />
                            </ContentTemplate></asp:UpdatePanel>
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Customer</td>
                <td class="auto-style3">
                      <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txcustomer" runat="server" Width="400px"></asp:TextBox>
                         <asp:CheckBox ID="chcust" runat="server" Text="ALL" AutoPostBack="True" OnCheckedChanged="chcust_CheckedChanged" />
                          <asp:HiddenField ID="hdcust" runat="server" />
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" TargetControlID="txcustomer" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList" UseContextKey="True" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthc">
                </asp:AutoCompleteExtender>
                
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">From product:</td>
                <td class="auto-style3">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cbProd_cdFr" runat="server" Width="300px">
                    </asp:DropDownList>
                            </ContentTemplate></asp:UpdatePanel>
                </td>
                <td class="auto-style4">To Product:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cbProd_cdTo" runat="server" Width="300px">
                    </asp:DropDownList>
                            </ContentTemplate></asp:UpdatePanel>
                </td>
            </tr>
        </table>


    </div>

    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
</asp:Content>

