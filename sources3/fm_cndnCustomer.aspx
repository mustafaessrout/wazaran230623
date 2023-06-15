<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" EnableViewState="true" AutoEventWireup="true" CodeFile="fm_cndnCustomer.aspx.cs" Inherits="fm_cndnCustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return;
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btlookup.ClientID%>').click();
        }

        function openwindow() {
            var oNewWindow = window.open("lookup_acccndn.aspx", "lookup", "height=700,width=1200,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        function SelectData(sVal) {
            $get('<%=hdfCNDNID.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }

        function TaxSelected(sender, e) {
            $get('<%=hdtaxall.ClientID%>').value = e.get_value();
            $get('<%=bttax.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdtaxall" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdfCNDNID" runat="server" />
    <asp:HiddenField ID="hdfFileName" runat="server" />
    <asp:HiddenField ID="hdfFileNameExtension" runat="server" />
    <asp:HiddenField ID="hdfUser" runat="server" />
    <div class="alert alert-info text-bold">DN Customer</div>
    <%--<div class="form-horizontal">--%>
    <div class="container">
        <div class="form-group">
            <div class="row">
                <label class="control-label-sm input-sm col-sm-1">Cust DN No</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <%--<div class="input-group-sm">

                        </div>--%>
                        <asp:Label ID="lbsysno" CssClass="form-control input-sm input-group-sm" runat="server" Text=""></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <label class="control-label-sm input-sm col-sm-1">Customer</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txcust" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server" CompletionListCssClass="input-sm"
                        TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true"
                        MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                        ContextKey="true">
                    </asp:AutoCompleteExtender>
                    <div id="divw" class="auto-text-content"></div>
                </div>

                <label class="control-label-sm input-sm col-sm-1">Date</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="dtcndn" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtcndn">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label-sm input-sm col-sm-1">Post Date</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="dtpost" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="dtprop_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpost">
                    </asp:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label-sm input-sm col-sm-1">Operation Type</label>
                <div class="col-sm-2 drop-down require">
                    <asp:DropDownList ID="cbtype" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <label class="control-label-sm input-sm col-sm-1">Tax</label>
                <div class="col-sm-2 drop-down require">
                    <asp:DropDownList ID="cbtax" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbtax_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <label class="control-label-sm input-sm col-sm-1">Amount Inclusive TAX</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
                <label class="control-label-sm input-sm col-sm-1">Ref No</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txtManual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <label class="control-label-sm input-sm col-sm-1">Approved By</label>
                <div class="col-sm-2  drop-down">
                    <asp:DropDownList ID="cbapproval" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbapproval_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <label class="control-label-sm input-sm col-sm-1">Reason</label>
                <div class="col-sm-2 drop-down require">
                    <asp:DropDownList ID="cbreason" CssClass="form-control input-sm" runat="server">
                    </asp:DropDownList>
                </div>
                <label class="control-label-sm input-sm col-sm-1">Remark</label>
                <div class="col-sm-5 require">
                    <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">

                <label class="control-label-sm input-sm col-sm-1">Document</label>
                <div class="col-sm-5 require">
                    <asp:FileUpload ID="upl" CssClass="form-control input-sm" runat="server" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                    OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing"
                    OnRowUpdating="grd_RowUpdating"
                    ForeColor="#333333" OnRowDeleting="grd_RowDeleting" EmptyDataText="No records Found">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="DN No">
                            <ItemTemplate>
                                <asp:Label ID="lblDN_no" runat="server" Text='<%#Eval("cndn_no") %>'></asp:Label>
                                <asp:HiddenField ID="hdfcndncust_sta_id" runat="server" Value='<%#Eval("cndncust_sta_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("statusvalue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesman" runat="server" Text='<%#Eval("salesman") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DN Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblDNAmt" runat="server" Text='<%#Eval("amt") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDNAmt" runat="server" Text='0'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DN Balance">
                            <ItemTemplate>
                                <asp:Label ID="lblbalance" runat="server" Text='<%#Eval("balance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:CommandField ShowCancelButton="true" ShowEditButton="true" ShowDeleteButton="true" />--%>
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
            </div>
        </div>
        <div class="navi row margin-bottom">
            <asp:LinkButton ID="btnNew" runat="server" OnClientClick="ShowProgress();" OnClick="btnNew_Click" CssClass="btn btn-success btn-sm "><span >New </span></asp:LinkButton>
            <%-- <input type="button" id="btHTM" title="Save" class="btn btn-success btn-sm" value="save" />
            <asp:LinkButton ID="btnViewCNDNC" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnViewCNDNC_Click">View DN Customer</asp:LinkButton>
            <asp:LinkButton ID="btnJquery" runat="server" CssClass="btn btn-success btn-sm" Style="display: none"><span >JQuery </span></asp:LinkButton>--%>

            <asp:LinkButton ID="btSave" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btSave_Click">Save CNDN</asp:LinkButton>
            <asp:Button ID="btlookup" OnClientClick="ShowProgress();" runat="server" OnClick="btlookup_Click" Text="Button" Style="display: none" />
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Button" Style="display: none" />
            <asp:LinkButton ID="btDNCustomer" runat="server" CssClass="btn btn-success btn-sm" OnClick="btDNCustomer_Click" Style="display: none">Show</asp:LinkButton>
            <asp:Button ID="bttax" runat="server" OnClick="bttax_Click" Text="Button" Style="display: none" />
            <asp:LinkButton ID="btprint" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>

        </div>
    </div>
    <%--</div>--%>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

