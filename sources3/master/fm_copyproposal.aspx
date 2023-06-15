<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_copyproposal.aspx.cs" Inherits="master_fm_copyproposal" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdprop" runat="server" />
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Copy Proposal</h4>
        <div class="h-divider"></div>
        <div class="form-group">
             <label class="control-label col-md-1">Proposal</label>
            <div class="col-md-8">
                <asp:TextBox ID="txproposal" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txproposal_AutoCompleteExtender" runat="server" TargetControlID="txproposal" MinimumPrefixLength="1" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" OnClientItemSelected="PropSelected" ServiceMethod="GetCompletionList" UseContextKey="True">
                </asp:AutoCompleteExtender>
            </div>
        </div>
        <h4 class="jajarangenjang">Proposal Date Changed</h4>
        <div class="h-divider"></div>
        <div class="form-group">
              <label class="control-label col-md-1">Start Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtstart" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
                <label class="control-label col-md-1">Delivery Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtdelivery" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtdelivery_CalendarExtender" runat="server" TargetControlID="dtdelivery" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-md-1">End Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtend" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-md-1">Claim Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtclaim" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtclaim_CalendarExtender" runat="server" TargetControlID="dtclaim" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Remark</label>
            <div class="col-md-11">
                <asp:TextBox ID="txremark" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">New Proposal</label>
            <div class="col-md-8">
                <asp:Label ID="lbnewproposal" CssClass="form-control" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-success" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btexecute" OnClientClick="javascript:ShowProgress();" OnClick="btexecute_Click" CssClass="btn btn-primary" runat="server">Duplicate Now!</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

