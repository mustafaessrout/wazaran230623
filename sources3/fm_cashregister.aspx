<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashregister.aspx.cs" Inherits="fm_cashregister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
     <script>
         function ShowProgress() {
             $('#pnlmsg').show();
         }

         function HideProgress() {
             $("#pnlmsg").hide();
             return false;
         }
         //$(document).ready(function () {
         //    $('#pnlmsg').hide();
         //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">CASH REGISTER</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row margin-bottom">
                    <label class="control-label col-md-1">
                        Cash In / Out
                    </label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbinout" onChange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="control-label col-md-9">
                        <strong style="color:red">Please check Beginning Balance, Total Cashout, Total Cash in more detail!</strong>
                    </div>
                </div>
                <div class="row margin-bottom margin-top" style="background-color: yellow">
                    <label class="control-label col-md-1">OPEN BALANCE </label>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbopenbalance" CssClass="control-label col-md-2" Font-Size="X-Large" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <label class="control-label col-md-1">CASH OUT </label>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcashout" Font-Size="X-Large" CssClass="control-label col-md-2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <label class="control-label col-md-1">CASH IN </label>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcashin" CssClass="control-label col-md-2" Font-Size="X-Large" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <label class="control-label col-md-1">CLOSING CASH </label>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcashClosing" CssClass="col-md-2 control-label" Font-Size="X-Large" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="row margin-bottom">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="No Data Found" ShowFooter="True" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Cashout Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdids" value='<%#Eval("cash_id") %>' runat="server" />
                                        <%#Eval("itemco_desc") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotamt" runat="server" Font-Bold="True" Font-Size="X-Large" Text=""></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#Eval("amt") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT">
                                    <ItemTemplate>
                                        <%#Eval("vat_amt") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotvatamt" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reference">
                                    <ItemTemplate>
                                        <asp:Label ID="lbrefno" runat="server" Text='<%#Eval("ref_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PIC">
                                    <ItemTemplate>
                                        <%#Eval("emp_desc") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="Yellow" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row margin-bottom">
                    <div class="col-md-12" style="text-align:center">
                        <asp:LinkButton ID="btaccept" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btaccept_Click">
                            <%if (cbinout.SelectedValue.ToString()=="I"){ %>
                            RECEIVED ALL
                            <%}else{ %>
                            PAID ALL
                            <%}%>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

