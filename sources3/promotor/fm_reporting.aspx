<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_reporting.aspx.cs" Inherits="promotor_fm_reporting" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Reporting</h4>
        <div class="h-divider"></div>
        <h6 class="jajarangenjang">Daily Stock Summary (<%=Request.Cookies["exh_cd"].Value.ToString() %>)</h6>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dt" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dt_CalendarExtender" runat="server" TargetControlID="dt" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btprintss" CssClass="btn btn-primary" runat="server" OnClick="btprintss_Click">PRINT</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

