<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupvhc.aspx.cs" Inherits="maintenance_lookupvhc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Vehicle</title>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
   
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
   <br /><br /><br />
    <form id="form1" runat="server">

        <div class="container">
            <div class="form-horizontal" style="font-family:Calibri;font-size:small">
                <div class="form-group">
                    <label class="control-label col-sm-2">Search Plate No</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txsearch" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                    <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                    </div>
                    </div>
                <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" CssClass="mydatagrid" HeaderStyle-CssClass="header" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Vhc Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbvhccode" runat="server" Text='<%#Eval("vhc_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plate No">
                                <ItemTemplate><%#Eval("plate_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Engine No">
                                <ItemTemplate><%#Eval("engine_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plat Type">
                                <ItemTemplate><%#Eval("plate_typ") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Driver">
                                <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                     
                    </asp:GridView>
                </div>
               </div>
            </div>
        </div>
   
    </form>
</body>
</html>
