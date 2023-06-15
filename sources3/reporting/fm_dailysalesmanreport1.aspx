<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_dailysalesmanreport1.aspx.cs" Inherits="fm_dailysalesmanreport1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" href="css/anekabutton.css" />
    <link rel="Stylesheet" href="css/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
    <script src="css/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">

    <div class="divheader">DAILY SALESMAN REPORT</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="clearfix margin-bottom form-group">
                <div class="col-md-2 titik-dua control-label">
                    Branch
                </div>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-2 titik-dua control-label">
                    Transaction Type
                </div>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbreport" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbreport_SelectedIndexChanged" AutoPostBack="True">
                        
                        <asp:ListItem Value="1">DAILY SALESMAN REPORT (OPERATIONAL)</asp:ListItem>                        
                        <asp:ListItem Value="2">SALESMAN RPS</asp:ListItem>
                        <asp:ListItem Value="3">SALES FORCE PRODUCTIVITY EVALUATION</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix margin-bottom form-group">
                <div class="col-md-2 titik-dua control-label">
                    SALESMAN
                </div>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                           <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="clearfix margin-bottom form-group">
            <div class="col-md-2 titik-dua control-label">
                Transaction Date
            </div>
           <%-- <div class="col-md-4">--%>
                <div class="col-md-4 col-sm-8 drop-down-date">
                <asp:TextBox ID="dtrps" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="dtrps_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtrps">
                </asp:CalendarExtender>
            </div>
            <div class="col-md-4 col-sm-8 drop-down-date">
                <asp:TextBox ID="dtrps2" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="dtrps2_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtrps2">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="clearfix margin-top margin-bottom form-group">
            <div class="navi">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btprint_Click" />
                <asp:Button ID="btsendemail" runat="server" Text="Email" CssClass="btn btn-info" OnClick="btsendemail_Click" Visible="False" />
            </div>
        </div>
    </div>
</asp:Content>

