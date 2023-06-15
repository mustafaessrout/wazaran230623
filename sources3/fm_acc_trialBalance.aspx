<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_trialBalance.aspx.cs" Inherits="fm_acc_trialBalance" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Trial Balance Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Monthly Trial Balance</div>
            </div>
     
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint1" runat="server" CssClass="form-control">
                    </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">                            
                <label class="control-label col-md-1">Is Worksheet</label>
                <div class="col-lg-10">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <%--<asp:RadioButtonList ID="rdIsWorksheet" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbtworksheet_SelectedIndexChanged">--%>
                            <asp:RadioButtonList ID="rdIsWorksheet" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                                <asp:ListItem Value="T">Trial Balance</asp:ListItem>
                                <asp:ListItem Value="W">Worksheet</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtfrom0" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtfrom0_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtfrom0_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtfrom0">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">End Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtto0" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtto0_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtto0_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtto0">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <br/><br/>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprinttrialbalance" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprinttrialbalance_Click" />
                </div>
            </div>

        </div>
    </div>

    <br/><br/>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Yearly Trial Balance</div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint2" runat="server" CssClass="form-control">
                    </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">                            
                <label class="control-label col-md-1">Is Worksheet</label>
                <div class="col-lg-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%--<asp:RadioButtonList ID="rdIsWorksheet2" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbtworksheet_SelectedIndexChanged">--%>
                            <asp:RadioButtonList ID="rdIsWorksheet2" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                                <asp:ListItem Value="T">Trial Balance</asp:ListItem>
                                <asp:ListItem Value="W">Worksheet</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Year</label>
                <div class="col-sm-10 require">
                    <asp:TextBox ID="txYear" runat="server" CssClass="form-control">
                    </asp:TextBox>  
                </div>
            </div>
            <br/><br/>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprinttrialbalanceyr" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprinttrialbalanceyr_Click" />
                </div>
            </div>
       </div>
    </div>
  
</asp:Content>

