<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_BAOtherPayment.aspx.cs" Inherits="fm_BAOtherPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <link href="css/styles.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script type="text/javascript">


        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btShowInvoice.ClientID%>').click();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return;
        }
    </script>
    <style type="text/css">
        .switch-slider input {
            opacity: 0;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
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
                height: 18px;
                width: 18px;
                left: 2px;
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
            -webkit-transform: translateX(16px);
            -ms-transform: translateX(16px);
            transform: translateX(16px);
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
    <div class="form-horizontal">
        <h4 class="jajarangenjang">BA Other Payment</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Customer</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdcust" runat="server" />
                                <div class="input-group">
                                    <div>
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btShowInvoice" runat="server" CssClass="btn btn-success" OnClick="btShowInvoice_Click">Show</asp:LinkButton>
                        </div>
                        <div id="divw" class="auto-text-content"></div>
                    </div>
                    <label class="control-label col-md-1">Vat Type</label>
                    <div class="col-md-2  drop-down">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCNDNType" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCNDNType_SelectedIndexChanged">
                                    <asp:ListItem Text="Non Vat" Value="NonVAT" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Vat" Value="VAT"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                            OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing"
                            OnRowUpdating="grd_RowUpdating" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowCommand="grd_RowCommand"
                            OnRowDataBound="grd_RowDataBound" ForeColor="#333333" EmptyDataText="No records Found">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Inv No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontract_no" runat="server" Text='<%#Eval("contract_no") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfsalesman_cd" runat="server" Value='<%#Eval("salesman_cd") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsalesman" runat="server" Text='<%#Eval("salesman") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("balance") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                    <FooterTemplate>
                                        <asp:Label ID="lbTotBalance" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Other Payment">
                                    <ItemTemplate>
                                        <label class="switch-slider" style="position: relative; width: 40px;">
                                            <asp:CheckBox ID="chonepct" runat="server" onclick="javascript:ShowProgress();"
                                                AutoPostBack="True" OnCheckedChanged="chonepct_CheckedChanged" />
                                            <span class="slider round"></span>
                                            <%----%>
                                        </label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOtherPayment" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayment" runat="server" Text='0'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPayment" runat="server" Text='0'></asp:TextBox>

                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbTotPayment" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="true" ShowCancelButton="true" ShowEditButton="true" />
                            </Columns>
                            <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                            <EditRowStyle BackColor="#999999" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

