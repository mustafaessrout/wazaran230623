<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_advancedtimer.aspx.cs" Inherits="Adminbranch_fm_advancedtimer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Closing Timer Advanced</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Branch</label>
            <div class="col-md-3">
                <asp:Label ID="lbbranch" runat="server" CssClass="form-control"></asp:Label>
            </div>
             <label class="control-label col-md-1">Time to be advanced</label>
            <div class="col-md-2">
                <asp:TextBox ID="txdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>               
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:Button ID="btsave" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="btsave_Click" />
            </div>
        </div>
    </div>
</asp:Content>

