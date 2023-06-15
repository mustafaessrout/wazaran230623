<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_doloading.aspx.cs" Inherits="fm_doloading" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">DO For Loading</div>
    <div class="h-divider"></div>

    <div class="container-fluid margin-bottom padding-bottom">
        <div class="row ">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="overflow-y" style="max-height: 300px;">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" data-table-page="#copytable" CssClass="table  table-page-fix text-center table-fix table-hover table-striped" GridLines="None" AllowPaging="True" PageSize="30">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DO Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdono" runat="server" Text='<%# Eval("do_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DO Date">
                                    <ItemTemplate><%# Eval("do_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salespoint">
                                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Number">
                                    <ItemTemplate><%# Eval("po_no") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle CssClass="row-pop-up" />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                    <div class="table-copy-page-fix" id="copytable"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="margin-bottom margin-top">
            <asp:Label ID="lbstatus" runat="server" Text="LOADING" CssClass="label label-danger"></asp:Label>
        </div>
    </div>


    <div class="divheader subheader  capital top-devider">details information</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="divheader subheader title-sub">Delivery Order No.
                        <asp:Label ID="lbmstdono" runat="server" ForeColor="Red"></asp:Label></div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="clearfix">
                <div class="form-group clearfix col-md-6">
                    <label class="col-sm-2 control-label no-margin-bottom">Source Warehouse</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbwhs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>

                    </div>
                </div>
                <div class="form-group clearfix col-md-6">
                    <label class="col-sm-2 control-label">Delivery Type</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbdeliverytype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbdeliverytype_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="form-group clearfix col-md-6">
                    <label class="col-sm-2 control-label">Expedition</label>
                    <div class="col-sm-10 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbexpedition" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbdeliverytype" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div class="form-group clearfix col-md-6">
                    <label class="col-sm-2 control-label">Driver Name</label>
                    <div class="col-sm-10 ">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txdriver_name" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:DropDownList ID="cbemp_cd" runat="server" OnSelectedIndexChanged="cbemp_cd_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group clearfix col-md-6">
                    <label class="col-sm-2 control-label">Vehicle</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbvehicle" runat="server" OnSelectedIndexChanged="cbemp_cd_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="form-group clearfix col-md-6">
                    <label class="col-sm-2 control-label">Trailer Box</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbtrailerbox" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                    </div>
                </div>
            </div>

            <div class=" margin-top padding-top">
                <table class="table table-striped mygrid">
                    <tr>
                        <th>Item</th>
                        <th>Qty Ordered</th>
                        <th>Stock</th>
                        <th>Action</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <div id="divwidthi">
                            </div>
                            <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitemsearch" UseContextKey="True" CompletionListElementID="divwidthi" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" type="number"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txstock" runat="server" CssClass="form-control input-sm" type="number"></asp:TextBox></td>
                        <td style="width: 80px;">
                            <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn-sm btn btn-add" OnClick="btadd_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="margin-top padding-top">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grddtl" runat="server" CssClass="table table-striped table-hover mygrid" AutoGenerateColumns="False" OnSelectedIndexChanging="grddtl_SelectedIndexChanging" GridLines="None" CellPadding="0" OnRowDataBound="grddtl_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <%# Eval("size")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Order">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbqty_order" runat="server" Style="font-weight: bold; color: blue" Text='<%# Eval("qty_order") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty_order" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Submit By HO">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbqty" runat="server" Style="font-weight: bold; color: blue" Text='<%# Eval("qty") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lbuom" runat="server" Style="font-weight: bold; color: blue" Text='<%# Eval("uom") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Deliver">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:TextBox ID="txqtydeliver" runat="server" Text='<%# Eval("qty") %>' BackColor="#CCFF99"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div class="text-right">
                                            <asp:Label ID="lbltotalqtydeliver" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM Delivery">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" style="background-color:#CCFF99">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From Bin">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cbbin" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Stock"></asp:TemplateField>

                                <asp:CommandField />

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
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

        <div class="row navi  padding-top margin-bottom">
            <asp:Button ID="btprintload" runat="server" Text="Print Loading" CssClass="btn-info btn btn-print" OnClick="btprintload_Click" />
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
            <asp:Button ID="btprintinvo" runat="server" Text="Print Invoice" CssClass="btn-info btn btn-print" OnClick="btprintinvo_Click" />
        </div>
    </div>


</asp:Content>

