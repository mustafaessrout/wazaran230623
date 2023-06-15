<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_po_ho_dtl.aspx.cs" Inherits="fm_po_ho_dtl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script>
       function openreport(url)
       {
           window.open(url, "myrep");
       }
   </script>
</head>
<body>
    <div class="container-fluid">
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <ol class="breadcrumb">
                        <li><h3><asp:Label ID="lbsalespoint" runat="server"></asp:Label> (<asp:Label ID="lbpo" runat="server"></asp:Label>)</h3></li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>PO Date</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbpo_dt" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>Delivery Date</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbdelivery_dt" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Destination</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbdestination" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>To</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbto" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Remark</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-9"><h6><strong><asp:Label ID="lbremark" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h5><strong>Details</strong></h5></div>
                <div class="col-sm-1"><h6></h6></div>
                <div class="col-sm-9"></div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdpo" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="false" EmptyDataText="NO DATA" CssClass="table table-striped"  ShowFooter="True" OnRowEditing="grdpo_RowEditing" OnRowCancelingEdit="grdpo_RowCancelingEdit" OnRowUpdating="grdpo_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %>.</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate>
                                        <%# Eval("branded_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" type="number" Text='<%# Eval("qty") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Stock">
                                    <ItemTemplate><%# Eval("stock") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CWH Stock">
                                    <ItemTemplate><%# Eval("stock_cwh") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("unit_price") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsubtotal" runat="server" Text='<%# Eval("subtotal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lbsource" runat="server" Text='<%# Eval("lbsource") %>'></asp:Label>--%>
                                        <asp:DropDownList ID="cbsource" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="NA">No Action</asp:ListItem>
                                            <asp:ListItem Value="CWH">CWH</asp:ListItem>
                                            <asp:ListItem Value="FA">Factory</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:CommandField ShowEditButton="True" />--%>
                            </Columns>
                            <HeaderStyle CssClass="table-header" />
                            <FooterStyle CssClass="table-footer" />
                            <EditRowStyle CssClass="table-edit" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                        </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div> 

        </div>

        <div class="row">
          <div class="col-sm-12">
            <div class="text-center">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-success btn-print" OnClick="btprint_Click" />
                <asp:Button ID="btprocess" runat="server" Text="Process this PO" CssClass="btn btn-primary btn-save" OnClick="btprocess_Click" />
            </div>
          </div>
        </div>

    </form>
    </div>
</body>
</html>
