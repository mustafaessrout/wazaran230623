<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_balance.aspx.cs" Inherits="fm_balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link rel="Stylesheet" href="css/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
    <script src="css/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="divheader">
        <h3>Branch Balance Report</h3>
    </div>
    <div class="h-divider"></div>
    <%--<div class="h-divider"></div> --%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-6 ">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Branch</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="form-control  " OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Salesman</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control  " Enabled="False">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
           <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Balance Limit</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txbalance" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <div class="navi">
                    <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

