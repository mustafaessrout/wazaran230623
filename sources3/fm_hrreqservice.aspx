<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrreqservice.aspx.cs" Inherits="fm_hrreqservice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">HR Service Request</div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <table><tr><td>Request No.</td><td>:</td><td>
            <asp:TextBox ID="txreqno" runat="server"></asp:TextBox></td><td>Date</td><td>:</td><td>
                <asp:TextBox ID="dtrequest" runat="server"></asp:TextBox></td></tr><tr><td class="auto-style1">Requestor</td><td class="auto-style1">:</td><td class="auto-style1">
                <asp:TextBox ID="txrequestor" runat="server"></asp:TextBox>
                </td><td class="auto-style1"></td><td class="auto-style1"></td><td class="auto-style1">
                </td></tr><tr><td class="auto-style2"></td><td class="auto-style2"></td><td class="auto-style2">
                </td><td class="auto-style2"></td><td class="auto-style2"></td><td class="auto-style2">
                </td></tr><tr style="background-color:gray;font-family:Calibri,Tahoma;font-weight:bold"><td>HR Service</td><td>&nbsp;</td><td>
                Remark</td><td>&nbsp;</td><td>&nbsp;</td><td>
                &nbsp;</td></tr><tr><td>
                <asp:DropDownList ID="cbhrservice" runat="server" Width="15em">
                </asp:DropDownList>
                </td><td>&nbsp;</td><td>
                <asp:TextBox ID="txremark" runat="server"></asp:TextBox>
                </td><td>
                    <asp:Button ID="btadd" runat="server" Text="Add Service" CssClass="button2 add" />
                </td><td>&nbsp;</td><td>
                &nbsp;</td></tr></table>
    </div>
</asp:Content>

