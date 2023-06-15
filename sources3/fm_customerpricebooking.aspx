<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customerpricebooking.aspx.cs" Inherits="fm_customerpricebooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.min.js"></script>
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <%-- <link href="admin/css/bootstrap.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.js"></script>--%>

    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btprice.ClientID%>').click();
        }

        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Price Booking</div>
    <div class="h-divider"></div>

    <div class="container">

        <div class="row clearfix">
            <div class="clearfix">
                <div class="col-md-4 col-sm-6 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">Booking Date </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="dtbooking" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="dtbooking_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtbooking" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>

                <div class="col-md-4 col-sm-6 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">Start Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="dteff" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="dteff_CalendarExtender" CssClass="date" runat="server" TargetControlID="dteff" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">End Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" CssClass="date" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>

            <div class="clearfix">
                <label class="control-label col-sm-2 titik-dua">Customer Type</label>

                <div class="clearfix col-sm-10">
                    <table class="table table-striped mygrid no-margin">
                        <tr>
                            <th>Cust Type</th>
                            <th>Item</th>
                            <th>Curr Pricd</th>
                            <th>Chg Price</th>
                            <th>Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control input-sm" ID="cbcusttype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcusttype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:TextBox ID="txsearchitem" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" CompletionListElementID="divitem" OnClientItemSelected="ItemSelected">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hditem" runat="server" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbcurrent" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbcusttype" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txprice" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-warning btn-sm" OnClick="btadd_Click">Save</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <asp:GridView ID="grd" runat="server" CellPadding="0" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True" CssClass="table table-striped mygrid">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Type">
                                <ItemTemplate>
                                    <%# Eval("cust_typ") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effective Date">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdids" runat="server" Value='<%# Eval("IDS") %>' />
                                    <%# Eval("start_dt","{0:d/M/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <%# Eval("item_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="navi margin-bottom">
            <asp:Button ID="btprice" runat="server" OnClick="btprice_Click" Text="Button" CssClass="divhid" />
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>


</asp:Content>

