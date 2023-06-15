<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_bankhoconfirmandpostpone.aspx.cs" Inherits="fm_bankhoconfirmandpostpone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Bank HO Confirm / Postpone</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-2">
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                        OnRowCommand="grd_RowCommand" DataKeyNames="deposit_id" Width="900">
                        <Columns>
                            
                            <asp:BoundField DataField="transtyp" HeaderText="Type" />
                            <asp:BoundField DataField="bankname" HeaderText="Bank Name" />
                            <asp:BoundField DataField="payment_no" HeaderText="Payment No" />
                            <asp:BoundField DataField="amt" HeaderText="Amount" />
                            <asp:ButtonField ButtonType="button" CommandName="btnConfirmation" HeaderText="Confirm" ControlStyle-CssClass="btn btn-success" Text="Confirmation" />
                            <asp:ButtonField ButtonType="button" CommandName="btnPostpone" HeaderText="Postpone" ControlStyle-CssClass="btn btn-print" Text="Postpone" />
                            <asp:ButtonField ButtonType="button" CommandName="btnrejected" HeaderText="Rejected" ControlStyle-CssClass="btn btn-warning" Text="Rejected" />
                            <%--<asp:ButtonField ButtonType="button" CommandName="btnShowDetails" HeaderText="ShowDetails" ControlStyle-CssClass="btn btn-info" Text="ShowDetails" />--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
