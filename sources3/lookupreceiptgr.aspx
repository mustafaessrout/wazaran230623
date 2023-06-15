<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupreceiptgr.aspx.cs" Inherits="lookupreceiptgr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup</title>
    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group"></div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:GridView ID="grd" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate><%#Eval("item_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><i class="input-sm" style="font-weight:bold"><%#Eval("item_nm") %></i> </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%#Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="Yellow" Font-Bold="True" />
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div style="text-align:center" class="col-sm-12">
                    <asp:LinkButton ID="btclose" CssClass="btn btn-primary" OnClientClick="window.close();" runat="server">Close</asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
