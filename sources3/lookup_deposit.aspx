<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_deposit.aspx.cs" Inherits="lookup_deposit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposit browse</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="frmdeposit" runat="server">
    <div class="divheader">
        List Of Deposit Available
    </div>
        <div>
            <asp:GridView ID="grd" runat="server" CssClass="mygrid" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Dep Code">
                        <ItemTemplate>
                            <asp:Label ID="lbdepcode" runat="server" Text='<%# Eval("dep_cd") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%# Eval("dep_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dep Type">
                        <ItemTemplate>
                            <%# Eval("dep_typ_nm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payment Type">
                        <ItemTemplate><%# Eval("payment_typ_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust">
                        <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman">
                        <ItemTemplate><%# Eval("emp_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate><%# Eval("amt","{0:F2}") %></ItemTemplate>
                        <ItemTemplate>
                            <%# Eval("amt","{0:F2}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
