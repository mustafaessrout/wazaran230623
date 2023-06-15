<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_cashregoutall.aspx.cs" Inherits="fm_cashregoutall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List</title>
    <script src="admin/js/bootstrap.js"></script>
    <link href="admin/css/bootstrap.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="container">
        <div class="form-horizontal">
            <h3>List Of Cashregister Available</h3>
            <div class="form-group">
                <div class="col-md-12">

                    <asp:GridView ID="grd" runat="server" CellPadding="0" CssClass="mygrid" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Cash Code">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdcashid" runat="server" Value='<%# Eval("cash_id") %>' />
                                    <%# Eval("itemco_cd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cashout Nm">
                                <ItemTemplate><%# Eval("itemco_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%# Eval("cash_dt","{0:d/M//yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt">
                                <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="App By">
                                <ItemTemplate><%# Eval("app_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NR">
                                <ItemTemplate><%# Eval("nr") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref No">
                                <ItemTemplate><%# Eval("ref_no") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="navi">
                          <asp:Button ID="btsave" runat="server" CssClass="btn-default" OnClick="btsave_Click" Text="RECEIPT ALL" />
              </div>
                          </div>
            </div>
           
              
            
        </div>
    </div>
    </form>
</body>
</html>
