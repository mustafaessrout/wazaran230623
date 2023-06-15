<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupreqdiscount.aspx.cs" Inherits="lookupreqdiscount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Request Discount(Scheme)</title>
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
                    <th>Request Discount No</th>
                    <th>
                        <asp:TextBox ID="txsearch" runat="server"></asp:TextBox></th>
                    <th>
                        <asp:LinkButton ID="btsearch" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                    </th>
                    <th>
                        <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                            <asp:ListItem Value="ALL" Text="ALL STATUS"></asp:ListItem>
                            <asp:ListItem Value="N" Text="NEW"></asp:ListItem>
                            <asp:ListItem Value="AP" Text="PROCESSED To PROPOSAL"></asp:ListItem>
                            <asp:ListItem Value="W" Text="WAITING APPROVAL (PROPOSAL)"></asp:ListItem>
                            <asp:ListItem Value="AS" Text="PROCESSED To SCHEME"></asp:ListItem>
                            <asp:ListItem Value="A" Text="APPROVED"></asp:ListItem>
                            <asp:ListItem Value="R" Text="REJECTED"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th>
                        <asp:DropDownList ID="cbproduct" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbproduct_SelectedIndexChanged">
                        </asp:DropDownList>
                    </th>
                    <th>
                        <asp:DropDownList ID="cbsupervisor" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsupervisor_SelectedIndexChanged">
                        </asp:DropDownList>
                    </th>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <itemtemplate><a href="javascript:window.opener.SelectDiscount('<%#Eval("disc_cd") %>');window.close();"><%#Eval("disc_cd") %></a></itemtemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prop No">
                        <ItemTemplate><%#Eval("prop_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch">
                        <ItemTemplate><%#Eval("branch") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Promotion">
                        <ItemTemplate><%#Eval("promotion") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><%#Eval("rdpayment") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product SPV">
                        <ItemTemplate><%#Eval("product_spv") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product">
                        <ItemTemplate><%#Eval("product_list") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created Date">
                        <ItemTemplate><%#Eval("created_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate><%#Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate><%#Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate><%#Eval("customer") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget">
                        <ItemTemplate><%#Eval("budget") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%#Eval("status") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Noted">
                        <ItemTemplate><%#Eval("noted") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount No.">
                        <ItemTemplate><%#Eval("disc_new_cd") %></ItemTemplate>
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
