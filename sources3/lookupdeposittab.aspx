<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupdeposittab.aspx.cs" Inherits="lookupdeposittab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salesman Deposit</title>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">

            <div class="row margin-bottom margin-top">
            </div>
            <div class="row margin-bottom margin-top">
                <asp:GridView ID="grd" CssClass="table table-striped input-sm" runat="server" AutoGenerateColumns="False" EmptyDataText="Deposit not foulnd !" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Dep Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdepositcode" runat="server" Text='<%#Eval("tab_deposit_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbdepositdate" runat="server" Text='<%#Eval("deposit_dt","{0:d/M/yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate>
                                <%#Eval("emp_nm") %>
                                <asp:HiddenField ID="hdsalesman" Value='<%#Eval("salesman_cd") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lbamount" runat="server" Text='<%#Eval("amt","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Desc">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdacc" Value='<%#Eval("acc_no") %>' runat="server" />
                                <%#Eval("acc_desc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deposit No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hddepositNo" Value='<%#Eval("deposit_no") %>' runat="server" />
                                <%#Eval("deposit_no") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Import">
                            <ItemTemplate>
                                <asp:LinkButton ID="btimport" OnClick="btimport_Click" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server">Process</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Postpone">
                            <ItemTemplate>
                                <asp:LinkButton ID="btpostpone" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server">Postpone</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                <asp:LinkButton ID="btreject" CssClass="btn btn-danger btn-sm" OnClientClick="ShowProgress();" OnClick="btreject_Click" runat="server">Reject</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle BackColor="Yellow" />
                    <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" />
                </asp:GridView>
            </div>
        </form>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
