<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_invoicereceived.aspx.cs" Inherits="fm_invoicereceived" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-bullet text-bold">
        Invoice Received
    </div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm col-sm-1 input-sm">Invoice Received Back From Customer</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txinvoiceno" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
            </div>
            <div class="col-sm-8 text-left">
                <span style="font-display:block;font-style:italic">Enter Invoice</span>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Inv No">
                            <ItemTemplate>
                                <asp:Label ID="lbinvoiceno" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No">
                            <ItemTemplate><%#Eval("manual_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DO No.">
                            <ItemTemplate><%#Eval("do_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate><%#Eval("cust_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Received Date">
                            <ItemTemplate>
                                <asp:Label ID="dtreceived" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Invoice Sign Cust">
                            <ItemTemplate>
                                <asp:FileUpload ID="fupl"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btreceived" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btreceived_Click" runat="server">Received</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>


