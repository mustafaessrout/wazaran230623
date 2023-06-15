<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_gl.aspx.cs" Inherits="fm_acc_gl" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">General Ledger</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print GL By Unit Of Work</div>
            </div>
            &nbsp
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                   <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                   </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtfrom_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtfrom_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtfrom">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">End Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtto" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtto_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtto_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtto">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>

        <div class="row navi">
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <asp:Button ID="btprintbyuow" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprintbyuow_Click" />
            </div>
        </div>

    <br/><div class="h-divider"></div>
        

        <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print GL All Unit Of Work</div>
            </div>
            <%--&nbsp--%>
            <label class="control-label col-md-1">Start Date</label>
            <div class="col-sm-10 drop-down-date">
                <asp:TextBox ID="dtfrom2" runat="server" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="dtfrom_CalendarExtender2" CssClass="date" runat="server" BehaviorID="dtfrom_CalendarExtender2" Format="d/M/yyyy" TargetControlID="dtfrom2">
                </ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
            <label class="control-label col-md-1">End Date</label>
            <div class="col-md-10 drop-down-date">
                <asp:TextBox ID="dtto2" runat="server" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="dtto_CalendarExtender2" CssClass="date" runat="server" BehaviorID="dtto_CalendarExtender2" Format="d/M/yyyy" TargetControlID="dtto2">
                </ajaxToolkit:CalendarExtender>
            </div>
        </div>

        <div class="row navi">
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <asp:Button ID="btprintalluow" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprintalluow_Click" />
            </div>
        </div>
    </div>
  
</asp:Content>

