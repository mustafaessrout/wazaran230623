<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_ct.aspx.cs" Inherits="lookup_ct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Transfer</title>
    
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>
    <script>
        function closepage(sval)
        {
            window.opener.RefreshData(sval);
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">

       
            <div class="divheader">Lookup Customer Transfer</div>
            <div class="h-divider"></div>

            <div class="margin-bottom">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid">
                    <Columns>
                        <asp:TemplateField HeaderText="Transfer No.">
                            <ItemTemplate><a href="javascript:closepage('<%# Eval("trf_no") %>');"><%# Eval("trf_no") %></a></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("trf_dt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Old Salesman">
                            <ItemTemplate><%# Eval("old_salesman") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%# Eval("old_name") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="New Salesman">
                            <ItemTemplate><%# Eval("new_salesman") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%# Eval("new_name") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer"/>
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page"/>
                    <RowStyle />
                    <SelectedRowStyle CssClass="table-edit"/>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
