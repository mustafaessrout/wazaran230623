<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_stockcard.aspx.cs" Inherits="fm_acc_stockcard" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Inventory Stock Ledger</div>
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
         <%--   <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
            </div>--%>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Warehouse Bin</label>
                <div class="col-sm-10 require">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlWhBin" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWhBin_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Item Code</label>
  <%--              <div class="col-sm-10 require">
                    <asp:TextBox ID="txItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                </div>--%>
                <div class="col-sm-10 require">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlItemCode" CssClass="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Monthly or Yearly</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rdmoy" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                                <asp:ListItem Value="M">Monthly</asp:ListItem>
                                <asp:ListItem Value="Y">Yearly</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">As Of Date</label>
                <div class="col-sm-10 drop-down-date require">
                    <asp:TextBox ID="asofdate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="asofdate_CalendarExtender" CssClass="date" runat="server" BehaviorID="asofdate_CalendarExtender" Format="d/M/yyyy" TargetControlID="asofdate">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>

        <div class="row navi">
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>
        </div>
<%--        <div class="row navi">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="textResult" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>
    </div>
  
</asp:Content>

