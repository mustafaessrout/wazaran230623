<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupclaimcndn.aspx.cs" Inherits="lookupclaimcndn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup CN</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
     
</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">Search claim CN/DN</div>
            <div class="h-divider"></div>

            <div class="container">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Sys No">
                            <ItemTemplate>
                                <asp:Label ID="lblcndncode" runat="server" Text='<%# Eval("cndn_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("cndn_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust">
                            <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt Paid">
                            <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><%# Eval("status") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle CssClass="table-edit" />
                </asp:GridView>                
            </div>
        </div>
    </form>
</body>
</html>
