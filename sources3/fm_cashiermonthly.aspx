<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashiermonthly.aspx.cs" Inherits="fm_cashiermonthly" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Cashier Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                   </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Start Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtfrom_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtfrom_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtfrom">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">End Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtto" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtto_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtto_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtto">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>

        <div class="row navi">
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>
        </div>
    </div>
  
</asp:Content>

