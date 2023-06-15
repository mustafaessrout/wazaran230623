<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_dailyinvrecpvouch.aspx.cs" Inherits="fm_dailyinvrecpvouch" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 482px;
        }
        .drop-down-date.drop-down-custom::before{
            right:10px;
        }
        .main-content #mCSB_2_container{
            min-height: 540px;
        }
        </style>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Daily Invoice, Receipt Voucher, &amp; Transfer Report by Outlets</div>
    <div class="h-divider"></div>

    <div class="container-fluid clearfix">
        <div class="row col-sm-8 col-sm-offset-2">
            <div class="clearfix col-sm-6">
                <asp:Label ID="lbldt1" runat="server" Text="Start Date" CssClass="control-label"></asp:Label>
                <div class="drop-down-date drop-down-custom">
                    <asp:TextBox ID="txdt1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="txdt1_CalendarExtender" runat="server" BehaviorID="txdt1_CalendarExtender" Format="d/M/yyyy" TargetControlID="txdt1">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="clearfix col-sm-6">
                 <asp:Label ID="lbldt2" runat="server" Text="End Date" CssClass="control-label"></asp:Label>
                <div class="drop-down-date drop-down-custom">
                    <asp:TextBox ID="txdt2" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="txdt2_CalendarExtender" runat="server" BehaviorID="txdt2_CalendarExtender" TargetControlID="txdt2" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="navi row col-sm-8 col-sm-offset-2 margin-top margin-bottom ">
            <div class="col-sm-12">
                <asp:Button ID="btreport" runat="server" Text="Print" OnClick="btreport_Click" CssClass="btn-info btn btn-print" />
            </div>
     </div>
    </div>
  
     
    
</asp:Content>


