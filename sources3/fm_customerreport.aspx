<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customerreport.aspx.cs" Inherits="fm_customerreport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
    </script>
    <style type="text/css">
       .main-content #mCSB_2_container{
            min-height: 540px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Customer list by salesman Report</div>
    <div class="h-divider"></div>
    
            <div class="container-fluid">
                <div class="row clearfix">
                    <div class="col-md-offset-3 col-md-6 col-sm-offset-2 col-sm-8">
             
                        <div class="clearfix form-group">
                            <asp:Label ID="lbdate" runat="server" Text="Report" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-10">
                                <div class="drop-down">
                                    <asp:UpdatePanel ID="UpdatePanelAll" runat="server" >
                                    <ContentTemplate>
                                    <asp:DropDownList ID="cbcst" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcst_SelectedIndexChanged" CssClass="form-control">
                                        <asp:ListItem Value="1">Customer Balance</asp:ListItem>
                                        <asp:ListItem Value="2">Customer Document Detail</asp:ListItem>
                                        <asp:ListItem Value="0">Customer report</asp:ListItem>
                                        <asp:ListItem Value="3">Customer Document Summary</asp:ListItem>
                                        <asp:ListItem Value="4">Customer Not Buying</asp:ListItem>
                                    </asp:DropDownList>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                   
              
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <div class="clearfix form-group" runat="server" id="viewSalesman">
                            <asp:Label ID="lbdate2" runat="server" Text="Salesman" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-10">
                                <div >
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txsalesman" runat="server" CssClass="form-control"></asp:TextBox>      
                                            <div id="divwidths"></div>                                     
                                            <asp:HiddenField ID="hdsalesman_cd" runat="server" />               
                                            <ajaxToolkit:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListItemCssClass="auto-complate-item" CompletionListHighlightedItemCssClass="auto-complate-hover" ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True" 
                                                    CompletionListElementID="divwidths" MinimumPrefixLength="1" EnableCaching="false"  FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:CheckBox ID="chsalesman" runat="server" AutoPostBack="True" OnCheckedChanged="chsalesman_CheckedChanged" Text="ALL SALESMAN" CssClass="checkbox no-margin" />
                            </div>
                        </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbcst" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                        <div class="clearfix form-group" runat="server" id="viewPeriod">
                            <div >
                                <asp:Label ID="lbdate3" runat="server" Text="From Date" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                                <div class="col-md-4 col-sm-8 drop-down-date">
                                    <asp:TextBox ID="dtdata" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender CssClass="date" ID="dtdata_CalendarExtender" runat="server" BehaviorID="dtdata_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtdata">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div >
                                <asp:Label ID="lbdate4" runat="server" Text="To Date" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                                <div class="col-md-4 col-sm-8 drop-down-date">
                                    <asp:TextBox ID="dtdata1" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender CssClass="date" ID="dtdata1_CalendarExtender" runat="server" BehaviorID="dtdata1_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtdata1">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbcst" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                        <div class="clearfix form-group" runat="server" id="viewProduct">
                            <asp:Label ID="lbproduct" runat="server" Text="Product" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-5">
                                <div class="drop-down">         
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
                                    <ContentTemplate>
                                    <asp:DropDownList ID="cbproduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbproduct_SelectedIndexChanged" CssClass="form-control">      
                                    </asp:DropDownList>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="drop-down">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbitem" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbproduct" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbcst" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
               
                    </div>         
                </div>

                <div class="navi row margin-top margin-bottom">
                    <asp:Button ID="btprint" CssClass="btn-info btn btn-print" runat="server" Text="Print" OnClick="btprint_Click" />
                </div>
            </div>
</asp:Content>

