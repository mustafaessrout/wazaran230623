<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_cmo_ho.aspx.cs" Inherits="fm_lookup_cmo_ho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search CMO Generated</title>
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
                    <th>CMO No</th>
                    <th>
                        <asp:TextBox ID="txsearch" runat="server"></asp:TextBox></th>
                    <th>
                        <asp:LinkButton ID="btsearch" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                    </th>
                    <th>
                        <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                            <asp:ListItem Value="ALL" Text="ALL STATUS"></asp:ListItem>
                            <asp:ListItem Value="N" Text="NEW"></asp:ListItem>
                            <asp:ListItem Value="A" Text="APPROVED"></asp:ListItem>
                            <asp:ListItem Value="R" Text="REJECTED"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="CMO No">
                        <ItemTemplate>
                            <itemtemplate><a href="javascript:window.opener.SelectCMO('<%#Eval("cmo_no") %>');window.close();"><%#Eval("cmo_no") %></a></itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Periode">
                        <ItemTemplate><%#Eval("periode") %></ItemTemplate>
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
