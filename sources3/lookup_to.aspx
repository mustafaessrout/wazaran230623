<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_to.aspx.cs" Inherits="lookup_to" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
     <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    
    <%--script--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/index.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/jquery.floatThead.js"></script>



    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width:200px;
       height:200px;
       position:fixed;
       opacity:0.9;
       overflow-y:auto;
         /*-webkit-transition: background-color 0;
    transition: background-color 0;*/
  }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
    
    </style> 
  
</head>
<body>
    <form id="form1" runat="server" class="container-fluid">
        <div class="row ">
            <div class=" containers bg-white">

                  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        
                    <div class="divheader margin-top">Take Order List</div>
        
                    <div class="h-devider"></div>
                    <div class="clearfix">
                        <div class="form-group clearfix col-md-4 col-sm-6">
                            <label for="cbstatus" class="control-label pull-left">Status Take Order :</label> 
                            <div class="drop-down pull-left" style="padding-left:10px; width:calc(100% - 120px); min-width:150px;">
                                <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group clearfix col-md-4 col-md-offset-4 col-sm-6">
                            <div class=" pull-left">
                                <div class="input-group">

                                    <asp:TextBox runat="server" ID="searchbox" CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:Button runat="server" ID="searchboxbtn" OnClick="searchboxbtn_Click" text="Search" CssClass="btn btn-primary btn-search"/>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
     
                    <div class="divgrid overflow-x" style="height:500px;">
                      
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None"  AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="15" CssClass="table table-fix table-striped mygrid">
                            <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Lnd No">
                                    <ItemTemplate>
                                        <%# Eval("ldn_seq") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TO No.">
                                    <ItemTemplate>
                                      <asp:Label ID="lbtono" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manual No"><ItemTemplate><%# Eval("ref_no") %></ItemTemplate></asp:TemplateField>
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
                                    <ItemTemplate><%# Eval("emp_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tab No">
                                    <ItemTemplate><%# Eval("tabno") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemTemplate><%# Eval("app_sta_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="action">
                                    <ItemTemplate> <a class="btn no-padding-top btn-block btn-link" href="javascript:window.opener.RefreshData('<%# Eval("so_cd") %>');window.close();">Select</a> </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page"/>
                            <RowStyle />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>


            </div>
        </div>
      

    </form>
</body>
</html>
