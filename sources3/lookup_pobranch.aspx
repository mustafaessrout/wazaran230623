<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_pobranch.aspx.cs" Inherits="lookup_pobranch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup PO Branchese</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>

    <script>
        function setvalue(sval)
        {
            window.opener.setvalue(sval);
            window.close();
        }
    </script>
</head>
<body>
    <form id="fm" runat="server" class="center">
        <div class="containers bg-white" style="width:800px;">
            <div class="divheader">PO Branches List</div>
            <div class="h-divider"></div>

            <div class="clearfix margin-bottom">
                <label class="control-label col-sm-2">Salespoint </label> 
                <div class="drop-down col-sm-10">
                    <asp:DropDownList ID="cbsp" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsp_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="divgrid margin-bottom">
                <asp:GridView ID="grd" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="0" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdsp" Value='<%# Eval("salespointcd") %>' runat="server" />
                                <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO No">
                            <ItemTemplate>
                              <a href="javascript:setvalue('<%# Eval("po_no") %>');"><asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("po_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Delivery">
                            <ItemTemplate>
                                <%# Eval("po_delivery_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle CssClass="table-edit" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
