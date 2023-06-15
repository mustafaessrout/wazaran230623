<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockopnameentry_uom.aspx.cs" Inherits="fm_stockopnameentry_uom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .divmsg {
            /*position:static;*/
            top: 30%;
            right: 50%;
            left: 50%;
            width: 200px;
            height: 200px;
            position: fixed;
            /*background-color:greenyellow;*/
            overflow-y: auto;
        }

        .divhid {
            display: none;
        }

        .divnormal {
            display: normal;
        }

        @media (min-width: 768px) {
            .form-custom {
                padding-left: 5% !important;
                width: 91.65% !important;
            }
        }
    </style>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_stockOpname.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function ItemSelecteditem_cd(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
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
    <div class="alert alert-info text-bold">Stock Opname</div>
    <div id="container">
        <div class="row margin-bottom">
            <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-3 control-label">Stk Opnm No.</label>
                <div class="col-sm-9">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txstockno" runat="server" CssClass="makeitreadonly ro form-control"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" OnClick="btsearch_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" Style="display: none" />
                </div>
            </div>

            <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-3 control-label">Warehouse Type</label>
                <div class="col-sm-9 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhstype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbwhstype_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbwhstype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>

            <div class="col-sm-6 clearfix margin-bottom">
                <label class="control-label col-sm-3">Date</label>
                <div class="col-sm-9 ">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtstocktopname" runat="server" CssClass="makeitreadonly ro form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtstocktopname_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstocktopname">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-sm-6 clearfix margin-bottom">
                <label class="control-label col-sm-3">Warehouse / Van</label>
                <div class="col-sm-9 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>

            <div class="col-sm-12 clearfix margin-bottom">
                <label class="control-label col-sm-1">Bin</label>
                <div class="col-sm-10 form-custom">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="drop-down">
                                <asp:DropDownList ID="cbbinb" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="text-center margin-top">
                                <asp:Button ID="btgenerate" runat="server" OnClientClick="ShowProgress();" CssClass="btn-success btn btn-add" OnClick="btgenerate_Click" Text="Show Item" Visible="true" />
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>



        <div class="row " runat="server" id="vItem" style="display: none">
            <div class="h-divider"></div>
            <table class="table table-striped mygrid row-no-padding margin-bottom">
                <tr>
                    <th>Item Name</th>
                    <th>Location</th>
                    <th>Qty System</th>
                    <th>Qty Actual</th>
                    <th style="width: 100px;">OUM</th>
                    <th>Expire Date</th>
                    <th>Bin</th>
                    <th>Reason</th>
                    <th>Action</th>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txitem_cdPnl">
                                    <asp:TextBox ID="txitem_cd" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txitem_cd_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitem_cd_AutoCompleteExtender" runat="server" TargetControlID="txitem_cd" ServiceMethod="GetCompletionListitem_cd" MinimumPrefixLength="1"
                                        EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelecteditem_cd" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                    <asp:HiddenField ID="hditem" runat="server" />
                                </asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>

                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txlocation" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txqty_system" runat="server" CssClass="form-control input-sm ro makeitreadonly" Enabled="False"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txqty_actualPnl">
                                    <asp:TextBox ID="txqty_actual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="cbUOMPnl">
                                    <asp:DropDownList ID="cbUOM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbUOM_SelectedIndexChanged" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txexpire_dtPnl">
                                    <asp:TextBox ID="txexpire_dt" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txexpire_dt_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="txexpire_dt_CalendarExtender" PopupPosition="TopLeft" CssClass="date" runat="server" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TargetControlID="txexpire_dt" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbin" runat="server" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" CssClass="form-control input-sm" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txreason" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btadd" runat="server" CssClass="btn-sm btn-success btn btn-add" OnClick="btadd_Click" Text="Add" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>

        <div class="h-divider"></div>

        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="center">
                <ContentTemplate>
                    <div class=" overflow-y" style="max-height: 300px; width: 1000px;">
                        <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-fix table-hover mygrid " AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnPageIndexChanging="grd_PageIndexChanging" DataKeyNames="seqID" OnRowDeleting="grd_RowDeleting" OnRowCancelingEdit="grd_RowCancelingEdit" CellPadding="0" GridLines="None" OnRowDataBound="grd_RowDataBound">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="System Stock">
                                    <ItemTemplate><%# Eval("qty_system") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate><%# Eval("location") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtlocation" runat="server" Text='<%# Eval("location") %>' CssClass="form-control input-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Stock">
                                    <ItemTemplate><%# Eval("qty_actual") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtqtyactual" runat="server" Text='<%# Eval("qty_actual") %>' CssClass="form-control input-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OUM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboUOM" runat="server" Width="90px" CssClass="form-control input-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expire Date">
                                    <ItemTemplate><%# Eval("expire_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtexpire_dt" runat="server" Text='<%# Eval("expire_dt","{0:d/M/yyyy}") %>' Width="90px" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtstocktopname_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="txtexpire_dt" PopupPosition="TopLeft">
                                        </asp:CalendarExtender>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bin CD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbin_cd" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cbobincd" runat="server" Width="90px" CssClass="form-control input-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate><%# Eval("reason") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtreason" runat="server" Text='<%# Eval("reason") %>' Width="90px" CssClass="form-control input-sm"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbseqID" runat="server" Text='<%# Eval("seqID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
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

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="listItem" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:GridView ID="grditem" CssClass="mGrid" runat="server" AutoGenerateColumns="False" ShowFooter="False" CellPadding="0" OnRowDataBound="grditem_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitemcd" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                                |
                                                <asp:Label ID="lbitemnm" runat="server" Text='<%#Eval("item_shortname") %>'></asp:Label>
                                                |
                                                <asp:Label ID="lbitembranded" runat="server" Text='<%#Eval("branded_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty System">
                                            <ItemTemplate>
                                                <asp:Label ID="lbqtysys" runat="server" Text='<%#Eval("qty_system_conv") %>'></asp:Label>
                                                <asp:HiddenField ID="hdqtysys" runat="server" Value='<%#Eval("qty_system") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty Actual">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdqtyactual" runat="server" Value='<%#Eval("qty_actual") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdqtyactual_conv" runat="server" Value='<%#Eval("qty_actual_conv") %>'></asp:HiddenField>
                                                <asp:TextBox ID="txqty_ctn" Text='<%#Eval("qty_actual_ctn") %>' runat="server" TextMode="Number"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="cbuom_ctn"></asp:DropDownList>
                                                <asp:TextBox ID="txqty_pcs" Text='<%#Eval("qty_actual_pcs") %>' runat="server" TextMode="Number"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="cbuom_pcs"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="cbuom" ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bin">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="cbbin"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txexpiredt" runat="server" Text='<%# Eval("expire_dt","{0:d/M/yyyy}") %>' CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:CalendarExtender ID="txexpiredt_CalendarExtender" PopupPosition="TopLeft" CssClass="date" runat="server" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TargetControlID="txexpiredt" TodaysDateFormat="d/M/yyyy">
                                                </asp:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txlocation" Text='<%#Eval("location") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txreason" Text='<%#Eval("reason") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btgenerate" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <div class="h-divider"></div>
        <div class="navi margin-bottom margin-top padding-bottom">
            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
                    <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
                    <asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btDelete_Click" Text="Delete" />
                    <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
                    <asp:Button ID="btautoadj" runat="server" CssClass="btn-primary btn" OnClick="btautoadj_Click" Text="Auto Adj" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btgenerate" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


        <div class="divmsg loading-cont" id="dvshow">
            <div>
                <i class="fa fa-spinner spiner fa-spin fa-3x fa-fw" aria-hidden="true"></i>
            </div>
        </div>

    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

