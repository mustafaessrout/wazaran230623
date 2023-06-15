<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_financialReports.aspx.cs" Inherits="fm_acc_financialReports" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Financial Reports</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <%--<div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Financial Reports</div>--%>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint2" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                    </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Report</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="ddReport" runat="server" CssClass="form-control">
                    </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Layout</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="ddLayout" runat="server" CssClass="form-control">
                    </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Post Period (yyyyMM)</label>
                <div class="col-sm-10 require">
                    <asp:TextBox ID="txPeriod" runat="server" CssClass="form-control">
                    </asp:TextBox>  
                </div>
            </div>
            <br/><br/>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprinttrialbalanceyr" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprintfinreport_Click" />
                </div>
            </div>
       </div>
    </div>
  
</asp:Content>

