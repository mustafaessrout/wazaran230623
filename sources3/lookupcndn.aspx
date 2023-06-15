<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcndn.aspx.cs" Inherits="lookupcndn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup CNDN</title>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
    <style>
        th {
            position: sticky;
            top: 0;
        }
    </style>
</head>
<body>
    <form name="idmain" runat="server">
        <div class="alert alert-info text-bold">
            Lookup  CN DN
        </div>
        <div class="container">
            <div class="row margin-bottom">
                <label class="col-sm-2 control-label input-sm">Status</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="cbstatus" CssClass="form-control input-sm" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-sm-12 overflow-y" style="max-height: 360px">
                    <asp:GridView ID="grd" CssClass="table table-bordered input-sm" runat="server" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="CNDN No">
                                <ItemTemplate><%#Eval("cndn_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%#Eval("cndn_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%#Eval("cndntype") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbamount" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" Text='<%#Eval("amt","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate><%#Eval("cust_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
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
        </div>
    </form>
</body>
</html>
