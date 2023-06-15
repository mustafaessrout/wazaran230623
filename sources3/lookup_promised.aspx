<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_promised.aspx.cs" Inherits="lookup_promised" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Promised</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    
    <script src="js/jquery-1.9.1.min.js"></script>
    
    <script src="admin/js/bootstrap.min.js"></script>
   <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="containers bg-white">
        <div class="divheader">List Of Promised</div>
        <div class="h-divider"></div>

        <div class="container">
                <div class="form-group">
                    <div class="overflow-x" style="height:500px;">
                        <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-fix mygrid" AutoGenerateColumns="False" CellPadding="2" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Promised No">
                                     <ItemTemplate>
                                    <asp:Label runat="server" ID="lbpromisedcode" Text='<%# Eval("promised_cd") %>'></asp:Label>
                                   </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate><%# Eval("promised_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amt">
                                    <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust">
                                    <ItemTemplate><%# Eval("cust") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman">
                                    <ItemTemplate><%# Eval("salesman") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                        </asp:GridView>
                    </div>
                </div>
           
        </div>
    </div>
    </form>
</body>
</html>
