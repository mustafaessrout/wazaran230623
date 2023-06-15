<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_internaltransfer.aspx.cs" Inherits="fm_internaltransfer" %>

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
    </style>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_internalTranfer.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btrefresh.ClientID%>').click();
            <%--$get('<%=txqty.ClientID%>').focus();--%>
        }

        function popupreport() {
            window.open("fm_report.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes", true);
        }
    </script>
    <style type="text/css">
        .divmsg {
            /*position:static;*/
            top: 7%;
            right: 36%;
            left: 55%;
            width: 200px;
            height: 200px;
            position: fixed;
            opacity: 0.9;
            overflow-y: auto;
            /*-webkit-transition: background-color 0;
    transition: background-color 0;*/
        }

        .divhid {
            display: none;
        }

        .divnormal {
            display: normal;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
            font-family: Calibri;
            font-size: small;
        }
    </style>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 70px;
            height: 35px;
        }

            /* Hide default HTML checkbox */
            .switch input {
                display: none;
            }

        /* The slider */
        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 34px;
                width: 34px;
                left: 3px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(20px);
            -ms-transform: translateX(20px);
            transform: translateX(20px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 25px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
            <ContentTemplate>
                Internal Transfer
                <asp:Label ID="lbstatus" runat="server" CssClass="badge danger"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row margin-bottom">
            <div class=" clearfix">
                <div class="margin-bottom clearfix">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <label class="control-label titik-dua col-sm-2">Branc#</label>
                            <div class="col-sm-4 ">
                                <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control input-sm ro" Enabled="false" >
                                </asp:DropDownList>
                            </div>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="margin-bottom clearfix">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <label class="control-label titik-dua col-sm-2">Transfer No.</label>
                            <div class="col-sm-4 ">
                                <div class="input-group">
                                    <asp:TextBox ID="txtransferno" runat="server" CssClass="makeitreadonly ro form-control" Enabled="false"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btsearch2" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                                    </div>
                                </div>
                            </div>

                            <label class="control-label titik-dua col-sm-1">Tab No.</label>

                            <div class="col-sm-4">
                                <div class="input-group">
                                    <asp:TextBox ID="txtabno" runat="server" CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:Button ID="bttabsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="bttabsearch_Click" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" Style="display: none" />
                </div>
                <div class="margin-bottom clearfix">
                    <label class="col-sm-2 control-label titik-dua ">Date</label>
                    <div class="col-sm-9 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dttrf" runat="server" CssClass="makeitreadonly ro form-control" Enabled="false"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dttrf_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dttrf">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>

                <div class="margin-bottom clearfix">
                    <label class="col-sm-2 control-label titik-dua ">Manual No.</label>
                    <div class="col-sm-9 no-padding">
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txmanualno" runat="server" CssClass="makeitreadonly form-control"></asp:TextBox>
                                </div>

                                <asp:Label ID="lbreason" runat="server" Text="Reason" CssClass="col-sm-2 control-label titik-dua"></asp:Label>
                                <div class="col-sm-5 ">
                                    <asp:DropDownList ID="cbreason" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbreason_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="margin-bottom clearfix">
                    <div class="col-sm-2 no-padding ">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbloadingdt" runat="server" Text="Loading Date" CssClass="control-label titik-dua block"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-11 no-padding-left">
                                    <asp:TextBox ID="dtloading" runat="server" CssClass="makeitreadonly form-control ro" Enabled="false"></asp:TextBox>
                                    <asp:CalendarExtender CssClass="date" ID="dtloading_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtloading">
                                    </asp:CalendarExtender>
                                </div>
                                <div class="col-sm-1 no-padding-left text-right checkbox">
                                    <asp:CheckBox ID="cburgent" runat="server" Text="Urgent" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="margin-bottom clearfix">
                    <label class="col-sm-2 control-label titik-dua ">Transfer Type</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="optwhs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="optwhs_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="radio-inline radio no-margin" CellPadding="0" CellSpacing="0">
                                    <asp:ListItem Value="I">Depo To Van</asp:ListItem>
                                    <asp:ListItem Value="O">Depo To Depo</asp:ListItem>
                                    <asp:ListItem Value="V">Van To Depo</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2 text-right" style="display: none;">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <label class=" control-label titik-dua ">Prev stk</label>
                                <label class="switch sw-small">
                                    <asp:CheckBox ID="chprevstk" runat="server" AutoPostBack="true" OnCheckedChanged="chprevstk_CheckedChanged" />
                                    <div class="slider round"></div>
                                </label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="margin-bottom clearfix">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <label class="control-label col-sm-2 titik-dua">Remark</label>
                        <div class="col-sm-4 require">
                            <asp:TextBox ID="txremark" CssClass="form-control require" runat="server"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="clearfix">
                <table class="table table-striped mygrid row-no-padding table-title-left">
                    <tr>
                        <th class="text-left"></th>

                        <th class="text-left">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbtrtx1" runat="server" Text=""></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </th>
                        <th class="text-left">Bin</th>
                        <th class="text-left"></th>
                        <th class="text-left">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbtrtx2" runat="server" Text=""></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </th>
                        <th class="text-left">Bin</th>
                    </tr>
                    <tr>
                        <td style="width: 50px; vertical-align: middle;" class="text-bold">From </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbfrom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbfrom_SelectedIndexChanged" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbbinfrom" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbfrom" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 120px; vertical-align: middle;" class="text-bold">Transfer To <i class="fa fa-arrow-right" aria-hidden="true" style="margin-left: 5px;"></i></td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbto_SelectedIndexChanged" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbbinto" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbto" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row clearfix">
            <table class="table table-striped mygrid row-no-padding table-title-left margin-bottom">
                <tr>
                    <th></th>
                    <th>Item</th>
                    <th>Stk</th>
                    <th></th>
                </tr>
                <tr>
                    <td style="width: 150px; vertical-align: middle;" class="text-bold">Item To Be Transferred</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="text-left">
                            <ContentTemplate>

                                <asp:TextBox ID="txitem" AutoPostBack="true" runat="server" OnTextChanged="txitem_TextChanged" CssClass="form-control col-sm-6 input-sm" Style="border-radius: 4px; display: inline-block; width: calc(60% - 5px); margin-right: 5px;"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" EnableCaching="true" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" OnClientItemSelected="ItemSelected" MinimumPrefixLength="1" CompletionListElementID="divwidthi">
                                </asp:AutoCompleteExtender>
                                <div id="divwidthi"></div>

                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txqty_TextChanged" Style="width: calc(40% - 60px); border-radius: 4px; display: inline-block;" min="0"></asp:TextBox>

                                <asp:HiddenField ID="hditem" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="min-width: 25px; width: 100px;">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txstk" runat="server" CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="min-width: 25px; width: 100px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <asp:Button ID="btsearch" runat="server" Text="Add" CssClass="btn-success btn btn-sm btn-add" OnClick="btsearch_Click" />
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" GridLines="None" CellPadding="0" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDeleting="grd_RowDeleting" OnRowDataBound="grd_RowDataBound" ShowFooter="True" CssClass="table table-striped mygrid">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1%>.</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate>
                                    <%# Eval("branded_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty" runat="server" />
                                    </div>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbstock_qty" runat="server" Text='<%# Eval("stock_qty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbseqID" runat="server" Text='<%# Eval("seqID") %>' Visible="false"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
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
            </asp:UpdatePanel>
        </div>
    </div>


    <div class="navi margin-bottom">
        <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" Style="display: none" />
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
    </div>


    <div class="divmsg loading-cont" id="dvshow">
        <div>
            <i class="fa fa-spinner spiner fa-spin fa-3x fa-fw" aria-hidden="true"></i>
        </div>
    </div>
</asp:Content>

