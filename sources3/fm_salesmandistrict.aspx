<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_salesmandistrict.aspx.cs" Inherits="fm_salesmandistrict" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Trip</title>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Salesman : <asp:Label ID="lbsalesman" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <div class="row">
                <div class="col-sm-4">
                    <asp:DropDownList ID="cbcity" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcity_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="cbdistrict" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
                <div class="col-sm-1">
                    <asp:LinkButton ID="btadd" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                </div>
            </div>
        </div>
        <div>
            <asp:GridView ID="grd" CssClass="table table-bordered input-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="No District Assigned !" ShowHeaderWhenEmpty="True" OnRowDeleting="grd_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdcity" Value='<%#Eval("city_cd") %>' runat="server" />
                            <%#Eval("city_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District">
                        <ItemTemplate>
                            <asp:HiddenField ID="hddistrict" Value='<%#Eval("district_cd") %>' runat="server" />
                            <%#Eval("district_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
