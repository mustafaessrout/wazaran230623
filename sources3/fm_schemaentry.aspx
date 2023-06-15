<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_schemaentry.aspx.cs" Inherits="fm_schemaentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-weight: normal;
        }
    .auto-style2 {
        height: 23px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Discount Schema</h3>
    <table style="width:100%"> 
       <tr>
           <td>T<span class="auto-style1">ype</span></td>
           <td>:</td>
           <td>
               <asp:DropDownList ID="cbdisctype" runat="server" Height="16px" Width="127px">
               </asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td>Proposal</td>
           <td>:</td>
           <td>
               <asp:TextBox ID="txref" runat="server"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td>Description</td>
           <td>:</td>
           <td>
               <asp:TextBox ID="txref0" runat="server"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td>Start Date</td>
           <td>:</td>
           <td>
               <asp:TextBox ID="txref1" runat="server"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td>End Date</td>
           <td>:</td>
           <td>
               <asp:TextBox ID="txref2" runat="server"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td class="auto-style2">Program Type</td>
           <td class="auto-style2">:</td>
           <td class="auto-style2">
               <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Width="127px">
               </asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td class="auto-style2">Payment Type</td>
           <td class="auto-style2">:</td>
           <td class="auto-style2">
               <asp:DropDownList ID="DropDownList3" runat="server" Height="16px" Width="127px">
               </asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td class="auto-style2">&nbsp;</td>
           <td class="auto-style2">&nbsp;</td>
           <td class="auto-style2">&nbsp;</td>
       </tr>
       <tr>
           <td class="auto-style2">&nbsp;</td>
           <td class="auto-style2">&nbsp;</td>
           <td class="auto-style2">&nbsp;</td>
       </tr>
       </table>
    
    
</asp:Content>

