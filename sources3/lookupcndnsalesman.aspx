<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcndnsalesman.aspx.cs" Inherits="lookupcndnsalesman" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup</title>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
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
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <asp:GridView ID="grd" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" EmptyDataText="No CNDN Found !" ShowHeaderWhenEmpty="True" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Cndn Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcndncode" runat="server" Text='<%#Eval("cndn_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount"></asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EmptyDataRowStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
            <div class="row">
                <div class="col-sm-12 text-center">
                    <asp:LinkButton ID="btclose" CssClass="btn btn-primary btn-sm" OnClientClick="window.close();" runat="server">Close</asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
