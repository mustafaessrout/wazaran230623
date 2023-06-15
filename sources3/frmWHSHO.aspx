<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmWHSHO.aspx.cs" Inherits="frmWHSHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
      function openwindow() {
          var oNewWindow = window.open("fm_lookup_WHSHO.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");

      }
      function updpnl() {
          document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  class="divheader">Warehouse HO</div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>whsHOCD</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txwhsHOCD" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 0px" />
                        <asp:TextBox ID="txKey" runat="server"  Width="40px" style="display:none"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
            </td>
            <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
        </tr>
        <tr>
            <td>Warehouse Name</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txwhsHOName" runat="server" Width="400px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Warehouse Group</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbwhsHOGrpCD" runat="server" Width="300px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <img src="div2.png" class="divid" />
    <div class="navi">

        <asp:Button ID="btnew" runat="server" Text="New" CssClass="button2 add" OnClick="btnew_Click"/>
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" />
                    
    </div>
</asp:Content>

