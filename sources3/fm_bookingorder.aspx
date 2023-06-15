<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_bookingorder.aspx.cs" Inherits="fm_bookingorder" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }
    </script>
 	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <h4>Sales Order Approval&nbsp;&nbsp;&nbsp;<asp:Image ID="img" runat="server" Height="20px" ImageUrl="~/green.png" Width="100px" />
        </h4>
        <div class="h-divider"></div>
        <div class="form-horizontal">
            <div class="form-group">
                <label class="col-md-1">AmtTrx:</label>
                <div class="col-md-4 col-lg-offset-0">
                    <asp:TextBox ID="txamt" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                </div>
                <label class="col-md-1">Cust:</label>
                <div class="col-md-4 col-lg-offset-0">
                    <asp:TextBox ID="txcust" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                    <asp:AutoCompleteExtender OnClientItemSelected="CustSelected" ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true">
                    </asp:AutoCompleteExtender>
                    <asp:HiddenField ID="hdcust" runat="server" />
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
              <label class="col-md-1">CL:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:Label ID="lbcl" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <label class="col-md-1">Remain:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:Label ID="lbremaincl" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <label class="col-md-1">Type:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:Label ID="lbcredit" runat="server" CssClass="form-control"></asp:Label>
                </div>
                 <label class="col-md-1">Chnnl:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:Label ID="lbchannel" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" CssClass="mygrid" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True" Width="100%" OnRowDataBound="grd_RowDataBound" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Sys Inv No">
                                <ItemTemplate><%# Eval("inv_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inv No">
                                <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <%# Eval("inv_dt","{0:d/M/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate><%# Eval("due_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Amt">
                                <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance") %>'></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <h5><asp:Label ID="lbtotbalance" runat="server" style="font-weight:bold"></asp:Label></h5>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Silver" />
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-1">Option:</label>
                <div class="col-md-5 col-lg-offset-0">
                    <asp:RadioButtonList ID="rdpaid" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" CssClass="form-control" RepeatDirection="Horizontal" Width="100%">
                        <asp:ListItem Value="R">Real Payment</asp:ListItem>
                        <asp:ListItem Value="P">Promise Payment</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
              <div class="form-group">
                <label class="col-md-1">Paid DT:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:TextBox ID="dtpaid" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                </div>
                <label class="col-md-1">Py No:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:TextBox ID="dtpaymentno" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                </div>
                <label class="col-md-1">Pay Type:</label>
                <div class="col-md-2 col-lg-offset-0">
                    <asp:DropDownList ID="cbpaymenttype" runat="server" CssClass="form-control-static" Width="100%">
                        <asp:ListItem Value="CQ">Cheque</asp:ListItem>
                        <asp:ListItem Value="BT">Bank Transfer</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="navi">
            <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" />
        </div>
    </div>
</asp:Content>

