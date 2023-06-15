<%@ Page Title="" Language="C#" MasterPageFile="~/trn/trn.master" AutoEventWireup="true" CodeFile="fm_po_transaction.aspx.cs" Inherits="fm_po_transaction" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <%-- <link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <script>
        
    </script>
    <script>


        function openPopup(url) {
            //alert($(url).attr('href'));
            //alert('http://localhost:29002/' + url)
            console.log(url.length);
            $('#popUpimg').attr('src', 'http://' + window.location.host + '/' + url);
            $('#btnPopup').click();

        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();

        }
        function ConfirmMessage() {
            var form = this;
            //e.preventDefault();
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this imaginary file!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, I am sure!',
                cancelButtonText: "No, cancel it!",
                closeOnConfirm: false,
                closeOnCancel: false
            },
            function (isConfirm) {
                if (isConfirm) {
                    swal({
                        title: 'Shortlisted!',
                        text: 'Candidates are successfully shortlisted!',
                        type: 'success'
                    }, function () {
                        //form.submit();
                    });

                } else {
                    swal("Cancelled", "Your imaginary file is safe :)", "error");
                }
            });
        }
    </script>
    <style>
        .form-horizontal.required .form-control {
            content: "*";
            color: red;
        }

        .mygrid thead th, .mygrid tbody th {
            background-color: #5D7B9D !important;
        }

        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: 10px;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            width: 250px;
            padding: 0px;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
            width: 250px;
        }

        #divwidth {
            width: 250px !important;
        }

            #divwidth div {
                width: 250px !important;
            }

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

        .circleBase {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
        }

        .type1 {
            width: 20px;
            height: 20px;
            display: inline-block;
            /*background: yellow;*/
        }

            .type1.green {
            }

            .type1.red {
            }

        .lblHOStat {
            vertical-align: top;
            line-height: 20px;
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <asp:HiddenField ID="hdAssecode" runat="server"></asp:HiddenField>

    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">HO SO Transaction</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Branch Name</label>
            <div class="col-md-2  drop-down require">

                <asp:DropDownList ID="cbsalespoint" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                </asp:DropDownList>

            </div>
            <label class="control-label col-md-1">PO Status</label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlPOStatus" Enabled="false" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPOStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--<label class="control-label col-md-1">Branch Status</label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <div id="dvHOStatusValue" class="circleBase type1" runat="server"></div>
                        <asp:Label ID="lblHOStat" Text="Connected" CssClass="lblHOStat text-bold" runat="server" />
                        <asp:Button ID="btnRefesh" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btSearchHO_Click" Text="Refresh" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>--%>

            <label class="control-label col-md-1">Date</label>
            <div class="col-md-2  drop-down require">
                <asp:TextBox ID="dtdo" runat="server" CssClass="makeitreadonly ro form-control input-sm" Enabled="false"></asp:TextBox>
                <asp:CalendarExtender CssClass="date" ID="dtdo_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdo">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">
                Delivery From warehouse
            </label>
            <div class="col-md-2  drop-down require">
                <asp:DropDownList ID="cbwarehouse" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwarehouse_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">
                Manual Invoice
            </label>
            <div class="col-md-2  drop-down require">
                <asp:TextBox ID="txmanualinvoice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbcap" runat="server" CssClass="control-label col-md-1"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbwarehouse" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="col-md-2  drop-down require ">
                <asp:TextBox ID="txgdn" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">
                Expedition Type
            </label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbexpedition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexpedition_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">
                Expedition Company
            </label>
            <div class="col-md-2  drop-down require">
                <asp:DropDownList ID="cbcompany" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">
                Driver
            </label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="cbdriverPanel" CssClass="drop-down">
                            <asp:DropDownList ID="cbdriver" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:TextBox ID="txdrivername" runat="server" CssClass="form-control"></asp:TextBox>

            </div>
            <label class="control-label col-md-1">
                Trella/Truck/Van
            </label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server" class="drop-down">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbtrella" runat="server" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">
                Trailer Box
            </label>
            <div class="col-md-2  drop-down require">
                <asp:DropDownList ID="cbbox" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfBranchConnected" runat="server" />
                        <asp:HiddenField ID="hdfPO" runat="server" />
                        <asp:HiddenField ID="isUpdatePO" runat="server" />
                        <div class="table-page-fixer">
                            <div class="overflow-y relative" >
                                <asp:GridView ID="grdPO"
                                    runat="server"  data-table-page="#ss1" CssClass="table table-striped table-page-fix  table-fix mygrid" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                    AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanged = "OnSelectedIndexChanged" OnSelectedIndexChanging="grdPO_SelectedIndexChanging"
                                    OnPageIndexChanging="grdPO_PageIndexChanging" CellPadding="0" GridLines="Both" PageSize="10">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="PO Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPO" runat="server" Text='<%#Eval("po_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="true" />
                                    </Columns>
                                   
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle />
                                    <SelectedRowStyle CssClass="table-edit"  />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="table-copy-page-fix" id="ss1"></div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
        <div class="clearfix margin-bottom">
            <div class="col-md-12">
                <table class="table mygrid">
                    <tbody>
                        <tr>
                            <th>PO Number</th>
                            <th id="headerItem" runat="server">Item</th>
                            <th>Unit Price</th>
                            <th>Qty</th>
                            <th id="headerBtAdd" runat="server">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txpono" runat="server" CssClass="form-control " Enabled="False"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txitemname" OnTextChanged="txitemname_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <div id="divwidthi"></div>

                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitemname_AutoCompleteExtender" runat="server" ServiceMethod="GetItemList" TargetControlID="txitemname" UseContextKey="True" OnClientItemSelected="ItemSelected" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthi">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hditem" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:Label runat="server" ID="lblUnitPrice" Text="" CssClass="form-control input-sm" ></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control" type="number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txqty"
                                            FilterType="Numbers" ValidChars="0123456789" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>

                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btadd" runat="server" CssClass="btn btn-block btn-success btn-sm" OnClick="btadd_Click" Text="Add" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="overflow-y" style="max-height:380px;">
                                <asp:GridView ID="grd"
                                    runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss"
                                    AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanging="grd_SelectedIndexChanging"
                                    OnPageIndexChanging="grd_PageIndexChanging" CellPadding="0" OnRowDeleting="grd_RowDeleting" GridLines="Both" PageSize="10">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <%-- <asp:TemplateField HeaderText="PO Number">
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblPO_No" runat="server" Text='<%#Eval("po_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                 <asp:HiddenField ID="hdfdo_no" runat="server" Value='<%#Eval("do_no") %>' />
                                                <asp:HiddenField ID="hdfItem_cd" runat="server" Value='<%#Eval("item_cd") %>' />
                                                <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("item_nm")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSize" runat="server" Text='<%#Eval("size") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branded">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbranded_nm" runat="server" Text='<%#Eval("branded_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit_price" runat="server" Text='<%#Eval("unitprice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubtotal" runat="server" Text='<%#Eval("subtotal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:CommandField ShowDeleteButton="true" ShowSelectButton="True" />
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
                        <asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                        <%--<asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbEmp_type" EventName="SelectedIndexChanged" />--%>
                    </Triggers>

                </asp:UpdatePanel>

            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <div class="navi margin-bottom padding-bottom margin-top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btsave" runat="server" Text="Save" OnClick="btsave_Click" CssClass="btn btn-warning btn-save" />
                            <asp:Button ID="btprint" runat="server" Text="Print" OnClick="btprint_Click" CssClass="btn btn-info btn-print" />
                            <asp:Button ID="btnSummary" runat="server" Text="Summary" OnClick="btnSummary_Click" CssClass="btn btn-info btn-print" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Image</h4>
                </div>
                <div class="modal-body">
                    <img id="popUpimg" src="" width="200" height="200" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

