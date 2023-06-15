<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_soa.aspx.cs" Inherits="fm_soa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
        function CustSelected(sender, e) {

            $get('<%=hdcust_cd.ClientID%>').value = e.get_value();
        }
    </script>
    <style type="text/css">
        .caret-off > i {
            display: none;
        }

        .dropdown-active + i {
            display: block;
        }

        .container-fluid {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid no-padding">
        <div class=" row col-md-12 clear-float">
            <div class="col-md-6 no-padding clearfix">
                <div class="divheader">Statment Of Account Report</div>

            </div>
            <div class="col-md-6 no-padding" style="height: 40px;">
                <div class="alert alert-danger" style="padding: 9px 15px;">
                    <span class="auto-style13">Different Between SOA and Outstanding is </span>
                    <asp:Label ID="lbl" runat="server" CssClass="auto-style13"></asp:Label>
                    <span class="auto-style13">&nbsp;SR.</span>
                </div>
            </div>
        </div>
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid top-devider">
        <div class="row col-md-8 col-md-offset-2">
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbbranch" runat="server" Text="Branch" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbtypofrep" runat="server" Text="Report Type" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-8 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbtypofrep" runat="server" CssClass="form-control">
                                <asp:ListItem Value="SOA1">STATMENT OF ACCOUNT 1</asp:ListItem>
                                <asp:ListItem Value="SOA2">STATMENT OF ACCOUNT 2</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="chsummary" Text="DETAILS" runat="server" AutoPostBack="True" OnCheckedChanged="chsummary_CheckedChanged" />



                </div>

            </div>
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbstartdt" runat="server" Text="Start Date" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                    </ajaxToolkit:CalendarExtender>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbenddate" runat="server" Text="End Date" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtend_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtend_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                    </ajaxToolkit:CalendarExtender>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <asp:Label ID="lbrepby" runat="server" Text="Report By" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbrepby" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbrepby_SelectedIndexChanged">
                        <asp:ListItem Value="0">Branch</asp:ListItem>
                        <asp:ListItem Value="1">Salesman</asp:ListItem>
                        <asp:ListItem Value="2">Customer</asp:ListItem>
                        <asp:ListItem Value="3">Customer Group</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix ">
                <asp:Label ID="lbcusgrcd" runat="server" Text="Report By" CssClass="control-label col-sm-2"></asp:Label>

                <asp:Panel ID="Panel5" runat="server" class="col-sm-10 drop-down">

                    <asp:DropDownList ID="cbcusgr" runat="server" CssClass="form-control dropdown-active margin-bottom">
                    </asp:DropDownList>

                </asp:Panel>

            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom">
                        <asp:Label ID="lbsalesman" runat="server" Text="Salesman" CssClass="control-label col-sm-2"></asp:Label>
                        <div class="col-sm-7">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txsalesmanPnl">
                                        <asp:TextBox ID="txsalesman" runat="server" CssClass="ro form-control"></asp:TextBox>
                                    </asp:Panel>
                                    <div id="divwidth" class="auto-text-content"></div>
                                    <asp:HiddenField ID="hdsalesman_cd" runat="server" />
                                    <ajaxToolkit:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True"
                                        CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3 ">
                            <asp:Button ID="btadd" runat="server" CssClass="btn-success btn btn-add sm-padding-top" OnClick="btadd_Click" Text="ADD" Style="width: 100%; min-width: 0;" />
                        </div>
                    </div>
                    <div class="clearfix ">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdsl" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdsl_RowDeleting" GridLines="None" CssClass="margin-bottom margin-top table table-hover mygrid">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salesman Name">
                                            <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom">
                        <asp:Label ID="lbcust" runat="server" Text="Customer" CssClass="col-sm-2 control-label "></asp:Label>
                        <div class="col-sm-7">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txcustPnl">
                                        <asp:TextBox ID="txcust" runat="server" CssClass="ro form-control"></asp:TextBox>
                                    </asp:Panel>
                                    <div id="divwidth1" class="auto-text-content"></div>
                                    <asp:HiddenField ID="hdcust_cd" runat="server" />
                                    <ajaxToolkit:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" CompletionSetCount="1" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetCompletionList1" UseContextKey="True" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidth1">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btaddcust" runat="server" CssClass="btn-success btn btn-add full" Text="ADD" OnClick="btaddcust_Click" />
                        </div>
                    </div>

                    <div class="clearfix">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdcust_RowDeleting" GridLines="None" CssClass="table table-hover mygrid margin-bottom">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="row padding-top margin-top col-md-8 col-md-offset-2">
            <asp:GridView ID="grddiff" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdcust_RowDeleting" GridLines="None" CssClass="table table-hover mygrid">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="cust_cd" HeaderText="Code" />
                    <asp:BoundField DataField="cust_nm" HeaderText="Customer Name" />
                    <asp:BoundField DataField="soab" HeaderText="SOA" />
                    <asp:BoundField DataField="balance" HeaderText="Outsatnding" />
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
        <div class="navi row padding-top margin-bottom padding-bottom col-md-12">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            <asp:Button ID="btdiff" runat="server" CssClass="btn-primary btn btn-different" OnClick="btdiff_Click" Text="Different" />
        </div>
    </div>

</asp:Content>

