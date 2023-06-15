<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_tabsalesorder.aspx.cs" Inherits="fm_tabsalesorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
     <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    
    <%--script--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/index.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/jquery.floatThead.js"></script>



    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</head>
<body >
    <div class="containers bg-white">
        <div class="row  ">
            <form id="form1" runat="server" class="container-fluid">         
                <div class="divheader">tablet sales order transaction</div>
                <div class="h-divider"></div>
                <div class="form-group clearfix">
                    <label class="control-label pull-left">Status: </label>
                    <div class="drop-down col-sm-5">
                        <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" ></asp:DropDownList>
                    </div>
                </div>
          
                <div class="overflow-x margin-bottom" style="max-height:150px">
                    <asp:GridView ID="grdtab" CssClass="table table-fix table-striped table-hover mygrid" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanging="grdtab_SelectedIndexChanging"  OnPageIndexChanging="grdtab_PageIndexChanging">
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"  OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tab No">
                                <ItemTemplate>
                                    <asp:Label ID="lbso_cd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment No">
                                <ItemTemplate>
                                    <asp:Label ID="lbpayment_no" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual No">
                                <ItemTemplate>
                                    <asp:Label ID="lbmanual_no" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbso_dt" runat="server" Text='<%# Eval("so_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate>
                                    <%# Eval("emp_desc") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="lbcust" runat="server" Text='<%# Eval("customer") %>'></asp:Label>
                                    <%--<asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disc On/Off">
                                <ItemTemplate>
                                    <asp:Label ID="lbrdonoff" runat="server" Text='<%# Eval("rdonoff") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbtype" runat="server" Text='<%# Eval("payment_type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbloc" runat="server" Text='<%# Eval("location") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate> <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>' Visible="false"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate> <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>' Visible="false"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate> <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>' Visible="false"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate> <asp:Label ID="lbwhs_cd" runat="server" Text='<%# Eval("whs_cd") %>' Visible="false"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate> <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd") %>' Visible="false"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>

                <div class="h-divider"></div>

                <asp:Label Text="TRANSACTION DETAIL" runat="server" ID="grdtabdtl_lbl" Visible="false"/>

                <div class="overflow-x" style="max-height:180px;">
                    <asp:GridView ID="grdtabdtl" CssClass="table table-fix table-striped table-hover mygrid" runat="server" AutoGenerateColumns="False"  CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" ForeColor="#333333" GridLines="None" Width="100%" ShowFooter="True">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <%# Eval("item_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Order">
                                <ItemTemplate>
                                    <%# Eval("qty") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotqty" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate>
                                    <%# Eval("unitprice") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subtotal">
                                <ItemTemplate>
                                    <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal","{0:F2}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock Cust">
                                <ItemTemplate>
                                    <%# Eval("stock_cust") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <%# Eval("uom") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    </asp:GridView>
                </div>
                
                <asp:Label Text="" CssClass="h-divider" ID="devider" Visible="false" runat="server" />
                
                <div class="divheader">Discount Information</div>
                        <asp:GridView ID="grddisc" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Disc Code">
                                    <ItemTemplate><%# Eval("disc_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Qty">
                                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Free Amt">
                                    <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                <div class="navi" style="padding-bottom:1em;padding-top:1em">
                    <asp:Button ID="btapply" runat="server" Text="Transfer To Back Office" CssClass="btn-success btn btn-add" OnClick="btapply_Click"/>
                    <asp:Button ID="btcancel" runat="server" Text="Cancel Tab Salesorder" CssClass="btn-danger btn btn-delete" OnClick="btcancel_Click"/>
                    <asp:Button ID="btpostpone" runat="server" Text="Postphone to next day" CssClass="btn-warning btn btn-edit" OnClick="btpostpone_Click"/>

                </div>
                     
            </form>

            <div class="container-fluid">
                    Compulsory before transferred :<br />
                    1. Tablet with advanced date, if has discount unable to transfer if hit by discounnt and out of disc date.<br />
                    2. 
                    Tablet date will allignment with system date at back office.

            </div>
           </div>
        </div>
</body>
</html>
