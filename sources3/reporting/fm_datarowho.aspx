<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_datarowho.aspx.cs" Inherits="fm_datarowho" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="css/anekabutton.css" rel="stylesheet" />
        <style type="text/css">
            .auto-style1 {
                width: 100%;
            }
            .auto-style2 {
                width: 1px;
            }
            .auto-style3 {
                width: 92px;
            }
            .auto-style4 {
                width: 92px;
                height: 20px;
            }
            .auto-style5 {
                width: 1px;
                height: 20px;
            }
            .auto-style6 {
                height: 20px;
            }
            .main-content #mCSB_2_container{
                min-height: 540px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="divheader">Sales Data Row Report</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="col-md-offset-2 col-md-8 col-sm-12 clearfix no-padding margin-bottom">
                    <div class="margin-bottom clearfix">
                        <label class="control-label col-md-2 col-sm-4 titik-dua">BRANCH</label>
                        <div class="col-md-10 col-sm-8 drop-down">
                            <asp:DropDownList ID="cbbranch" runat="server" CssClass="drop-down form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="margin-bottom clearfix">
                        <label class="control-label col-md-2 col-sm-4 titik-dua">TYPE</label>
                        <div class="col-md-10 col-sm-8 drop-down">
                            <asp:DropDownList ID="CBTYPE" runat="server" CssClass="drop-down form-control">
                                <asp:ListItem Value="0">SALES RAWDATA</asp:ListItem>
                                <asp:ListItem Value="1">CLAIM SALES RAW DATA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div >
                        <asp:Label ID="lbdate" runat="server" Text="From Date" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 drop-down-date">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="dtdata" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtdata_CalendarExtender" runat="server" TargetControlID="dtdata" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div >
                        <asp:Label ID="lbdate2" runat="server" Text="To Date" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 drop-down-date">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="dtdata1" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtdata1_CalendarExtender" runat="server" TargetControlID="dtdata1" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>
           
            <div class="navi margin-bottom">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>

        </div>

    </div>
    
</asp:Content>

