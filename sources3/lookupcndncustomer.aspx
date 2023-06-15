<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcndncustomer.aspx.cs" Inherits="lookupcndncustomer" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="row margin-bottom">
            <label class="col-sm-3 control-label input-sm">Status</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="cbstatus" CssClass="form-control input-sm" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-bordered input-sm" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Cndn No">
                            <ItemTemplate>
                                <asp:Label ID="lbcndnnumber" runat="server" Text='<%#Eval("cndn_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%#Eval("cndn_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Post Date">
                            <ItemTemplate>
                                <%#Eval("post_dt","{0:d/M/yyyy}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate><%#Eval("cust_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate><%#Eval("crdb") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount"></asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
