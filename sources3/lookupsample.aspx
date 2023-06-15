<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupsample.aspx.cs" Inherits="lookupsample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Sample</title>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />

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
        <div class="container">
            <div class="row margin-top margin-bottom">
                <label class="col-sm-2 control-label input-sm">Search</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txsearch" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
                </div>
            </div>
            <div class="row margin-bottom margin-top">
                <div class="col-sm-12">
                    <asp:GridView ID="grd" CssClass="table table-striped mGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Sample No">
                                <ItemTemplate>
                                    <asp:Label ID="lbsamplecode" runat="server" Text='<%#Eval("sample_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%#Eval("sample_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate><%#Eval("reasn_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%#Eval("sample_sta_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
