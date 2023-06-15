<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_targetsp.aspx.cs" Inherits="lookup_targetsp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divheader">
       List Of Sales Target
    </div>
        <img src="div2.png" />
    <div>
        Period : <asp:DropDownList ID="cbperiod" runat="server" Width="20em"></asp:DropDownList>
    </div>
    <div class="divgrid">

        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="No"></asp:TemplateField>
                <asp:TemplateField HeaderText="Salesman Code"></asp:TemplateField>
                <asp:TemplateField HeaderText="Salesman Name"></asp:TemplateField>
                <asp:TemplateField HeaderText="Remark"></asp:TemplateField>
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
