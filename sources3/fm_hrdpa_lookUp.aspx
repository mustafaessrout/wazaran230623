<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_hrdpa_lookUp.aspx.cs" Inherits="fm_hrdpa_lookUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sync Branch Target</title>
   
    <script src="css/jquery-1.12.4"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

</head>
<body>

    <form id="form1" runat="server">
        <div>
           
                   
            <div class="container">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
                        <div class="clearfix">
                            <div class="col-sm-4">
                                Branch Name
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlBranch" runat="server">

                                    <asp:ListItem Value="sp_syncsalestargetjeddah" Selected="True" Text="SBTC JEDDAH"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetmakkah" Text="SBTC MAKKAH"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetmadinah" Text="SBTC MADINAH"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargettaif" Text="SBTC TAIF"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetyanbu" Text="SBTC YANBU"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetriyadh" Text="SBTC RIYADH"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetkharaj" Text="SBTC KHARJ"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetqasheem" Text="SBTC GASIEM"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetdawadmi" Text="SBTC DAWADMI"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargethail" Text="SBTC HAIL"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetkhobar" Text="SBTC KHOBAR"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetjubail" Text="SBTC JUBAIL"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargethufuf" Text="SBTC HUFUF"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargethafrbatin" Text="SBTC HAFR BATIN"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetskaka" Text="SBTC SKAKA"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargettabuk" Text="SBTC TABUK"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetkhamis" Text="SBTC KHAMIS MUSHAIT"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetjizan" Text="SBTC JIZAN"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetnajran" Text="SBTC NAJRAN"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetbaha" Text="SBTC BAHA"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetqunfuda" Text="SBTC QONFUDA"></asp:ListItem>
                                    <asp:ListItem Value="sp_syncsalestargetbisha" Text="SBTC BISHA"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click">Sync Branch</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


        </div>
        <div>
        </div>
    </form>
</body>
</html>
