<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_salestargetho2_lookUp.aspx.cs" Inherits="fm_salestargetho2_lookUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sync Branch Target</title>
    <%--    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>

    <script src="css/jquery-1.12.4"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

</head>
<body>

    <form id="form1" runat="server">




        <div class="form-horizontal">
            <div class="container" style="padding: 15px 0;">
                <h4 >Salesman Target Synchronization</h4>
            </div> 
            <div class="h-divider"></div>
            <div class="container">
                <div class="form-group">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label class="control-label">Last closing period</label>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLastMonthClosing" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class=" col-md-2">
                            <label class="control-label">User Selected Period</label>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblUserSelectedPeriod" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class=" col-md-2">
                            <label class="control-label">Branch</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">

                                <asp:ListItem Value="sp_syncsalestargetjeddah|101" Selected="True" Text="SBTC JEDDAH"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetmakkah|105" Text="SBTC MAKKAH"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetmadinah|108" Text="SBTC MADINAH"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargettaif|112" Text="SBTC TAIF"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetyanbu|114" Text="SBTC YANBU"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetriyadh|201" Text="SBTC RIYADH"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetkharaj|202" Text="SBTC KHARJ"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetqasheem|204" Text="SBTC GASIEM"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetdawadmi|208" Text="SBTC DAWADMI"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargethail|209" Text="SBTC HAIL"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetkhobar|301" Text="SBTC KHOBAR"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetjubail|302" Text="SBTC JUBAIL"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargethufuf|306" Text="SBTC HUFUF"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargethafrbatin|307" Text="SBTC HAFR BATIN"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetskaka|401" Text="SBTC SKAKA"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargettabuk|407" Text="SBTC TABUK"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetkhamis|501" Text="SBTC KHAMIS MUSHAIT"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetjizan|507" Text="SBTC JIZAN"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetnajran|511" Text="SBTC NAJRAN"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetbaha" Text="SBTC BAHA"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetqunfuda|517" Text="SBTC QONFUDA"></asp:ListItem>
                                <asp:ListItem Value="sp_syncsalestargetbisha|526" Text="SBTC BISHA"></asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>
        </div>



        <div class="navi">

            <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click">Sync Branch</asp:LinkButton>
        </div>

    </form>
</body>
</html>
