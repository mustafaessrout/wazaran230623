<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupCM.aspx.cs" Inherits="fm_lookupCM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Searcth Credit Memo</title>
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
                <th>Credit Memo No</th>
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
                    <asp:TemplateField HeaderText="Credit Memo No">
                        <ItemTemplate>
                            <itemtemplate><a href="javascript:window.opener.CMSelected('<%#Eval("cm_no") %>');window.close();"><%#Eval("cm_no") %></a></itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reference No">
                        <ItemTemplate><%#Eval("cm_ref_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit Memo Date">
                        <ItemTemplate><%#Eval("cm_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash Amount">
                        <ItemTemplate><%#Eval("cash_amount") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Transfer Amount">
                        <ItemTemplate><%#Eval("bank_amount") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clearence of Debt Amount">
                        <ItemTemplate><%#Eval("debt_amount") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agreements Amount">
                        <ItemTemplate><%#Eval("ag_amount") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount">
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
