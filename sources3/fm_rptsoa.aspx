<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptsoa.aspx.cs" Inherits="fm_rptsoa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsl.ClientID%>').click();
        }
    </script>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
    <style>
  
     </style>
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid padding-top margin-top padding-bottom margin-bottom">
        <div class="row col-md-offset-3 col-md-6">
            <div class="clearfix margin-bottom">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <label class="control-label col-sm-2">Type of Report </label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbtype_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Value="0">Statement of Account</asp:ListItem>
                        <asp:ListItem Value="1">Statement of Account 2</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <label class="control-label col-sm-2">Date Transaction</label>
                <div class="col-sm-10 ">
                    <div class="col-sm-5 drop-down-date no-padding">
                        <asp:TextBox ID="dtsoa" runat="server" OnTextChanged="dtsoa_TextChanged" AutoPostBack="True" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="dtsoa_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtsoa">
                        </asp:CalendarExtender>

                    </div>
                    <div class="col-sm-2 text-center">
                        <p class="text-bold" style="padding-top: 10px;">To</p>
                    </div>
                    <div class="col-sm-5 drop-down-date no-padding">
                        <asp:TextBox ID="dtsoato" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="dtsoato_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtsoato">
                        </asp:CalendarExtender>

                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <label class="control-label col-sm-2">Customer</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcust" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                            </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="divw" class="auto-text-content"></div>
                </div>
                <div class="col-sm-2">
                    <div class="checkbox-inline checkbox">
                        <asp:CheckBox ID="chall" runat="server" Text="All" OnCheckedChanged="chall_CheckedChanged" AutoPostBack="True" />
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <label class="control-label col-sm-2">Salesman</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>
                <div class="col-sm-12">
                    <asp:HiddenField ID="hdcust" runat="server" />
                </div>
            </div>
        </div>
        <div class="row navi col-sm-offset-3 col-sm-6">
            <asp:Button ID="btprint" OnClientClick="ShowProgress();" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            <asp:Button ID="btsl" runat="server" Text="Refresh" CssClass="btn-primary btn btn-refresh" OnClick="btsl_Click" />
        </div>
        <div class="row">
            <div id="cbsalespointa" style="font-family: Calibri; font-size: small;"></div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>

