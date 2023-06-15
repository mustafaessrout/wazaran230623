<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_cashout.aspx.cs" Inherits="lookup_cashout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

  
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
   

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
  
   

    <!--custom js-->
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
  

 
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script>
        function CloseWindow(sval)
        {
            window.opener.RefreshData(sval);
            window.close();
        }
    </script>
</head>
<body>
    <form runat="server">
       <div class="containers bg-white">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="divheader">Cashout List</div>
            <div class="h-divider"></div>
            <div class="clearfix margin-bottom">
                <label class="col-md-2 col-sm-4 control-label titik-dua" style="margin-right:5px;">Cash Out Request Date</label> 
                <div class="col-md-4 col-sm-6 drop-down-date">
                    <asp:TextBox ID="dtcashout1" runat="server" CssClass="form-control"></asp:TextBox>
        
                    <asp:CalendarExtender ID="dtcashout1_CalendarExtender"  CssClass="date" runat="server" TargetControlID="dtcashout1" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                    
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                </div>
            </div>

            <div class="divgrid">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" CssClass="table table-striped mygrid">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Request No.">
                            <ItemTemplate>
                             <a href="javascript:CloseWindow('<%# Eval("casregout_cd")%>');"><%# Eval("casregout_cd") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("cashout_sta_nm") %></ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate><%# Eval("itemco_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("itemco_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt">
                            <ItemTemplate>
                                <%# Eval("amt","{0:f2}") %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                             <%# Eval("remark") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PIC">
                            <ItemTemplate>   <%# Eval("emp_nm") %></ItemTemplate>
                            <ItemTemplate>
                                <%# Eval("emp_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit"/>
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle   />
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
