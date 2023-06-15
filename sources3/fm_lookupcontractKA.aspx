<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupcontractKA.aspx.cs" Inherits="fm_lookupcontractKA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Searcth HO Agreement</title>
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
                <th>Contract No</th>
                <th>
                    <asp:TextBox ID="txsearch" runat="server"></asp:TextBox></th>
                <th>
                    <asp:LinkButton ID="btsearch" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                </th>
            </tr>
        </table>
    </div>
    <div>
        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Contract No">
                        <ItemTemplate>
                            <itemtemplate><a href="javascript:window.opener.ContractSelected('<%#Eval("contract_no") %>');window.close();"><%#Eval("contract_no") %></a></itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Manual No">
                        <ItemTemplate><%#Eval("manual_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created Date">
                        <ItemTemplate><%#Eval("create_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate><%#Eval("total") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%#Eval("status") %></ItemTemplate>
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
