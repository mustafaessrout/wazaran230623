<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_bypasscl.aspx.cs" Inherits="admin_fm_bypasscl" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="../css/newbootstrap.css" rel="stylesheet" />
    <script src="../css/jquery-1.9.1.js"></script>

    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="container">
        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <h4 class="jajarangenjang">Customer for bypass checking CL & Overdue</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Customer</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True" ContextKey="true">
                    </asp:AutoCompleteExtender>
                </div>
                <label class="control-label col-md-1">End Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control-static"></asp:TextBox>
                    <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btsave" CssClass="btn btn-primary btn-lg" runat="server" OnClick="btsave_Click">ADD</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mygrid" Width="100%" OnRowDeleting="grd_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd")%>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name">
                                <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel">
                                <ItemTemplate><%# Eval("otlcd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group">
                                <ItemTemplate><%# Eval("cusgrcd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate><%# Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

