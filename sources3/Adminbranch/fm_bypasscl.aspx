<%@ Page Title="" Language="C#" MasterPageFile="~/adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_bypasscl.aspx.cs" Inherits="admin_fm_bypasscl" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <%-- <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="../css/newbootstrap.css" rel="stylesheet" />
    <script src="../css/jquery-1.9.1.js"></script>--%>

    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }
    </script>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <%--<div class="container">--%>
        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <h4 class="jajarangenjang">Customer for bypass checking CL & Overdue</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Customer</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>
                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True" ContextKey="true">
                    </asp:AutoCompleteExtender>
                </div>
                <label class="control-label col-md-1">End Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control-static"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-md-1">Remark</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txremark" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btsave" CssClass="btn btn-primary btn-lg" runat="server" OnClick="btsave_Click">ADD</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Credit Limit</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txcreditlimit" runat="server" CssClass="form-control"></asp:TextBox>                 
                </div>
                <label class="control-label col-md-1">Over Due</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txdue" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Balance</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txbalance" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">TOP</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtop" runat="server" CssClass="form-control"></asp:TextBox>                 
                </div>
                <label class="control-label col-md-1">
                    Last Order
                </label>
                <div class="col-md-2">
                    <asp:TextBox ID="txlstorder" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                     <div class="overflow-y" style="width:100%; max-height:360px;">

                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-fix mygrid" Width="100%" OnRowDeleting="grd_RowDeleting">
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
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
   <%-- </div>--%>
    <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" style="display:none" />
</asp:Content>

