<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookproposal2.aspx.cs" Inherits="lookproposal2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup CN</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
  

</head>
<body>
    <form id="form1" runat="server">
    <div class="containers bg-white">
        <div class="divheader">Search Proposal</div>
        <div class="h-divider"></div>
        <div class="container">
            
            <div>
                Proposal No <asp:TextBox ID="txsearchprop" runat="server" Width="20em"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="btn btn-search" OnClick="btsearch_Click" />
            </div>
            <br/>
            <div class="col-md-12"> 
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" OnSelectedIndexChanging="grd_SelectedIndexChanging" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Prop No">
                            <ItemTemplate>
                                <asp:Label ID="lbpropno" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("prop_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="budget">
                            <ItemTemplate><%# Eval("budget_limit") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dest">
                            <ItemTemplate><%# Eval("rdcust") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
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
    </div>
    </form>
</body>
</html>
