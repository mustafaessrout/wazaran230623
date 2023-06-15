<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmprnSalestargetVsActualchart.aspx.cs" Inherits="frmprnSalestargetVsActualchart" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Sales Target VS Actual Chart</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">Period</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control  " >
                    </asp:DropDownList>
                     
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control  " Enabled="False" >
                    </asp:DropDownList>
                     
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                
                <div class="col-md-offset-2 col-md-4 col-sm-5">
                    <asp:TextBox ID="txfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="txfrom_CalendarExtender" CssClass="date" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                <p class="text-center col-md-1 col-sm-2" style="margin-top:8px;">to</p>
                <div class="col-sm-5">
                    <asp:TextBox ID="txto" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="txto_CalendarExtender" CssClass="date" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">Salesman</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True"  CssClass="form-control  ">
                    </asp:DropDownList>
                     
                </div>
            </div>
        </div>
        
        <div class="row navi padding-bottom padding-top">
            <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
        </div>
    </div>
    
    
</asp:Content>

