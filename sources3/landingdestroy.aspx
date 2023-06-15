<%@ Page Language="C#" AutoEventWireup="true" CodeFile="landingdestroy.aspx.cs" Inherits="landingdestroy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Landing List</title>
    <script src="js/jquery-1.9.1.min.js"></script>
    <link href="css/beatifullcontrol.css" rel="stylesheet" />
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">List Of Pending Approval</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" PagerStyle-CssClass="pager" CssClass="mydatagrid" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0">
                    <Columns>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <asp:Label ID="lbtrnstockno" runat="server" Text='<%#Eval("trnstkno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%#Eval("trnstkdate") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("trnstkremark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Action" SelectText="Detail" ShowSelectButton="True" />
                    </Columns>
                 </asp:GridView>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
