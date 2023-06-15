<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rptmerchavisit.aspx.cs" Inherits="fm_rptmerchavisit" %>

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
    <div class="container">
        <h4 class="jajarangenjang">Merchandiser Visit</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Report Type</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbreporttype" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbreporttype_SelectedIndexChanged">
                        <asp:ListItem Value="VM">Visit Merchandiser</asp:ListItem>
                        <asp:ListItem Value="SC">Stock Customer</asp:ListItem>
                        <asp:ListItem Value="VA">Visit All Merchandiser</asp:ListItem>
                        <asp:ListItem Value="MR">Merchandising Report</asp:ListItem>
                        <asp:ListItem Value="MD">Merchandising Summary Report</asp:ListItem>
                        <asp:ListItem Value="OS">Customer Out Off Stock Report</asp:ListItem>
                        <asp:ListItem Value="SN">STOCK CUSTOMER NEAR EXPIRE</asp:ListItem>
                        <asp:ListItem Value="MJ">Merchandiser Jared Report</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Merchandiser</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbmerchandiser" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Date Visit (Fr ..To)</label>
                <div class="col-md-2">
                    <div class="col-md-6 input-sm">
                        <asp:TextBox ID="dtvisit" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="dtvisit_CalendarExtender" runat="server" TargetControlID="dtvisit" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                    <div class="col-md-6 input-sm">
                        <asp:TextBox ID="dtend" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>
                <label class="control-label col-md-1">Product</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbproduct" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>


            </div>
            <div class="row margin-bottom center">
                <div class="col-md-1">
                    <asp:LinkButton ID="btprint" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btprint_Click">PRINT</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

