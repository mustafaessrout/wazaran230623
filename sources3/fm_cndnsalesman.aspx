<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cndnsalesman.aspx.cs" Inherits="fm_cndnsalesman" %>

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
        function SelectData(x) {
            $get('<%=hdcndncode.ClientID%>').value = x;
            $get('<%=btlookup.ClientID%>').click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcndncode" runat="server" />
    <div class="alert alert-info text-bold">CNDN Salesman </div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">DN Direct No</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txcndnno" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();PopupCenter('lookupcndnsalesman.aspx','Lookup',800,600);" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label-sm input-sm col-sm-1">CNDN Type</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbcndntype" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Post Date</label>
            <div class="col-sm-2 drop-down-date require">
                <asp:TextBox ID="dtpost" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtpost_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtpost">
                </asp:CalendarExtender>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Date</label>
            <div class="col-sm-2 drop-down-date require">
                <asp:TextBox ID="dtcndn" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtcndn_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtcndn">
                </asp:CalendarExtender>
            </div>

        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">Salesman</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbsalesman" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged"></asp:DropDownList>
            </div>
             <label class="control-label-sm input-sm col-sm-1">Reason</label>
            <div class="col-sm-8 require">
                <asp:TextBox ID="txreason" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">Amount (Inclusive VAT)</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txamt" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Current Balance</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txcurrentbalance" CssClass="form-control input-sm" runat="server" Font-Bold="True"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">VAT</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbvat" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
           
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" OnClientClick="ShowProgress();" CssClass="btn btn-info btn-sm" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" OnClientClick="ShowProgress();" CssClass="btn btn-danger btn-sm" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:Button ID="btlookup" Style="display: none" runat="server" OnClick="btlookup_Click" Text="Button" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

