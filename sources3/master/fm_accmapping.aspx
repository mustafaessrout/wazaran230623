<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_accmapping.aspx.cs" Inherits="master_fm_accmapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/style.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function DebetSelected(sender, e) {
            $get('<%=hddebet.ClientID%>').value = e.get_value();
        }

        function CreditSelected(sender, e) {
            $get('<%=hdcredit.ClientID%>').value = e.get_value();
        }
    </script>

    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        $(document).ready(function () {
            $('#pnlmsg').hide();
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <asp:HiddenField ID="hdcredit" runat="server" />
    <asp:HiddenField ID="hddebet" runat="server" />
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Transaction Mapping</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-3">
                <asp:DropDownList ID="cbtrn" CssClass="form-control" onchange="javascript:ShowProgress();" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbtrn_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="cbrefno" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Debet</label>
            <div class="col-md-2">
                <asp:TextBox ID="txdebet" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txdebet_AutoCompleteExtender" runat="server" MinimumPrefixLength="1" ServiceMethod="GetCompletionList" TargetControlID="txdebet" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" EnableCaching="false" OnClientItemSelected="DebetSelected">
                </asp:AutoCompleteExtender>
            </div>
            <label class="control-label col-md-1">Credit</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcredit" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcredit_AutoCompleteExtender" runat="server" MinimumPrefixLength="1" TargetControlID="txcredit" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" EnableCaching="false" OnClientItemSelected="CreditSelected" ServiceMethod="GetCompletionList2">
                </asp:AutoCompleteExtender>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btsave" CssClass="btn btn-primary" runat="server" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
            </div>
        </div>
        <div class="form-group">
            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Transaction">
                        <ItemTemplate>
                            <asp:Label ID="lbrefno" runat="server" Text='<%#Eval("refno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate><%#Eval("refname") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debet">
                        <ItemTemplate><%#Eval("coa_debet_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate><%#Eval("coa_credit_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

