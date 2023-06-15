<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_merchandisercustomer.aspx.cs" Inherits="fm_merchandisercustomer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function MerchandiserSelected(sender, e) {
            $get('<%=hdmerchandiser.ClientID%>').value = e.get_value();
            $('#<%=txmerchandiser.ClientID%>').addClass("ro");
            $get('<%=btsearch.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdmerchandiser" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Merchandiser Customer Visit List</h4>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <label  class="col-md-1 control-label">Merchandiser</label>
            <div class="col-md-3">
                <asp:TextBox ID="txmerchandiser" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txmerchandiser_AutoCompleteExtender" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" ServiceMethod="GetMerchandiser" OnClientItemSelected="MerchandiserSelected" runat="server" TargetControlID="txmerchandiser">
                </asp:AutoCompleteExtender>
            </div>
            <label class="col-md-1 control-label">Customer</label>
            <div class="col-md-3">
                <asp:TextBox ID="txcustomer" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" TargetControlID="txcustomer" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" ServiceMethod="GetCustomer" OnClientItemSelected="CustSelected">
                </asp:AutoCompleteExtender>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click">ADD</asp:LinkButton>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-md-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcustcode" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate><%#Eval("otlcd_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group">
                            <ItemTemplate><%#Eval("cusgrcd_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row center">
            <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
            <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" style="display:none" />
        </div>
    </div>
</asp:Content>

