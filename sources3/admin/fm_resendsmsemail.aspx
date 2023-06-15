<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_resendsmsemail.aspx.cs" Inherits="admin_fm_resendsmsemail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/newbootstrap.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h3 class="jajarangenjang">Resend SMS And Email</h3>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1" style="font-size:small">Select SMS Or Email</label>
                <asp:RadioButtonList ID="rdsend" runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal" CssClass="form-control-static input-sm" AutoPostBack="True" OnSelectedIndexChanged="rdsend_SelectedIndexChanged">
                    <asp:ListItem Value="S">SMS</asp:ListItem>
                    <asp:ListItem Value="E">Email</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd_sms" runat="server" AutoGenerateColumns="False" CssClass="mygrid" Width="100%" OnSelectedIndexChanging="grdsms_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SMS TIME">
                                <ItemTemplate>
                                    <%# Eval("sms_dt") %>'></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOC NO">
                                <ItemTemplate>
                                    <asp:Label ID="lbsms" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMS TO">
                                <ItemTemplate><%# Eval("smsto") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMS">
                                <ItemTemplate><%# Eval("smsmsg") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="RESEND" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grd_email" runat="server" AutoGenerateColumns="False" CssClass="mygrid" Width="100%" OnSelectedIndexChanging="grdemail_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="EMAIL TIME">
                                <ItemTemplate>
                                    <%# Eval("email_dt") %>'></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOC NO">
                                <ItemTemplate><asp:Label ID="lbemail" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMAIL TO">
                                <ItemTemplate><%# Eval("emailto") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMAIL">
                                <ItemTemplate><%# Eval("emailmsg") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="RESEND" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

