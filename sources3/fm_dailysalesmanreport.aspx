<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_dailysalesmanreport.aspx.cs" Inherits="fm_dailysalesmanreport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">DAILY SALESMAN REPORT</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="clearfix margin-bottom">
                <div class="col-md-2 titik-dua control-label">
                    Branch
                </div>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-2 titik-dua control-label">
                    Transaction Type
                </div>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbreport" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbreport_SelectedIndexChanged" AutoPostBack="True">
                        
                        <asp:ListItem Value="1">DAILY SALESMAN REPORT (OPERATIONAL)</asp:ListItem>
                        <asp:ListItem Value="2">SALESMAN RPS</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-2 titik-dua control-label">
                    SALESMAN
                </div>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txsalesman" Enabled="false" runat="server" Height="2em" Width="100%" CssClass="form-control"></asp:TextBox>
                            <div id="divwidth" style="font-size: small; font-family: Calibri; position: absolute;"></div>
                            <asp:HiddenField ID="hdsalesman_cd" runat="server" />
                            <asp:AutoCompleteExtender ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True"
                                CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                            </asp:AutoCompleteExtender>
                           
                        </ContentTemplate>
                    </asp:UpdatePanel> 
                    
                </div>
                <asp:CheckBox ID="chdisc" runat="server"  Text="select all"  />
            </div>
        </div>
        <div class="clearfix margin-bottom">
            <div class="col-md-2 titik-dua control-label">
                Transaction Date
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="dtrps" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender CssClass="date" ID="dtrps_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtrps">
                </asp:CalendarExtender>
            </div>
            <div class="col-md-1 titik-dua control-label ">
                To
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="dtrpsto" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender CssClass="date" ID="dtrps_CalendarExtenderto" runat="server" Format="d/M/yyyy" TargetControlID="dtrpsto">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="clearfix margin-top margin-bottom">
            <div class="navi">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btprint_Click" />
            </div>
        </div>
    </div>
</asp:Content>

