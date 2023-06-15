<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcust.aspx.cs" Inherits="lookupcust" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Customer</title>
    <script src="css/jquery-1.9.1.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/custom/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                &nbsp;
            </div>
            <div class="form-group">
                <label class="col-sm-1 input-sm control-label">Cust</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txsearch" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:GridView ID="grd" CssClass="table table-bordered table-condensed input-sm" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcode" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name">
                                <ItemTemplate>
                                    <%#Eval("cust_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Addresss">
                                <ItemTemplate><%#Eval("addr") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact">
                                <ItemTemplate><%#Eval("contact1") %></ItemTemplate>
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
