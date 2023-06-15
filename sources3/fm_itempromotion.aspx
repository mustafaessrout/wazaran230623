<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_itempromotion.aspx.cs" Inherits="fm_itempromotion" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Item Promotion By Branch</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <table class="mGrid">
                    <tr>
                        <th>Item Code</th>
                        <th>Cust Code</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txitemcode" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txitemcode_AutoCompleteExtender" runat="server" OnClientItemSelected="ItemSelected" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" ServiceMethod="GetItemList" TargetControlID="txitemcode">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txcustomer" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="dtstart_AutoCompleteExtender" runat="server" ServiceMethod="GetCustomerList" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" UseContextKey="true" OnClientItemSelected="CustSelected" TargetControlID="txcustomer">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="dtstart" CssClass="form-control calendar calendar-dropdown" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="dtend" CssClass="form-control calendar calendar-dropdown" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btadd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row margin-bottom" style="border-radius:30px">
                <asp:GridView ID="grd" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%#Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branded">
                            <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdcustcode" Value='<%#Eval("cust_cd") %>' runat="server" />
                                <%#Eval("cust_desc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate>
                                <asp:Label ID="lbstartdate" CssClass="control-label" runat="server" Text='<%#Eval("start_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                             <ItemTemplate>
                                <asp:Label ID="lbenddate" CssClass="control-label" runat="server" Text='<%#Eval("end_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <strong style="color:red">Item promotion will show only current or next promotion date</strong>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" style="display:none" />
</asp:Content>

