<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_navisionfeed_cndn.aspx.cs" Inherits="fm_navisionfeed_cndn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
             

     </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="alert alert-info text-bold">Navision Feed Interface (CNDN)</div>
    <div class="container">
        
        <div class="row margin-bottom">
            <label class="control-label col-sm-1 input-sm">Start Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtstart" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender Format="d/M/yyyy" ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-sm-1 input-sm">End Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtend" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender Format="d/M/yyyy" ID="CalendarExtender1" runat="server" TargetControlID="dtend">
                </asp:CalendarExtender>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
            </div>
        </div>
       <%-- <div class="row margin-bottom alert alert-info">
            <div class="col-sm-12 overflow-y" style="max-height: 200px">
                <asp:GridView ID="grdsumm" CssClass="mGrid" runat="server" Caption="Summary Stocks" ShowHeaderWhenEmpty="True">
                    <HeaderStyle BackColor="Yellow" ForeColor="#CC3300" />
                </asp:GridView>
            </div>
        </div>--%>
        <div class="row margin-bottom alert alert-info">
            <div class="col-sm-12 overflow-y" style="max-height: 360px">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" Caption="Raw Data Stocks" ShowHeaderWhenEmpty="True">
                    <HeaderStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btfeed" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btfeed_Click">Feed To Navision</asp:LinkButton>
                <asp:LinkButton ID="btprintsummary" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprintsummary_Click">Print Summary</asp:LinkButton>
            </div>
        </div>
    </div>
     <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>

