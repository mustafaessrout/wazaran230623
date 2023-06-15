<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptinvoicelog.aspx.cs" Inherits="fm_rptinvoicelog" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Invoice Logging</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Period</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbperiod" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-condensed table-hover" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Order No">
                            <ItemTemplate><%#Eval("so_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%#Eval("so_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust code">
                            <ItemTemplate><%#Eval("cust_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Loading Date"></asp:TemplateField>
                        <asp:TemplateField HeaderText="GDN Date"></asp:TemplateField>
                        <asp:TemplateField HeaderText="GDN Received"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoide Date"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Received"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

