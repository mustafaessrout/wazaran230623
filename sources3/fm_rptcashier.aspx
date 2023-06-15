<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptcashier.aspx.cs" Inherits="fm_rptcashier" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .main-content #mCSB_2_container{
            min-height: 520px;
        }
     </style>
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row  col-sm-offset-3 col-sm-6 margin-top padding-top">
            <label class="col-sm-4 control-label">Payment Date </label>
            <div class="col-sm-8 drop-down-date">
                <asp:TextBox ID="dtcashier" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="dtcashier_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtcashier">
                </asp:CalendarExtender>
            </div>
        </div>

        <div class="row text-center padding-top  col-sm-offset-3 col-sm-6 margin-top  margin-bottom ">
            <asp:Button ID="btprint" runat="server" Text="Print" OnClick="btprint_Click" CssClass="btn btn-info btn-print" />
        </div>
    </div>

</asp:Content>

