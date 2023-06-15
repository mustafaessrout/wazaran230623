<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_book.aspx.cs" Inherits="lookup_book" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="divheader">
        Booking List
    </div>
        <img src="div2.png" class="divid" />
        <div class="divgrid">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Document Type">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdbookno" runat="server" Value='<%# Eval("book_no") %>' />
                            <%# Eval("doc_typ_nm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman">
                        <ItemTemplate><a href="javascript:window.opener.RefreshData('<%# Eval("book_no") %>');window.close();"> <%# Eval("emp_nm") %>  </a>                          
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start No">
                        <ItemTemplate><%# Eval("start_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End No">
                        <ItemTemplate><%# Eval("end_no") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
