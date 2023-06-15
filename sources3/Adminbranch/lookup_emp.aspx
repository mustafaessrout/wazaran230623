<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_emp.aspx.cs" Inherits="lookup_emp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Employee</title>
  <%--  <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>--%>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body style="font-family:Calibri,Tahoma,Verdana">
    <form id="form1" runat="server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Search Employee</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-2">Salespoint</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbsp" CssClass="form-control-static" runat="server"></asp:DropDownList>
                </div>
                <label class="control-label col-md-2">Emp ID/Name</label>
                <div class="col-md-5">
                    <div class="input-group">
                        <asp:TextBox ID="txsearch" CssClass="form-control" Width="100%" Height="100%" runat="server"></asp:TextBox>
                        <span class="input-group-btn">
                            <button type="submit" class="btn btn-primary" runat="server" id="btsearch" onserverclick="btsearch_Click">
                               <i class="glyphicon glyphicon-search" aria-hidden="true">Click</i>
                            </button>
                        </span>
                    </div>
                    </div>
                    
                </div>
            <div class="form-group">
                <div class="col-md-12">
                     <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mygrid" CellPadding="2" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate><%# Eval("emp_cd") %>
                             </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><a href="javascript:window.opener.refreshdata('<%# Eval("emp_cd")%>');window.close();"><%# Eval("emp_nm") %></a></ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate><%# Eval("dept_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level">
                        <ItemTemplate><%# Eval("levelName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Title">
                        <ItemTemplate><%# Eval("job_title_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nationality">
                        <ItemTemplate><%# Eval("nationality_nm") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                </div>
            </div>
            </div>
        </div>
   
      </form>
</body>
</html>
