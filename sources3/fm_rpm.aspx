<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rpm.aspx.cs" Inherits="fm_rpm" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function MerchandiserSelected(sender, e) {
            $get('<%=hdmerchandiser.ClientID%>').value = e.get_value();
            $('#<%=txmerchandiser.ClientID%>').addClass("form-no ro");
            $get('<%=btsearch.ClientID%>').click();
        }

        function CustSelected(sender, e) {
             $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdmerchandiser" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Route Plan Merchandiser</h4>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <label class="control-label col-md-1">Merchandiser</label>
            <div class="col-md-2">
                <asp:TextBox ID="txmerchandiser" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txmerchandiser_AutoCompleteExtender" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" UseContextKey="true" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="MerchandiserSelected" ServiceMethod="GetMerchandiser" runat="server" TargetControlID="txmerchandiser">
                </asp:AutoCompleteExtender>
            </div>
             <label class="control-label col-md-1">Day</label>
            <div class="col-md-2 drop-down">
                <asp:DropDownList ID="cbday" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbday_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Sequence</label>
            <div class="col-md-1 drop-down">
                <asp:DropDownList ID="cbsequence" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Customer</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcustomer" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" UseContextKey="true" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="CustSelected" ServiceMethod="GetCustomer" runat="server" TargetControlID="txcustomer">
                </asp:AutoCompleteExtender>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btadd" CssClass="btn btn-success" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <ini style="color:red">Day 1=Sunday, Day 2=Monday, Day 3=Tuesday, Day 4=Wednesday, Day 5=Thursday, Day 6=Friday, Day 7=Saturday</ini>
        </div>
        <div class="row margin-bottom">
            <div class="col-md-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Sequence">
                            <ItemTemplate>
                                <asp:Label ID="lbsequence" runat="server" Text='<%#Eval("sequenceno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day">
                            <ItemTemplate>
                                 <asp:Label ID="lbdaycode" runat="server" Text='<%#Eval("day_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdcustomer" Value='<%#Eval("cust_cd") %>' runat="server" />
                                 <asp:Label ID="lbcustomer" runat="server" Text='<%#Eval("cust_desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom center">
            <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
            <asp:Button ID="btsearch" runat="server" OnClick="btsearch_Click" Text="Button" style="display:none" />
        </div>
    </div>
</asp:Content>

