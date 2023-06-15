<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupinv.aspx.cs" Inherits="fm_lookupinv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function SelectInv(vselect)
        {
            window.opener.RefreshData(vselect);
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="divheader">
            Invoice Available to be cancelled
        </div>
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="mygrid" ForeColor="#333333" GridLines="None" OnRowDataBound="grd_RowDataBound">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Inv No.">
                    <ItemTemplate>
                        <%# Eval("manual_no") %>
                        <asp:HiddenField ID="hdinvdate" runat="server" value='<%# Eval("inv_dt") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sys No.">
                    <ItemTemplate><a href="javascript:SelectInv('<%# Eval("inv_no") %>');"><%# Eval("inv_no") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date"><ItemTemplate><%# Eval("inv_dt","{0:d/M/yyyy}")%></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Cust Code">
                    <ItemTemplate><%# Eval("cust_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust Name">
                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salesman">
                    <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salesman Name">
                    <ItemTemplate><%# Eval("emp_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                <ItemTemplate><%# Eval("inv_sta_nm") %></ItemTemplate>
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
    </form>
</body>
</html>
