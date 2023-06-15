<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesman_dep.aspx.cs" Inherits="fm_salesman_dep" %>

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
            $get('<%=hddeposit.ClientID%>').value = x;
            $get('<%=btlookup.ClientID%>').click();
        }
        function SelectDeposit(x) {
            $get('<%=hddeposit.ClientID%>').value = x;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hddeposit" runat="server" />
    <div class="alert alert-info text-bold">Salesman Deposit</div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Deposit Code</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="lbdepositcode" CssClass="form-control input-sm input-group-sm" runat="server" Text=""></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearchdeposit" OnClientClick="PopupCenter('lookupdeposit.aspx','Deposit Salesman',800,800);" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Device Code</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="lbtabdevicecode" CssClass="form-control input-sm input-group-sm" runat="server" Text=""></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearchdeposittab" OnClientClick="PopupCenter('lookupdeposittab.aspx','Deposit Salesman',1000,800);" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtdeposit" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Account No</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbbankaccount" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Salesman</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbsalesman" CssClass="form-control input-sm" runat="server" AutoPostBack="true" onchange="ShowProgress();" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <label class="control-label-sm input-sm col-sm-1">Amount</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txamt" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Deposit No</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txdepositno" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Remark</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <label class="control-label input-sm col-sm-1">Last Balance</label>
            <div class="col-sm-2">
                <asp:Label ID="lbbalance" CssClass="form-control input-sm" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-5" style="color:red">
                Last balance salesman registered , please confirmed with salesman!
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" CssClass="btn btn-success btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:Button ID="btlookup" Style="display: none" OnClientClick="ShowProgress();" runat="server" Text="Button" OnClick="btlookup_Click" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

