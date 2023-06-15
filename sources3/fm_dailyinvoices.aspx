<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_dailyinvoices.aspx.cs" Inherits="fm_dailyinvoices" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
    </script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 172px;
        }
        .auto-style5 {
            width: 172px;
            height: 31px;
        }
        .auto-style6 {
            height: 31px;
        }
        .auto-style7 {
            width: 172px;
            height: 34px;
        }
        .auto-style8 {
            height: 34px;
        }
        .auto-style9 {
            width: 172px;
            height: 32px;
        }
        .auto-style10 {
            height: 32px;
        }
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="margin-top padding-top container-fluid">
        <div class="row">
            <div class="col-sm-offset-2 col-sm-8 clearfix">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Report</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbreport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbreport_SelectedIndexChanged" CssClass="form-control clearfix">
                            <asp:ListItem Value="0">Daily Invoices</asp:ListItem>
                            <asp:ListItem Value="1">Invoices not received</asp:ListItem>
                            <asp:ListItem Value="2">Sales Book</asp:ListItem>
                            <asp:ListItem Value="3">Internal Stoock Transfer</asp:ListItem>
                            <asp:ListItem Value="4">Invoice not received 7days</asp:ListItem>
							<asp:ListItem Value="5">Driver not received 3 days</asp:ListItem>
                        </asp:DropDownList>
                        <div class="checkbox">
                            <asp:CheckBox ID="ch5days" runat="server" Text="All" Visible="False" CssClass="" />
                        </div>
                       
                    </div>
                </div>
                
                    <asp:Panel runat="server" ID="lbDatePnl"  CssClass="clearfix form-group no-padding col-sm-6">
                        <asp:Label ID="lbdate" runat="server" Text="Date" CssClass="control-label col-sm-4"></asp:Label>
                        <div class="col-sm-8  drop-down-date">
                            <asp:TextBox ID="dt" runat="server" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender CssClass="date" ID="dt_CalendarExtender" runat="server" BehaviorID="dt_CalendarExtender" TargetControlID="dt" Format="d/M/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </asp:Panel>
                  
                <asp:Panel runat="server" ID="lbdt0Pnl" CssClass="clearfix form-group no-padding col-sm-6">
                    <asp:Label ID="lbdt0" runat="server" Text="End Date" Visible="False" CssClass="control-label col-sm-4"></asp:Label>
                    <div class="col-sm-8 drop-down-date">
                         <asp:TextBox ID="dt0" runat="server" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="dt0_CalendarExtender" CssClass="date" runat="server" BehaviorID="dt0_CalendarExtender" Format="d/M/yyyy" TargetControlID="dt0">
                        </ajaxToolkit:CalendarExtender>
                    </div>
                </asp:Panel>
                <div class="clearfix form-group col-sm-12 no-padding">
                    <asp:Label ID="lblrepby" runat="server" Text="Report by" Visible="False" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10 ">
                         <asp:DropDownList ID="cbrepby" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbrepby_SelectedIndexChanged" Visible="False" CssClass="form-control">
                            <asp:ListItem Value="0">Branch</asp:ListItem>
                            <asp:ListItem Value="1">Salesman</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="clearfix form-group no-padding col-sm-6">
                    <asp:Label ID="lblsls" runat="server" Text="Salesman" Visible="False"  CssClass="control-label col-sm-4"></asp:Label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txsalesman" runat="server" CssClass="ro form-control" Visible="False"></asp:TextBox>                                                
                                <div id="divwidth" style="position:absolute;"></div>                                     
                                <asp:HiddenField ID="hdsalesman_cd" runat="server" />               
                                <ajaxToolkit:AutoCompleteExtender ID="txsalesman_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True" 
                                        CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false"  FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                </ajaxToolkit:AutoCompleteExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

           
            <div class="row navi padding-bottom col-xs-12">

                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>
            </div>
    </div>
</asp:Content>

