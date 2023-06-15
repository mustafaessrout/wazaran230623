<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_requestvehicle.aspx.cs" Inherits="fm_requestvehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 40px;
        }
        .auto-style2 {
            margin-left: 80px;
        }
        .auto-style3 {
            background-color: #6666FF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>Request Vehicle Information</strong>
    <table style="width:100%">
        <tr>
            <td>Branch / Salespoint</td>
            <td>:</td>
            <td class="auto-style2">
                <asp:DropDownList ID="cbsalespoint" runat="server"></asp:DropDownList>    
            </td>
            <td>Emp No.</td>
            <td>:</td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>    
            </td>
            <td>
                Phone / Mobile
            </td>
            <td>:</td>
            <td>

                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>    

            </td>
        </tr>
        <tr>
            <td>Division / Position</td>
            <td>:</td>
            <td class="auto-style2">
                &nbsp;</td>
            <td>Name</td>
            <td>:</td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>    
            </td>
            <td>
                Email Address</td>
            <td>:</td>
            <td>

                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>    

            </td>
        </tr>
        <tr>
            <td>Status</td>
            <td>:</td>
            <td class="auto-style2">
                <asp:DropDownList ID="cbstatus" runat="server">
                </asp:DropDownList>
            </td>
            <td>Reason</td>
            <td>:</td>
            <td class="auto-style2">
                <asp:DropDownList ID="cbreason" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>

                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="9" class="auto-style3"><strong>Request Vehicle Specification</strong></td>
        </tr>
        <tr>
            <td>Model/Type</td>
            <td>:</td>
            <td class="auto-style2">
                <asp:DropDownList ID="cbvhctype" runat="server">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>

                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

