<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmprnSalesActivityByCustomer.aspx.cs" Inherits="frmprnSalesActivityByCustomer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Sales Activity by Customer</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Period</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control  ">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control" Enabled="False" >
                    </asp:DropDownList>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <div class="col-md-offset-2 col-md-4 col-sm-5">
                    <asp:TextBox ID="txfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="txfrom_CalendarExtender" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                <p class="text-center col-sm-2 col-md-1" style="margin-top:8px;">To</p>
                <div class="col-sm-5">
                    <asp:TextBox ID="txto" runat="server" CssClass="form-control  "></asp:TextBox>
                    <asp:CalendarExtender ID="txto_CalendarExtender" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Salesman</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" CssClass="form-control  " >
                    </asp:DropDownList>
                     
                </div>
            </div>

        </div>
        <div class="row navi padding-bottom margin-bottom padding-top">
            <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
        </div>
    </div>
   
    
</asp:Content>

