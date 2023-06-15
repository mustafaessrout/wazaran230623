<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupcndndirect.aspx.cs" Inherits="lookupcndndirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>lookup cndn</title>
     <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="form-group">
               <label class="col-xs-6 control-label">Status</label>
                <div class="col-xs-6 drop-down">
                    <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="grd" CssClass="table table-condensed table-bordered table-responsive" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="CNDN No">
                                <ItemTemplate>
                                    <asp:Label ID="lbcndnno" runat="server" Text='<%#Eval("cndn_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%#Eval("cndn_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt">
                                <ItemTemplate><%#Eval("totamt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp">
                                <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust">
                                <ItemTemplate><%#Eval("cust_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" EditText="Detail" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
