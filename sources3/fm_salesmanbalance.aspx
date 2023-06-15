<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesmanbalance.aspx.cs" Inherits="fm_salesmanbalance" %>

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
    <div class="alert alert-info text-bold">Salesman Balance</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Start</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtstart" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtstart">
                </asp:CalendarExtender>
            </div>
            <label class="control-label input-sm col-sm-1">End</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtend" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtend">
                </asp:CalendarExtender>
            </div>
            <label class="control-label input-sm col-sm-1">Salesman</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbsalesman" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12  overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="table table-bordered input-sm" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" EmptyDataText="No Balance Found" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp Code">
                            <ItemTemplate><%#Eval("emp_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction">
                            <ItemTemplate><%#Eval("cash_typ_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%#Eval("balance_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit">
                            <ItemTemplate>
                                <asp:Label ID="lbcashin" runat="server" Text='<%#Eval("cashin_amt","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit">
                            <ItemTemplate>
                                <asp:Label ID="lbcashout" runat="server" Text='<%#Eval("cashout_amt","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lbbalance" Font-Bold="true" Font-Size="Medium" runat="server" Text='<%#Eval("balancemove","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Currency">
                            <ItemTemplate><%#Eval("currency") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btprint" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:LinkButton ID="btprintall" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprintall_Click">Print All</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

