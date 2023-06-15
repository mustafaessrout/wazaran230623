<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupdeposit.aspx.cs" Inherits="lookupdeposit" %>

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
    <form id="form1" runat="server">
        <div class="row margin-bottom margin-top">
            <label class="control-label input-sm col-sm-1">Status</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbstatus" onchange="ShowProgress();" AutoPostBack="true" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div>
            <asp:GridView ID="grd"  CssClass="table table-striped input-sm" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Dep Code">
                        <ItemTemplate>
                            <asp:Label ID="lbdepositcode" runat="server" Text='<%#Eval("deposit_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lbdepositdate" runat="server" Text='<%#Eval("deposit_dt","{0:d/M/yyyy}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman">
                        <ItemTemplate>
                            <%#Eval("salesman_nm") %>
                            <asp:HiddenField ID="hdsalesman" Value='<%#Eval("salesman_cd") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lbamount" runat="server" Text='<%#Eval("amt","{0:N2}") %>'></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%#Eval("salesdep_sta_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" />
            </asp:GridView>
        </div>
    </form>

     <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
