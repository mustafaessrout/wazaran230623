<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookpayment.aspx.cs" Inherits="lookpayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />

    <script src="js/jquery.min.js"></script>
     <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="containers bg-white">
    
        <div class="divheader">Lookup Payment</div>
        <div class="h-divider"></div>

        <div class="clearfix margin-bottom">
            <div class="col-sm-6 no-padding">
                <label class="control-label col-md-2 col-sm-4 titik-dua">Payment Status </label> 
                <div class="col-md-10 col-sm-8 drop-down">
                    <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>        
        </div>
         
        <div class="divgrid">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-hover mygrid" Width="100%" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Payment No">
                        <ItemTemplate><a href="javascript:window.opener.RefreshData('<%# Eval("payment_no") %>');window.close();"> <%# Eval("payment_no") %></a></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date"><ItemTemplate><%# Eval("payment_dt","{0:d/M/yyyy}") %></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><%# Eval("payment_typ") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tab No">
                        <ItemTemplate><%# Eval("tab_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salesman">
                        <ItemTemplate><%# Eval("emp_desc") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
