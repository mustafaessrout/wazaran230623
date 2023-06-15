<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockoutsample.aspx.cs" Inherits="fm_stockoutsample" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/lightbox.min.js"></script>

    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btitem.ClientID%>').click();
        }

        function EmployeeSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
            $get('<%=btemp.ClientID%>').click();
        }

        function CustSelected(sender, e) {

            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btcust.ClientID%>').click();
        }

        function SelectData(x) {
            $get('<%=hdsample.ClientID%>').value = x;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdemp" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdsample" runat="server" />
    <div class="alert alert-info text-bold">Stock Out For Sample Ver.1.0</div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Sample No.</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <%--<asp:Label ID="lbsampleno" runat="server" Text="" CssClass="control-label form-control input-group-sm"></asp:Label>--%>
                    <asp:TextBox ID="lbsampleno" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="popupwindow('lookupsample.aspx');" runat="server" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>

            </div>

            <label class="control-label input-sm col-sm-1">Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtsample" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtsample_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtsample">
                </asp:CalendarExtender>
            </div>
            <label class="control-label input-sm col-sm-1">PIC</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txpic" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txpic_AutoCompleteExtender" runat="server" TargetControlID="txpic" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" FirstRowSelected="false" EnableCaching="false" ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetEmployeeList" OnClientItemSelected="EmployeeSelected">
                </asp:AutoCompleteExtender>
            </div>
            <label class="control-label input-sm col-sm-1">Ref No</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txrefno" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>

        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">Purpose</label>
            <div class="col-sm-5 require">
                <asp:TextBox ID="txsamplename" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label input-sm col-sm-1">For Cust</label>
            <div class="col-sm-5">
                <table class="table table-bordered table-condensed table-hover input-sm">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chcust" Text="For Customer" CssClass="form-control input-sm" runat="server" AutoPostBack="true" onchange="ShowProgress();" OnCheckedChanged="chcust_CheckedChanged" />
                        </td>
                        <td class="require">
                            <asp:TextBox ID="txcustomer" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" TargetControlID="txcustomer" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" FirstRowSelected="false" EnableCaching="false" UseContextKey="true" OnClientItemSelected="CustSelected" ServiceMethod="GetCustList">
                            </asp:AutoCompleteExtender>
                            <asp:TextBox ID="txother" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">Reason</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbreason" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Sample Out By</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbsampleoutby" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <table class="table table-condensed table-hover table-bordered input-sm">
                    <tr>
                        <th style="width: 30%">Item</th>
                        <th style="width: 10%">Warehouse</th>
                        <th style="width: 10%">Bin</th>
                        <th>UOM</th>
                        <th>Stock AVL</th>
                        <th>Qty</th>
                        <th>ADD</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txitem" CssClass="form-control input-group-sm input-sm" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" UseContextKey="true" OnClientItemSelected="ItemSelected" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetItemList" runat="server" TargetControlID="txitem">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbwhs" AutoPostBack="true" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server"></asp:DropDownList></td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbbin" AutoPostBack="true" onchange="ShowProgress();" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" runat="server" CssClass="form-control input-sm"></asp:DropDownList></td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbuom" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbuom_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbstockavl" CssClass="form-control input-sm" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txqty" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:LinkButton ID="btadd" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btadd_Click">ADD</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-bordered table-condensed table-hover input-sm" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
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
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock Avl">
                            <ItemTemplate>
                                <asp:Label ID="lbstockavl" runat="server" Text='<%#Eval("stockavl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
    <div class="row margin-bottom margin-top">
        <div class="col-sm-12" style="text-align: center">
            <asp:LinkButton ID="btnew" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnew_Click">New</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btsave" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btprint" CssClass="btn btn-success btn-sm" runat="server" OnClientClick="ShowProgress();" OnClick="btprint_Click">Print</asp:LinkButton>&nbsp;
                <asp:Button ID="btitem" runat="server" OnClientClick="ShowProgress();" Style="display: none" OnClick="btitem_Click" Text="Button" />
            <asp:Button ID="btemp" runat="server" OnClick="btemp_Click" OnClientClick="ShowProgress();" Style="display: none" Text="Button" />
            <asp:Button ID="btcust" runat="server" OnClick="btcust_Click" Text="Button" Style="display: none" OnClientClick="ShowProgress();" />
            <asp:Button ID="btlookup" Style="display: none" runat="server" OnClick="btlookup_Click" Text="Button" />
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

