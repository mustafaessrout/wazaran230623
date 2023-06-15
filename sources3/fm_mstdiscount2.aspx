<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstdiscount2.aspx.cs" Inherits="fm_mstdiscount2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/anekabutton.css" rel="stylesheet" />

    <script>
        function RefreshData() {
            $get('<%=btrefresh.ClientID%>').click();
            //sweetAlert('Activated', '', 'success');
            return (false);
        }
    </script>
    <script>
        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=600");
            mywindow.moveTo(400, 200);
        }

        function DiscSelected(sender, e) {
            $get('<%=hddisc.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();

        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        //Stop Form Submission of Enter Key Press
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
    <script>

</script>
    <style>
        .auto-complate-list {
            max-height: 150px !important;
            min-width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hddisc" runat="server" />
    <%--  <div class="divheader">Discount Scheme Information</div>
    <div class="h-divider"></div>--%>
    <div class="alert alert-info text-bold">Discount Scheme Creation</div>
    <div class="container">

        <div class="row ">
            <div class="clearfix margin-bottom">
                <div class="col-sm-4 clearfix">
                    <label class="control-label col-sm-4 titik-dua">Search By Status</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-4 clearfix">
                    <div class="well well-sm no-margin-bottom no-margin-top checkbox">
                        <asp:CheckBox ID="chall" runat="server" CssClass="" OnCheckedChanged="chall_CheckedChanged" AutoPostBack="True" Text="All Salespoint" />
                    </div>
                </div>

                <div class="col-sm-4 clearfix">
                    <label class="control-label col-sm-4 titik-dua">Disc No</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="txsearch" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="divdis" OnClientItemSelected="DiscSelected">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>
        </div>

        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="table-page-fixer">
                        <div class="overflow-y relative" style="max-height: 250px;">
                            <asp:GridView ID="grddisc" runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss" AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanging="grddisc_SelectedIndexChanging" OnRowDeleting="grddisc_RowDeleting" OnRowEditing="grddisc_RowEditing" OnPageIndexChanging="grddisc_PageIndexChanging" OnRowDataBound="grddisc_RowDataBound" CellPadding="0" GridLines="None" PageSize="50">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Discount Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <div class="ellapsis2">
                                                <a href="javascript:openwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd") %>');"><%# Eval("remark") %></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdelivery_dt" runat="server" Text='<%# Eval("delivery_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbstart_dt" runat="server" Text='<%# Eval("start_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expired Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbend_dt" runat="server" Text='<%# Eval("end_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prop No">
                                        <ItemTemplate><%# Eval("proposal_no") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" InsertVisible="False">
                                        <ItemTemplate><strong><%# Eval("disc_sta_nm") %></strong></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <%--<asp:TemplateField HeaderText="sta_upd">--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lbsta_upd" runat="server" Text='<%# Eval("sta_upd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:CommandField HeaderText="ACTION" DeleteText="Deactivated" ShowDeleteButton="True" ShowEditButton="True" />--%>
                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                    <asp:CommandField HeaderText="Action" DeleteText="Deactivated" ShowDeleteButton="True" />
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
                        </div>
                    </div>
                    <div class="table-copy-page-fix" id="ss"></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbstatus" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" Style="display: none" />
        </div>

        <div class="navi margin-top">
            <asp:Button ID="btsearch" runat="server" OnClick="btsearch_Click" Text="Button" CssClass="divhid" />
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn btn-success btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>

    <div id="divdis" style="font-size: small; font-family: Calibri">
    </div>
</asp:Content>

