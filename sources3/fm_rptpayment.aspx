<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptpayment.aspx.cs" Inherits="fm_rptpayment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
       .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row margin-top padding-top">
            <div class="col-sm-offset-3 col-sm-6 ">
                <label class="col-sm-4 control-label" style="text-align:right;">Payment Date </label>
                <div class="col-sm-8 drop-down-date">
                    <asp:TextBox ID="dtpayment" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="dtpayment_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtpayment">
                    </asp:CalendarExtender>
                     
                </div>
                <div class="col-sm-offset-4 col-sm-8">
                    <div class="checkbox no-margin">
                        <asp:CheckBox ID="cbpaydet" runat="server" Text="Detail" Style="padding-left:0; padding-top:5px;" />
                    </div>
                </div>
            </div>
        </div>
        <div class="text-center row margin-top padding-top margin-bottom padding-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print" OnClick="btprint_Click" CssClass="btn btn-info btn-print" />
        </div>
    </div>
    
</asp:Content>

