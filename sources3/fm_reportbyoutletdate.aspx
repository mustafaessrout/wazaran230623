<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reportbyoutletdate.aspx.cs" Inherits="fm_reportbyoutletdate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
        width: 618px;
    }
        .auto-style2 {
            height: 29px;
        }
    .auto-style3 {
        height: 29px;
        width: 618px;
    }
    </style>
    <script>
        function SalesmanSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Customer Due Report </div>
    <div class="h-divider"></div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row clearfix">
                    <div class="col-md-offset-2 col-sm-offset-1 col-md-8 col-sm-10">
                        <div class="clearfix form-group">
                            <label class="col-sm-4">Report To Be Generated</label>
                            <div class="col-sm-8 drop-down">
                                <asp:DropDownList ID="cbreport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbreport_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Invoice report by customer and date</asp:ListItem>
                                    <asp:ListItem Value="1">Invoice report by near date</asp:ListItem>
                                            <asp:ListItem Value="2">Invoice report by customer Group and date</asp:ListItem>
                                            <asp:ListItem Value="3">Invoice report by customer group and near date</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-offset-4 col-sm-8 checkbox no-margin-top no-margin-bottom">
                                <asp:CheckBox ID="chcustomeralone" runat="server" Text="Each Customer in one Page" OnCheckedChanged="chcustomernm1_CheckedChanged" AutoPostBack="True"  CssClass="text-red text-bold" />
                            </div>
                        </div>

                        <div class="clearfix form-group">
                             <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="col-sm-4">
                                 <ContentTemplate>
                                    <asp:Label ID="lblpayment" runat="server" Text="Payment term" CssClass="control-label"></asp:Label>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                            <div class="col-sm-8 drop-down">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbpaymentterm" runat="server" CssClass="form-control"></asp:DropDownList>
                                
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-offset-4 col-sm-8 checkbox no-margin-top no-margin-bottom">
                                    <asp:CheckBox ID="chpayment" runat="server" Text="All" OnCheckedChanged="chpayment_CheckedChanged" AutoPostBack="True" />
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="col-sm-4">
                                <ContentTemplate>
                                    <asp:Label ID="lblcust" runat="server" Text="Salesman Name" CssClass="control-label"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-8">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hdemp" runat="server" />
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                
                                        <div id="divwidths"></div>
                                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="SalesmanSelected">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-offset-4 col-sm-8 checkbox no-margin-top no-margin-bottom">
                                <asp:CheckBox ID="chcustomernm" runat="server" Text="All" OnCheckedChanged="chcustomernm_CheckedChanged" AutoPostBack="True" />
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <asp:Label ID="lbcusgrcd" runat="server" Text="Customer Group" Visible="False" CssClass="col-sm-4 text-bold"></asp:Label>
                            <asp:UpdatePanel ID="panelcbcusgrcd" runat="server" class="col-sm-8 drop-down" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="clearfix form-group">
                            <asp:Label ID="lblday" runat="server" Text="Near Day" Visible="False" CssClass="col-sm-4 text-bold"></asp:Label>
                            <asp:UpdatePanel ID="panelcbday" runat="server" class="col-sm-8 drop-down" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbday" runat="server"  Visible="False" CssClass="form-control">
                                        <asp:ListItem Value="3">3 Days</asp:ListItem>
                                        <asp:ListItem Value="6">6Days</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>

            
                </div>
                <div class="navi row margin-top margin-bottom">
                     <asp:Button ID="btreport" runat="server" Text="Print" OnClick="btreport_Click" CssClass="btn-info btn btn-print" />
                 </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

