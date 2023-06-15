<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_discountinfo.aspx.cs" Inherits="fm_discountinfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>

    <script>
        function openreport(url) {
            window.open(url, url, "toolbar=no;fullscreen=yes;scrollbars=yes", true);
        }
    </script>
   
     
</head>
<body>
    <div class="containers bg-white">
        <div class="row">
            <form id="form1" runat="server" class="container-fluid">
                <asp:toolkitscriptmanager runat="server"></asp:toolkitscriptmanager>
        
                <div class="divheader">Discount Information</div>
                <div class="h-divider"></div>

                <div class="clearfix margin-bottom">
                    <div class=" clearfix col-sm-6 no-padding">
                        <div class="clearfix">
                            <asp:Label Text="Created Date" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbdate" runat="server" CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label Text="Delivery Date" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbdelivery" runat="server" Text="lbdelivery"  CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label Text="Start Date" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbstart" runat="server"  CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label ID="Label1" runat="server" Text="Created By" CssClass="titik-dua col-xs-4 control-label control-label-sm" ></asp:Label>
                            <div class="col-xs-8 ">
                                <asp:TextBox ID="txcratedby" runat="server" CssClass="ro padding-top-4 block"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label ID="Label2" runat="server" Text="Proposal No." CssClass="titik-dua col-xs-4 control-label control-label-sm" ></asp:Label>
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbpropno" runat="server" CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class=" clearfix col-sm-6 no-padding">
                        <div class="clearfix">
                            <asp:Label Text="Discount No." runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbdiscno" runat="server" CssClass="padding-top-4 block text-red text-bold"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label Text="Status" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbstatus" runat="server" CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label Text="End Date" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbend" runat="server" CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label Text="Qty Order Maximum" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbqtymax" runat="server" CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix">
                            <asp:Label Text="Discount Mechanism" runat="server" CssClass="titik-dua col-xs-4 control-label control-label-sm" />
                            <div class="col-xs-8 ">
                                <asp:Label ID="lbdiscmec" runat="server" CssClass="padding-top-4 block"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="h-divider"></div>

                <div class="clearfix ">
                    <asp:Label Text="Efective Salespoint" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" AllowPaging="True" Width="100%" OnPageIndexChanging="grd_PageIndexChanging" EmptyDataText="NO DATA" CssClass="table mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate><%# Eval("salespointcd") %></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salespoint Name">
                                    <ItemTemplate><%# Eval("salespoint_sn") %></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                </div>

                <div class="clearfix">
                    <asp:Label Text="Item to be discount Discount" runat="server" CssClass="text-bold"/>
                    <div>
                        <%# Eval("salespointcd") %>
                            <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" AllowPaging="True" Visible="False" Width="100%" OnPageIndexChanging="grditem_PageIndexChanging" EmptyDataText="NO DATA" CssClass="table mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                
                                    <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
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
                        <%# Eval("salespoint_sn") %>
                    </div>
                </div>

                <div class="clearfix">
                    <asp:Label Text="Product To Be Discount" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdprod" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" AllowPaging="True" Visible="true" Width="100%" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                
                                    <ItemTemplate><%# Eval("prod_cd") %></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
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
                </div>

                <div class="clearfix">
                    <asp:Label Text="Valid for Customer Type" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdcusttype" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="table mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Type">
                                    <ItemTemplate><%# Eval("otlcd_nm") %></ItemTemplate>
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
                </div>

                <div class="clearfix">
                    <asp:Label Text="Valid For Customer Group" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdcustgroup" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate><%# Eval("cusgrcd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Group">
                                    <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
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
                </div>

                <div class="clearfix">
                    <asp:Label Text="Valid for Customer" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdcustomer" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate><%# Eval("cust_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate><%# Eval("addr") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate><%# Eval("city_nm") %></ItemTemplate>
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
                </div>

                <div class="clearfix">
                    <asp:Label Text="Discount Formula" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdformula" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" AllowPaging="True" Width="100%" EmptyDataText="NO DATA" OnPageIndexChanging="grdformula_PageIndexChanging" CssClass="mygrid">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Minimum Qty">
                                
                                        <ItemTemplate><%# Eval("min_qty") %></ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate><%# Eval("uom")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount Method">
                                        <ItemTemplate><%# Eval("disc_method_nm") %></ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Qty">
                                        <ItemTemplate><%# Eval("disc_qty") %></ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free UOM">
                                        <ItemTemplate><%# Eval("uom_free") %></ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Cash">
                                        <ItemTemplate><%# Eval("disc_amt") %></ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Cash">
                                        <ItemTemplate><%# Eval("disc_pct") %></ItemTemplate>
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
                </div>


                 <div class="clearfix">
                    <asp:Label Text="Free Item" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdfreeitem" runat="server" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" Width="100%" CellPadding="0" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
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
                </div>

                <div class="clearfix">
                    <asp:Label Text="Free Product" runat="server" CssClass="text-bold"/>
                    <div>
                        <asp:GridView ID="grdfreeproduct" runat="server" AllowPaging="True" Width="100%" CellPadding="0" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="NO DATA" ShowHeaderWhenEmpty="True" CssClass="mygrid">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Free Group Product Code">
                                    <ItemTemplate><%# Eval("prod_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Group Product Name">
                                    <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
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
                </div>

                <div class="h-divider"></div>
                <div class="navi margin-bottom">
                    <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
