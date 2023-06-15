<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupclaimcashoutho.aspx.cs" Inherits="fm_lookupclaimcashoutho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Claim</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div>
                Proposal No <asp:TextBox ID="txsearchprop" runat="server" Width="20em"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="btn btn-search" OnClick="btsearch_Click" />
            </div>
            <div>
                <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Claim No">
                            <ItemTemplate>
                                <a href="javascript:window.opener.ClaimSelected('<%# Eval("claimco_cd") %>');window.close();">
                                <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claimco_cd") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prop No">
                            <ItemTemplate><%# Eval("prop_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("paid_dt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt">
                            <ItemTemplate><%# Eval("amt","{0:F2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Received">
                            <ItemTemplate><%# Eval("receiver") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><%# Eval("claimco_sta_id").ToString() == "A" ? "Approved" : Eval("claimco_sta_id").ToString() == "N" ? "New" : "Rejected" %></ItemTemplate>
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
