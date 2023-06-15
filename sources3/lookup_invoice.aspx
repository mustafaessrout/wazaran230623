<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_invoice.aspx.cs" Inherits="lookup_invoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Take Order / Canvaser Details</title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
           <div class="divheader">
               Take Order / Canvaser Details :
               <asp:Label ID="lblNumber" runat="server"></asp:Label>
            </div>
    
            <div class="h-divider"></div>
            <div class="divgrid">
                <div class="overflow-y" style="height:500px">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid table-fix" CellPadding="0" GridLines="Horizontal" OnPageIndexChanging="grd_PageIndexChanging" PageSize="30" HorizontalAlign="Left" ShowFooter="True" OnRowDataBound="grd_RowDataBound">
                        <AlternatingRowStyle/>
                        <Columns>
                   
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="total" runat="server" Text="  &nbsp; "></asp:Label>                                   
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
  
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                 <FooterTemplate>
                                    <asp:Label ID="lblTotalQty" runat="server"></asp:Label>                                   
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="total" runat="server" Text="&nbsp; "></asp:Label>                                   
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubTotal">
                                <ItemTemplate><%# Eval("subtotal","{0:N2}") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalInv" runat="server"></asp:Label>                                   
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>
