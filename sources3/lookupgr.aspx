<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupgr.aspx.cs" Inherits="lookupgr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Good Received</title>
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
            <div class="row margin-bottom margin-top">
                 <label class="col-sm-2 control-label input-sm">Period</label>
                <div class="col-sm-3 drop-down">
                    <asp:DropDownList ID="cbperiod" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="col-sm-2 control-label input-sm">Receipt No</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txsearch" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
                </div>
                
            </div>
        </div>
        <div>
            <asp:GridView ID="grd" CssClass="table table-striped" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Receipt No">
                        <ItemTemplate>
                            <asp:Label ID="lbreceiptno" runat="server" Text='<%#Eval("receipt_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%#Eval("receipt_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type Of Stockin">
                        <ItemTemplate>
                            <%#Eval("typeofstockin_nm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GDN Nav No.">
                        <ItemTemplate><%#Eval("do_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
