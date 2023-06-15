<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupemployee.aspx.cs" Inherits="master_lookupemployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="form-horizontal">
                <div class="form-group">
                     <table>
                        <tr><th>Search By Emp ID or Name</th><th>
                            </th>
                          </tr>     
                         <tr>
                             <td>
                                 <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                             </td>
                             <td>
                                  <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click">Search</asp:LinkButton>
                             </td>
                         </tr>
                    </table>
                </div>
                <div class="form-group">
                    <div class="col-md-12">
                           <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="0" EmptyDataText="No Employee Found !!" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lbempcode" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Name">
                        <ItemTemplate><%#Eval("fullname") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                </Columns>
                    <HeaderStyle CssClass="header"></HeaderStyle>

                    <PagerStyle CssClass="pager"></PagerStyle>

                    <RowStyle CssClass="rows"></RowStyle>
                </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
       
        
    </div>
        <div>
         
        </div>
    </form>
</body>
</html>
