<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trn_itemCustomerBlock.aspx.cs" Inherits="trn_itemCustomerBlock" MasterPageFile="~/master/homaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <%-- <link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <script>

</script>
    <style>
        input[type=checkbox][disabled] {
            outline: 1px solid red;
        }

        .circleBase {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
        }

        .type1 {
            width: 20px;
            height: 15px;
            display: inline-block;
            background: red;
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
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        $(document).ready(function () {
            //HideProgress();


        });


        function SelectData(sVal) {


        }
        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">

    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <h4 class="jajarangenjang">[Item-Customer] Blocking for Pcs </h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="col-md-2">Branch</label>
            <div class="col-md-8  ">
                <asp:DropDownList ID="cbsalespoint" runat="server" Width="13em" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>

            </div>

        </div>
        <div class="form-group">
            <label class="col-md-2">Branch Status</label>
            <div class="col-md-2  ">
                <div id="dvHOStatusValue" class="circleBase type1" runat="server"></div>
                <asp:Label ID="lblHOStat" Text="Connected" CssClass="lblHOStat text-bold" runat="server" />
            </div>
            <asp:Button ID="btnRefesh" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btSearchHO_Click" Text="Refresh" />

        </div>

        <div class="form-group">
            <div class="col-md-12">
                <div class="clearfix">
                    <table class="mGrid">
                        <tr>
                            <th>Parent Product</th>
                            <th>Product</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>Customer Group</th>
                            <th>Add</th>
                        </tr>
                        <tr>

                            <td>

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hdfHOConnected" runat="server" />
                                        <asp:DropDownList Style="width: 250px" ID="ddlProduct2" runat="server"  OnSelectedIndexChanged="ddlProduct2_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlProduct" Style="width: 250px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:TextBox ID="dtstart" runat="server"></asp:TextBox>
                                 <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtstart">
                                </asp:CalendarExtender>
                               
                            </td>
                            <td>
                                <asp:TextBox ID="dtEnd" runat="server"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="dtEnd">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCustomer" runat="server">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAdd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="overflow-y relative">
                                <asp:GridView ID="grdHO"
                                    runat="server" CssClass="mGrid" data-table-page="#ss"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="25"
                                    CellPadding="0" GridLines="None" OnSelectedIndexChanging="grdHO_SelectedIndexChanging" OnRowDeleting="grdHO_RowDeleting">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Parent Product">
                                            <ItemTemplate><asp:HiddenField ID="hdfserialNumber" runat="server" Value='<%#Eval("serialNumber")  %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdfblockID" runat="server" Value='<%#Eval("blockID")  %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdfcomp_cd" runat="server" Value='<%#Eval("comp_cd")  %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hdfSalespointcd" runat="server" Value='<%#Eval("salespointcd")  %>'></asp:HiddenField>
                                                <asp:Label ID="lblparentProduct" runat="server" Text='<%#Eval("parentProduct")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproduct" runat="server" Text='<%#Eval("product")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblotlcdValue" runat="server" Text='<%#Eval("otlcdValue")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstart_dt" runat="server" Text='<%# Eval("start_dt", "{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblend_dt" runat="server" Text='<%# Eval("end_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Branch Updated ?">
                                            <ItemTemplate>
                                                <asp:Label ID="lblisBranchUpdate" runat="server" Text='<%# Eval("branchUpdateStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Branch Updated ?">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbDisposal" runat="server"
                                                    Checked='<%# Bind("isBranchUpdate") %>' Enabled="false" />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField SelectText="Delete" ShowDeleteButton="true" />
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSupplierTax_no" EventName="TextChanged" />
                    </Triggers>--%>
                </asp:UpdatePanel>

            </div>
        </div>

        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="overflow-y relative">
                                <asp:GridView ID="grd"
                                    runat="server" CssClass="mGrid" data-table-page="#ss"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="25"
                                    CellPadding="0" GridLines="None" OnRowCommand="grd_RowCommand" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Branch Code">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdfcomp_cd" runat="server" Value='<%#Eval("comp_cd")  %>'></asp:HiddenField>
                                                <asp:Label ID="lblsalespointcd" runat="server" Text='<%#Eval("salespointcd")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>

                                                <asp:Label ID="lblsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Data">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="viewJournal1" runat="server">
                                                        <a href='<%# Eval("branchLink")%>' target="_blank">View Branch</a>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="viewJournal2" runat="server">
                                                        <a href='<%# Eval("branchLink")%>' target="_blank">Update Branch</a>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deactive">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="viewJournal3" runat="server">
                                                        <a href='<%# Eval("branchLink")%>' target="_blank">Deactive Branch</a>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deactive1">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Deactive"
                                                    Text="Deactive1" CommandArgument='<%# Eval("salespointcd") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active1">
                                            <ItemTemplate>
                                                <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="Active"
                                                    Text="Active1" CommandArgument='<%# Eval("salespointcd") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DeactiveRes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeactive1" runat="server" Text="N/A"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActiveRes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActive1" runat="server" Text="N/A"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField />
                                        <asp:CommandField ShowSelectButton="True" SelectText="Update" />
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSupplierTax_no" EventName="TextChanged" />
                    </Triggers>--%>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="navi margin-bottom padding-bottom margin-top">
                <asp:ConfirmButtonExtender ID="btdelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure want to update branch" TargetControlID="btsave">
                </asp:ConfirmButtonExtender>
                <asp:LinkButton ID="btsave" CssClass="btn btn-primary" runat="server" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;</i>Update Branch</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <div class="navi margin-bottom padding-bottom margin-top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <%--<i class="fa fa-save">&nbsp;Save</i>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>--%>
</asp:Content>
