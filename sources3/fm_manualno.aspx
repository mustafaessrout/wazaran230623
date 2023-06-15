<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_manualno.aspx.cs" Inherits="fm_manualno" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="form-horizontal" style="font-family: Calibri">

        <div runat="server" id="showCheck">
            <h4 class="jajarangenjang">Manual No Confirmation</h4>
            <div class="h-divider"></div>
            <%-- Info CHECK Manual NO --%>
            <div class="form-group">
                <div class="col-md-6">
                    <table class="mGrid">
                        <tr>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                        <asp:TextBox ID="dtstart" runat="server" CssClass="form-control "></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                                        </asp:CalendarExtender>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control "></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                                        </asp:CalendarExtender>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:LinkButton ID="btcheck" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btcheck_Click"><i class="fa fa-check-square">&nbsp;Check Data</i></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div runat="server" id="showInvoice">
        <h5 class="jajarangenjang">Invoice (Takeorder)</h5>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grdinvoice" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grdinvoice_RowCancelingEdit" OnRowEditing="grdinvoice_RowEditing" OnRowUpdating="grdinvoice_RowUpdating" CellPadding="0">
                    <Columns>
                        <asp:TemplateField HeaderText="Manual No">
                            <ItemTemplate>
                                <asp:Label ID="lbmanual_no" runat="server" Text='<%#Eval("manual_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lbremarks" runat="server" Text='<%#Eval("remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txremarks" Text='<%#Eval("remarks") %>' runat="server" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>   
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Add Remarks" ShowEditButton="True" /> 
                    </Columns>
                    <FooterStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
    </div>

    </div>

    <div class="divmsg loading-cont" style="display: none" id="pnlmsg">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>. 
</asp:Content>

