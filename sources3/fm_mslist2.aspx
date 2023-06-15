<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mslist2.aspx.cs" Inherits="fm_mslist2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }
    </script>
    <style>
        th {
            position: sticky;
            top: 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Item List</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="clearfix col-sm-6 form-group">
                <label class="col-sm-2 control-label">Search </label>
                <div class="col-sm-10">
                    <div class="input-group">
                        <asp:HiddenField ID="hditem" runat="server" />
                        <asp:TextBox ID="txsearch" runat="server" class="form-control" Style="border-radius: 5px 0 0 5px;"></asp:TextBox>
                        <div id="divwidth" class="auto-complate-list"></div>

                        <div class="input-group-btn">
                            <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>

                    </div>
                    <asp:AutoCompleteExtender ID="acextend" runat="server" TargetControlID="txsearch" ServiceMethod="GetItemList" CompletionSetCount="1"
                        MinimumPrefixLength="1" CompletionInterval="10" FirstRowSelected="false" EnableCaching="false" UseContextKey="True" OnClientItemSelected="ItemSelected" BehaviorID="autoComplete"
                        CompletionListElementID="divwidth">
                    </asp:AutoCompleteExtender>
                </div>
            </div>
        </div>
    </div>


    <div class="h-divider"></div>
    <div class="container">
        <div class="row alert alert-info">
            <div class="col-sm-12 overflow-y" style="max-height: 380px;">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False" AllowPaging="True" GridLines="None" OnPageIndexChanging="grditem_PageIndexChanging" OnRowCommand="grditem_RowCommand" OnSelectedIndexChanging="grditem_SelectedIndexChanging" CellPadding="0" CssClass="table table-hover table-striped mygrid">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arabic">
                                    <ItemTemplate><%# Eval("item_arabic") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbranded" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate><%# Eval("item_sta_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>

                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row navi padding-bottom">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>

</asp:Content>

