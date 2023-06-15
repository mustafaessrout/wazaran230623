<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockindirect.aspx.cs" Inherits="fm_stockindirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/lightbox.min.js"></script>--%>
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
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdunitprice" runat="server" />
    <div class="alert alert-bullet text-bold">Stock In Direct To Warehouse</div>
    <div class="container block-shadow-info">
        <div class="form-group">
            <div class="row margin-bottom margin-top">
                <label class="control-label-sm col-sm-1 input-sm">Stock In Type</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbstockinstatus" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstockinstatus_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-sm-1 input-sm">Salespoint</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbsalespoint" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
                <label class="control-label col-sm-1 input-sm">From PO No.</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txpono" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-sm-1 input-sm">GDN No.</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbgdn" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgdn_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>

            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Stockin No.</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txstockinno" CssClass="form-control ro input-group-sm input-sm" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" OnClientClick="javascript:popupwindow('lookupstockindirect.aspx');" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label col-sm-1 input-sm">Date</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="dtstockin" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtstockin_calendar" Format="d/M/yyyy" runat="server" TargetControlID="dtstockin"></asp:CalendarExtender>
                </div>
                <label class="col-sm-1 control-label input-sm">Reference No</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txreference" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-sm-1 input-sm">Supplier</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbsupplier" CssClass="form-control input-sm" onchange="ShowProgress();" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsupplier_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="lbaddress" runat="server" Text="" CssClass="control-label input-sm"></asp:Label>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Remark</label>
                <div class="col-sm-11 require">
                    <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grdnavision" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="GDN No">
                            <ItemTemplate>
                                <%#Eval("DO_No") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code Nav">
                            <ItemTemplate><%#Eval("DO_Item_1") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code Waz">
                            <ItemTemplate><%#Eval("DO_Item_2") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <%#Eval("DO_Qty") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate><%#Eval("DO_Uom") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
            <div class="row margin-bottom">
                <div class="col-sm-12">
                    <table class="mGrid">
                        <tr>
                            <th style="width: 40%">Item</th>
                            <th style="width: 5%">Qty </th>
                            <th style="width: 5%">UOM</th>

                            <th style="width: 5%">To Warehouse</th>
                            <th style="width: 5%">To Bin</th>
                            <th style="width: 10%">Exp Date</th>
                            <th style="width: 10%">Prod Date</th>
                            <th style="width: 5%">Stock Avl</th>
                            <th style="width: 5%">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txitemsearch" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" OnClientItemSelected="ItemSelected" ServiceMethod="GetItemList" TargetControlID="txitemsearch">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" Width="5em" runat="server"></asp:TextBox>
                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>

                            <td class="drop-down">
                                <asp:DropDownList ID="cbwhs" onchange="ShowProgress();" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbbin" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="drop-down-date">
                                <asp:TextBox ID="dtexp" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="dtexp_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtexp">
                                </asp:CalendarExtender>

                            </td>
                            <td class="drop-down-date">
                                <asp:TextBox ID="dtprod" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="dtprod_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtprod">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbstock" runat="server" BackColor="Yellow" Font-Bold="True" Font-Size="Medium" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btadd_Click" Style="height: 30px">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-sm-12">
                    <asp:GridView ID="grd" CssClass="table table-bordered table-condensed input-sm" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expire Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbexpdate" runat="server" Text='<%#Eval("exp_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prod Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbproddate" runat="server" Text='<%#Eval("prod_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <%--  <div class="row margin-bottom">
                <label class="control-label col-sm-1 input-sm">Tallysheet File</label>
                <div class="col-sm-2">
                    <asp:FileUpload ID="fu_tallysheet" CssClass="form-control input-sm" runat="server" />
                </div>
            </div>--%>
            <div class="row margin-bottom center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-success btn-sm" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btnew_Click">New</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btsave" CssClass="btn btn-primary btn-sm" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btsave_Click">Save</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btsavecompleted" CssClass="btn btn-primary btn-sm" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btsavecompleted_Click">Save</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btprint" CssClass="btn btn-info btn-sm" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btprint_Click">Print</asp:LinkButton>&nbsp;
                <asp:Button ID="btrefresh" OnClientClick="ShowProgress();" runat="server" Text="Button" OnClick="btrefresh_Click" Style="display: none" />
                <asp:Button ID="btchanged" runat="server" Style="display: none" OnClientClick="ShowProgress();" OnClick="btchanged_Click" Text="Button" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

