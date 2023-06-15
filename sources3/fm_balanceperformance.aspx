<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_balanceperformance.aspx.cs" Inherits="fm_balanceperformance" MasterPageFile="~/site.master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="css/anekabutton.css" rel="stylesheet" />
  
    <script>
         function salesmanSelected(sender, e) {
                $get('<%=hdemp.ClientID%>').value = e.get_value();
        }
                function CustSelected(sender, e) {
                    $get('<%=hdcust.ClientID%>').value = e.get_value();
        }

    </script>
    <style>
        .main-content #mCSB_2_container{
            min-height: 540px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="divheader">Balance Perforrmance Repeort</div>
    <div class="h-divider"></div>
    <asp:UpdatePanel ID="UpdatePanelAll" runat="server" >
        <ContentTemplate>

            <div class="container-fluid">
                <div class="row">
                    <div class="clearfix">
                        <div class="col-md-offset-3 col-sm-offset-2 col-md-6 col-sm-8 form-group">
                            <asp:Label ID="lbreport" runat="server" CssClass="col-sm-2 control-label" Text="Report"></asp:Label>
                            <div class="col-sm-10 drop-down">
                                <asp:DropDownList ID="cbreport" runat="server" CssClass="form-control"  OnSelectedIndexChanged="cbreport_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="0">Balance Performance by Customer</asp:ListItem>
                                    <asp:ListItem Value="1">Balance Performance by Salesman</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-offset-3 col-sm-offset-2 col-md-6 col-sm-8 form-group">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                <ContentTemplate>
                                    <asp:Label ID="lbcust" runat="server" Text="Customer" Visible="False" CssClass="col-sm-2 control-label"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-10 ">
                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                        <asp:HiddenField ID="hdcust" runat="server" />
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList" UseContextKey="True" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthc">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="txsalesman" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>                        
                                        <asp:HiddenField ID="hdemp" runat="server" />
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList1" TargetControlID="txsalesman" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="salesmanSelected">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="h-divider"></div>
</asp:Content>


