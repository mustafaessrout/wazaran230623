<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="vendor_entry.aspx.cs" Inherits="vendor_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
       ENTRY VENDOR
   </div>
    <img src="div2.png" class="divid" />

        <table style="vertical-align:top">
        <tr>
            <td>
               Vendor Code
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txcode" runat="server"></asp:TextBox>
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
                Name
            </td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txname" runat="server" Height="16px" Width="256px"></asp:TextBox>
            </td>
            <td>
                Short Name</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txchkname" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Contact</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txcontact" runat="server" Width="252px"></asp:TextBox>
            </td>
            <td>
                Address</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txaddr1" runat="server" TextMode="MultiLine" Width="256px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>
               Address 2</td>
            <td>:</td>
            <td colspan="4">
                <asp:TextBox ID="txaddr2" runat="server" TextMode="MultiLine" Width="75%"></asp:TextBox></td>
            <tr>
            <td>
               Address 3</td>
            <td>:</td>
            <td colspan="4">
                <asp:TextBox ID="txaddr3" runat="server" TextMode="MultiLine" Width="75%"></asp:TextBox></td>
            <tr>
            <td>
               City</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbcity" runat="server"></asp:DropDownList>
            </td>
            <td>
                Post Code</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txpostcode" runat="server"></asp:TextBox>
             </td>
        </tr>
         <tr>
            <td>
                Currency</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbcurr" runat="server"></asp:DropDownList>
            </td>
            <td>
                Group</td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="cbgroup" runat="server"></asp:DropDownList>
             </td>
        </tr>
         <tr>
            <td>
                Bank</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbbank" runat="server"></asp:DropDownList>
            </td>
            <td>
                Acc. No</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txacc" runat="server"></asp:TextBox>
             </td>
        </tr>
         <tr>
            <td>
                Phone 1</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txphone1" runat="server"></asp:TextBox>
            </td>
            <td>
                Phone 2</td>
            <td>
                :</td>
            <td>
                <asp:TextBox ID="txphone2" runat="server"></asp:TextBox>
             </td>
        </tr>
         <tr>
            <td>
                Fax</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txfax" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
         
    </table>
    <img src="div2.png" class="divid" />
    <div class="navi">
         <asp:Button ID="btSave" runat="server" OnClick="btSave_Click" Text="SAVE" CssClass="button2 save" />
    </div>
</asp:Content>

