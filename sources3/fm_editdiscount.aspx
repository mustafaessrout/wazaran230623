<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_editdiscount.aspx.cs" Inherits="fm_editdiscount" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
 
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
            font-size: small;
            color: #0033CC;
            text-decoration: underline;
        }
        .auto-style2 {
            font-weight: bold;
            font-size: small;
            color: #000000;
        }
    </style>
    <style>
        .xx {
            padding-left:1px;
            padding-right:1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="divheader">Edit Discount</div>
    <img src="div2.png" class="divid" />
    <div style="border:2px solid red">
    <table style="width:100%"><tr>
        <td>
            Disc No
        </td>
        <td>
            :</td><td>
            <asp:Label ID="lbdisc" runat="server" Text="Label" CssClass="auto-style1"></asp:Label></td>
        <td>Proposal No</td>
        <td>:</td>
        <td>
            <asp:Label ID="lbprop" runat="server" Text="Label" CssClass="auto-style1"></asp:Label></td>
        <td>Remark</td>
        <td>:</td>
        <td>
            <asp:Label ID="lbremark" runat="server" Text="Label" CssClass="auto-style2"></asp:Label></td>
                              </tr><tr>
        <td>
            Start Date</td>
        <td>
            :</td><td>
                <asp:Label ID="dtstart" runat="server" CssClass="auto-style2" Text="Label"></asp:Label>
            </td>
        <td>Delivery Date</td>
        <td>:</td>
        <td>
            <asp:Label ID="lbdelidate" runat="server" CssClass="auto-style2" Text="Label"></asp:Label>
            </td>
        <td>End Date</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="dtend" runat="server"></asp:TextBox>
            <cc1:calendarextender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
            </cc1:calendarextender>
            </td>
                              </tr><tr>
        <td>
            Disc Mechanism</td>
        <td>
            :</td><td>
                <asp:Label ID="lbdiscmec" runat="server" CssClass="auto-style2" Text="Label"></asp:Label>
            </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
                              </tr></table>
        </div>
    <div class="divheader">
        Editable Information
    </div>
    <div class="container-fluid">
    <div class="row">
        
            <div class="col-lg-4">
            <label>Qty Maximum</label>
            <asp:TextBox ID="txqtymax" runat="server" cssclass="form-control" Height="2em"></asp:TextBox>
            </div>
            <div class="col-lg-4">
                    <label>End Date</label><asp:TextBox ID="dtenddisc" runat="server" Height="2em" CssClass="form-control"></asp:TextBox>
                    <cc1:calendarextender ID="dtenddisc_CalendarExtender" runat="server" TargetControlID="dtenddisc" Format="d/M/yyyy">
                    </cc1:calendarextender>
                    
           </div>
            <div class="col-lg-4">
              <label>Request By</label>
              <asp:TextBox ID="txrequest" runat="server" cssclass="form-control" Height="2em"></asp:TextBox>
           </div>
    </div>
     </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save Update" CssClass="button2 print" OnClick="btsave_Click" />
    </div>

     
</asp:Content>

