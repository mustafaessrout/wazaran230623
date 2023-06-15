<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trn_fm_supplier.aspx.cs" Inherits="trn_fm_supplier" MasterPageFile="~/master/homaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <%-- <link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <script>
        
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
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        $(document).ready(function () {
            //HideProgress();
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('lookup_mst_Supplier.aspx', 'xtf', '900', '500');
                $(this).removeClass('optional').addClass('selected');
            });

        });

        function PopupCenter(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }
        }
            function SelectData(sVal) {
                $get('<%=hdfSupplier_cdValue.ClientID%>').value = sVal;
            $get('<%=txtSupplier.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">Supplier</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Supplier Code</label>
            <div class="col-md-2">
                <div class="input-group">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfOldFileName" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtSupplier" CssClass="form-control input-group-sm" Enabled="true" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label col-md-1">Name</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfSupplier_cdValue" runat="server"></asp:HiddenField>
                        <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control "></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Company Registration</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCompanyRegistration" runat="server" CssClass="form-control "></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Supplier Tax Number</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtSupplierTax_no" runat="server" CssClass="form-control " AutoPostBack="True" OnTextChanged="txtSupplierTax_no_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>



        <div class="form-group">
            <label class="control-label col-md-1">Contact Number</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control "></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Address</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtSupplierAddress" runat="server" CssClass="form-control "></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">City</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control "></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Country Name</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="overflow-y relative" style="max-height: 250px;">
                                <asp:GridView ID="grd"
                                    runat="server" CssClass="mGrid" data-table-page="#ss"
                                    AutoGenerateColumns="False" AllowPaging="True"
                                    OnPageIndexChanging="grd_PageIndexChanging" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="0" GridLines="None">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Supplier S#">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdfSupplier_cd" runat="server" Value='<%#Eval("supplier_cd") %>' />
                                                <asp:Label ID="lblSupplier_cd" runat="server" Text='<%#Eval("supplier_cd")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("supplier_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Registration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyRegistration" runat="server" Text='<%#Eval("companyRegistration") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Tax No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierTax_no" runat="server" Text='<%#Eval("supplierTax_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("AddressValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="true" ShowDeleteButton="true" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="table-copy-page-fix" id="ss"></div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSupplierTax_no" EventName="TextChanged" />
                    </Triggers>

                </asp:UpdatePanel>

            </div>
        </div>

        <div class="row">
            <div class="form-group">
                <div class="navi margin-bottom padding-bottom margin-top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <%--<i class="fa fa-save">&nbsp;Save</i>--%>
                            <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                            <asp:LinkButton ID="btsave" runat="server" Style="font-size: 18px;" CssClass="btn btn-danger fa fa-save" Text="Save" OnClick="btsave_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-warning" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                            <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" Style="display: none" />

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
