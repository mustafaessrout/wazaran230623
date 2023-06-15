<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_co.aspx.cs" Inherits="lookup_co" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>
</head>
<body >
    <form id="form1" runat="server">
        <div class="containers bg-white margin-bottom margin-top">
            <div class="clearfix margin-bottom margin-top">
                 <div class="col-sm-12 clearfix">
                     <label class="col-sm-4 control-label titik-dua">Canvas Order No.</label>
                     <div class="col-sm-8">
                         <asp:TextBox ID="txcanvasno" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                 </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-sm-12 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Status</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div>
                <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="20">
                    <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="Canvasser No.">
                            <ItemTemplate>
                               <a href="javascript:window.opener.senddata('<%# Eval("so_cd") %>');window.close();"> <%# Eval("so_cd")%></a> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No">
                            <ItemTemplate><%# Eval("ref_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# Eval("so_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust CD">
                            <ItemTemplate>
                                <%# Eval("cust_cd") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tab No">
                            <ItemTemplate><%# Eval("tabno") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <%# Eval("remark") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
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
