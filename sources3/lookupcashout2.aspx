<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcashout2.aspx.cs" Inherits="lookupcashout2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Cashout</title>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="frmmain" runat="server">
        <div class="container">
            <div class="row">
                <div>
                    <asp:DropDownList CssClass="form-control" ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div>
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Cashout Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcashoutcode" runat="server" Text='<%#Eval("cashout_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cashout Name">
                                <ItemTemplate><%#Eval("itemco_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <%#Eval("amt") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT">
                                <ItemTemplate>
                                    <%#Eval("vat_amt") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <%#Eval("cashout_sta_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
