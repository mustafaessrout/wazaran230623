<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customerTransferApp.aspx.cs" Inherits="fm_customerTransferApp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdemployee" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Customer Transfer Approval Report</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-md-2">
                     <asp:TextBox ID="dtstart" runat="server" CssClass="form-control" onkeydown="return false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender3" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                                </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1">End Date</label>
                <div class="col-md-2">
                      <asp:TextBox ID="dtend" runat="server" CssClass="form-control" onkeydown="return false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                                </asp:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="center">
                 <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-primary" OnClick="btprint_Click">Print</asp:LinkButton>
        </div>
    </div>
</asp:Content>

