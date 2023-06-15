<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptpettycash.aspx.cs" Inherits="fm_rptpettycash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        th {
            position: sticky;
            top: 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Petty Cashier Report</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Pettycash PIC</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbcashierid" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label input-sm col-sm-1">Start Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtstart" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtstart">
                </asp:CalendarExtender>
            </div>
            <div class="row margin-bottom">
                <label class="control-label input-sm col-sm-1">End Date</label>
                <div class="col-sm-2 drop-down-date">
                    <asp:TextBox ID="dtend" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtend_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtend">
                    </asp:CalendarExtender>
                </div>

                <div class="col-sm-1">
                    <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
                </div>
            </div>

        </div>
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Opening Balance</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txopeningbalance" Font-Bold="true" Font-Size="Medium" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Ending Balance</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txendingbalance" Font-Bold="true" Font-Size="Medium" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="table table-bordered table-sm" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="#Ref">
                            <ItemTemplate><%#Eval("refno") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%#Eval("cash_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate><%#Eval("itemco_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("itemco_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit">
                            <ItemTemplate><%#Eval("cashin_amt","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit">
                            <ItemTemplate><%#Eval("cashout_amt","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate><%#Eval("balancemove","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btprint" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

