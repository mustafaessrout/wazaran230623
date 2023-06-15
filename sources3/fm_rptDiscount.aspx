<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptDiscount.aspx.cs" Inherits="fm_rptDiscount" %>

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
       
        <div class="text-center row margin-top padding-top margin-bottom padding-bottom">
            <label class="col-sm-4 control-label" style="text-align:right;">1% Discount report draft</label>
            <asp:Button ID="btprint" runat="server" Text="Print" OnClick="btprint_Click" CssClass="btn btn-info btn-print" />
        </div>
    </div>
    
</asp:Content>

