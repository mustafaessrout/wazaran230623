<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcustomerlist.aspx.cs" Inherits="fm_mstcustomerlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .build-ico {
            margin-right: 5px;
            position: relative;
        }

            .map-ico:hover,
            .build-ico:hover {
                text-decoration: none;
            }

        .map-ico i {
            font-size: 20px;
            color: #e74c3c;
        }

            .map-ico i:hover,
            .build-ico .fa-building:hover {
                animation-name: rubberBand;
            }

        .build-ico .fa-building {
            font-size: 18px;
            color: #8BCBDE;
        }

        .build-ico .chek {
            position: absolute;
            bottom: -7px;
            right: 0;
        }

        .build-ico .fa-check-circle {
            color: #27AE60;
        }
    </style>
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
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
    <style>
        th {
            position: sticky;
            top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Customer List</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="col-sm-2 control-label">Search Customer</label>
            <div class="col-sm-3">
                <div class="input-group">

                    <asp:TextBox ID="txcustsearch" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField ID="hdcust" runat="server" />
                    <div id="divwidth" class="auto-complate-list"></div>
                    <asp:AutoCompleteExtender ID="txcustsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustsearch" MinimumPrefixLength="1" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="ItemSelected" CompletionListCssClass="auto-complate-list" CompletionListItemCssClass="auto-complate-item" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListElementID="divwidth">
                    </asp:AutoCompleteExtender>
                    <div class="input-group-btn">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                    </div>

                </div>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 overflow-y" style="max-height: 340px">
                <asp:GridView ID="grd" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" AllowPaging="True" CellPadding="0" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing1" OnRowUpdating="grd_RowUpdating">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>' CssClass="uppercase"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <p class="no-margin uppercase"><%# Eval("cust_nm") %></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Shortname" >
                                <ItemTemplate><p class="no-margin uppercase"><%# Eval("cust_sn") %></p></ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Arabic">
                            <ItemTemplate><%# Eval("cust_arabic") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <p class="no-margin ellapsis2 uppercase" style="width: 60px;"><%# Eval("addr") %></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="City" >
                                <ItemTemplate><p class="no-margin uppercase"><%# Eval("city_cd") %></p></ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <p class="no-margin uppercase"><%# Eval("createdt","{0:d/M/yyyy}") %></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <p class="no-margin uppercase"><%# Eval("cust_sta_nm") %></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tax No">
                            <ItemTemplate>
                                <p class="no-margin uppercase"><%# Eval("tax_no") %></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR ID">
                            <ItemTemplate>
                                <p class="no-margin uppercase"><%# Eval("cr_no") %></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Latitude">
                            <ItemTemplate>
                                <div title="<%# Eval("latitude") %>" class="ellapsis2 uppercase" style="width: 50px;"><%# Eval("latitude") %></div>
                            </ItemTemplate>
                            <%--<EditItemTemplate>
                                    <asp:TextBox ID="txlatitude" runat="server" Text='<%# Eval("latitude") %>' CssClass="form-control input-sm"></asp:TextBox></EditItemTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Longitude">
                            <ItemTemplate>
                                <div title="<%# Eval("longitude") %>" class="ellapsis2 uppercase" style="width: 50px;"><%# Eval("longitude") %></div>
                            </ItemTemplate>
                            <%-- <EditItemTemplate>
                                    <asp:TextBox ID="txlongitude" runat="server" Text='<%# Eval("longitude") %>' CssClass="form-control input-sm"></asp:TextBox></EditItemTemplate>--%>

                        </asp:TemplateField>
                        <%--  <asp:TemplateField HeaderText="Created Date">
                                <ItemTemplate>--%>
                        <%--diisi tanggal pembuatan customer--%>

                        <%--</ItemTemplate>
                            </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Salesman Name">
                                <ItemTemplate>--%>
                        <%--diisi nama salesman + employ code--%>
                        <%--<div class="ellapsis" title=" lorem ipsum - ep21231" style="width:90px;">
                                        
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <div style="width: 50px;">
                                    <a class="example-image-link build-ico " href="/images/customer/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                        <asp:Label ID="lbfileloc" runat="server">
                                                <%--<img src="shop.png" width="20" height="20" />--%>
                                                 <i class="fa fa-building animated" aria-hidden="true"></i>
                                        </asp:Label>
                                        <%-- <asp:Label ID="chkgr" CssClass="chek" runat="server"><i class="fa fa-check-circle" aria-hidden="true"></i></asp:Label>--%>
                                    </a>
                                    <a class="map-ico" href="javascript:popupwindow('lookup_map.aspx?la=<%# Eval("latitude")%>&at=<%# Eval("longitude")%>&ct=<%# Eval("cust_nm") %>');">
                                        <%--<img src="gmap.png" width="20" height="20" />--%>
                                        <i class="fa fa-map-marker animated" aria-hidden="true"></i>
                                    </a>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate><%#Eval("salesman_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate><a href="fm_customerentry.aspx?cust=<%# Eval("cust_cd") %>" onclick="ShowProgress();" class="btn-sm btn-block btn btn-success btn-update">Edit</a></ItemTemplate>
                        </asp:TemplateField>
                        <%--  <asp:CommandField ShowSelectButton="True" SelectText="Deactivated" ShowEditButton="True" />--%>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

            </div>
        </div>
        <div class="h-divider"></div>
        <div class="row navi margin-bottom">
            <asp:Button ID="btnew" runat="server" OnClientClick="ShowProgress();" Text="New" CssClass="btn-success btn btn-add btn-sm" OnClick="btnew_Click" />
            <asp:Button ID="btprint" runat="server" Text="Print" OnClientClick="ShowProgress();" CssClass="btn-info btn btn-print btn-sm" OnClick="btprint_Click" />
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

