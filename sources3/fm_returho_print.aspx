<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" CodeFile="fm_returho_print.aspx.cs" Inherits="fm_returho_print" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdretho.ClientID%>').value = e.get_value();
        }

    </script>
    <style type="text/css">
        .caret-off > i {
            display: none;
        }

        .dropdown-active + i {
            display: block;
        }

        .container-fluid {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid no-padding">
        <div class=" row col-md-12 clear-float">
            <div class="col-md-6 no-padding clearfix">
                <div class="divheader">RETUR HO Report</div>
            </div>

        </div>
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid top-devider">
        <div class="row col-md-8 col-md-offset-2">
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbtypofrep" runat="server" Text="Report Type" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-8 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbtypofrep" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbtypofrep_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0">RETUR HO</asp:ListItem>
                                <asp:ListItem Value="1">RETUR HO RAW DATA</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>


            </div>
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbstartdt" runat="server" Text="Start Date" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-10 drop-down-date">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                            </ajaxToolkit:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbenddate" runat="server" Text="End Date" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-10 drop-down-date">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="dtend_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtend_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                            </ajaxToolkit:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom">
                        <asp:Label ID="lbsalesman" runat="server" Text="RETUR HO NO" CssClass="control-label col-sm-2"></asp:Label>
                        <div class="col-sm-7">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txsalesmanPnl">
                                        <asp:TextBox ID="txreturho" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:Panel>
                                    <div id="divwidth" class="auto-text-content"></div>
                                    <asp:HiddenField ID="hdretho" runat="server" />
                                    <ajaxToolkit:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txreturho_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txreturho" UseContextKey="True"
                                        CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbtypofrep" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btprint" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>


        </div>


        <div class="navi row padding-top margin-bottom padding-bottom col-md-12">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>

</asp:Content>

