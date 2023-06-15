<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupdo.aspx.cs" Inherits="lookupdo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Invoice</title>
    <link href="css/styles.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Calibri; font-size: small">
            <table>
                <tr>
                    <th>Invoice No</th>
                    <th>
                        <asp:TextBox ID="txsearch" runat="server"></asp:TextBox></th>
                    <th>
                        <asp:LinkButton ID="btsearch" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                    </th>
                    <th>
                        <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                            <asp:ListItem Value="ALL" Text="ALL STATUS"></asp:ListItem>
                            <asp:ListItem Value="D" Text="RECEIVING DRIVER"></asp:ListItem>
                            <asp:ListItem Value="C" Text="RECEIVING CUSTOMER"></asp:ListItem>
                            <asp:ListItem Value="W" Text="WAITING CONFIRMATION"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Inv No">
                        <ItemTemplate>
                            <itemtemplate><a href="javascript:window.opener.SelectInvoice('<%#Eval("inv_no") %>');window.close();"><%#Eval("inv_no") %></a></itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate><%#Eval("manual_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemTemplate><%#Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delivery date"></asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman">
                        <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amt">
                        <ItemTemplate><%#Eval("totamt") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
