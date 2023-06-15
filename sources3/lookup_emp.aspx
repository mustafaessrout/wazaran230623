<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_emp.aspx.cs" Inherits="lookup_emp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Employee</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body style="font-family:Calibri,Tahoma,Verdana">
    <form id="form1" runat="server">
    <div class="divheader">
            Search Employee
    </div>
    <table>
        <tr><td>Salespoint</td><td>:</td><td><asp:DropDownList ID="cbsp" runat="server"></asp:DropDownList><asp:CheckBox ID="chall" runat="server" Text="ALL" /></td></tr>
       <tr><td>Employee ID / Name </td><td>:</td><td><asp:TextBox ID="txsearch" runat="server" Width="20em"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click"/></td></tr>
        </table>
        <div class="divgrid">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mygrid" CellPadding="2" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate><%# Eval("emp_cd") %>
                             </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><a href="javascript:window.opener.refreshdata('<%# Eval("emp_cd")%>');window.close();"><%# Eval("emp_nm") %></a></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Birth of Place">
                        <ItemTemplate><%# Eval("birth_place") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Birth Of Date">
                        <ItemTemplate><%# Eval("birth_dt") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nationality">
                        <ItemTemplate><%# Eval("nationality_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Title">
                        <ItemTemplate><%# Eval("job_title_nm") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
    </form>
</body>
</html>
