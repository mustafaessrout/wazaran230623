<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupreturnbs.aspx.cs" Inherits="lookupreturnbs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup</title>
    <script src="css/jquery-1.9.1.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <label class="control-label col-sm-2">Status</label>
                <div class="col-sm-2">
                    <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-sm-1">
                    <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
               <div class="col-sm-12">
                   <asp:GridView ID="grd" CssClass="table table-bordered table-condensed table-responsive" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                       <Columns>
                           <asp:TemplateField HeaderText="Sys No">
                               <ItemTemplate>
                                   <asp:Label ID="lbreturno" runat="server" Text='<%#Eval("retur_no") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Date">
                               <ItemTemplate>
                                   <%#Eval("retur_dt","{0:d/M/yyyy}") %>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Amount">
                               <ItemTemplate><%#Eval("totamt") %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Cust">
                               <ItemTemplate>
                                      <%#Eval("cust_desc") %>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:CommandField ShowSelectButton="True" />
                       </Columns>
                   </asp:GridView>
               </div>
            </div>
        </div>
    </form>
</body>
</html>
