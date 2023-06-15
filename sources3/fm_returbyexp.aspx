<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_returbyexp.aspx.cs" Inherits="fm_returbyexp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="v4-alpha/docs.min.css" rel="stylesheet" />
    <script src="v4-alpha/bootstrap.min.js"></script>
    <script src="v4-alpha/docs.min.js"></script>
    <style>
        .main-content #mCSB_2_container {
            min-height: 540px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Return Report by expire date</div>
    <div class="h-divider"></div>

    <div class="container-fluid">

        <div class="row clearfix">
            <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">Branch</label>
                <div class="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbbranch" CssClass="form-control " runat="server" Enabled="false">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-4 col-form-label">Report</label>
                <div class="col-sm-8 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbreport" CssClass="form-control " runat="server">
                                <asp:ListItem Value="1">1 Month</asp:ListItem>
                                <asp:ListItem Value="2">2 Month</asp:ListItem>
                                <asp:ListItem Value="3">3 Month</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>



            <div class="row margin-top padding-top">
                <div class="navi margin-bottom margin-top">
                    <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-print" OnClick="btprint_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

