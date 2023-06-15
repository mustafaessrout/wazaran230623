<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_requestcndn.aspx.cs" Inherits="fm_requestcndn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function CustomerSelected(sender, e) {
            $get('<%=hdcustomer.ClientID%>').value = e.get_value();
            $get('<%=btsearchcustomer.ClientID%>').click();
        }
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        function SelectData(x) {
            $get('<%=hdcndn.ClientID%>').value = x;
            $get('<%=btlookup.ClientID%>').click();
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
    <asp:HiddenField ID="hdcustomer" runat="server" />
    <asp:HiddenField ID="hdcndn" runat="server" />
    <div class="alert alert-info text-bold">Credit Debit Note Request</div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Cn Dn No.</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txcndnno" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();PopupCenter('lookupcndn.aspx','Lookup',800,600);" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Post Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtpost" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtpost_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtpost">
                </asp:CalendarExtender>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtcndn" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>

            <label class="control-label-sm input-sm col-sm-1">Type</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbtype" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">Customer</label>
            <div class="col-sm-5">
                <asp:TextBox ID="txcustomer" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" CompletionListCssClass="input-sm" FirstRowSelected="false" EnableCaching="false" ShowOnlyCurrentWordInCompletionListItem="true" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" UseContextKey="true" OnClientItemSelected="CustomerSelected" ServiceMethod="GetCustomerList" runat="server" TargetControlID="txcustomer">
                </asp:AutoCompleteExtender>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">VAT Rate</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbvatrate" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Manual No</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txmanualno" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Reason</label>
            <div class="col-sm-2 require drop-down">
                <asp:DropDownList ID="cbreason" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Remark</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <%-- <div class="row margin-bottom">
            
        </div>--%>
        <div class="row margin-bottom alert alert-info">
            <div class="col-sm-12 overflow-y" style="max-height: 360px">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Invoice No">
                            <ItemTemplate>
                                <asp:Label ID="lbinvoiceno" runat="server" Text='<%#Eval("inv_no")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%#Eval("inv_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tot Amount">
                            <ItemTemplate><%#Eval("totamt","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lbinvoicebalance" Font-Bold="true" Font-Size="Medium" runat="server" Text='<%#Eval("inv_balance","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount Inclusive VAT">
                            <ItemTemplate>
                                <asp:Label ID="lbcnamount" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Red" Text='<%#Eval("inv_cnamount") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txamt" Text='<%#Eval("inv_cnamount") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VAT">
                            <ItemTemplate><%#Eval("totalvat","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount Based">
                            <ItemTemplate><%#Eval("based_amt","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">File Cndn</label>
            <div class="col-sm-2 require">
                <asp:FileUpload ID="fupl" CssClass="form-control input-sm" runat="server" />
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" CssClass="btn btn-danger btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:Button ID="btsearchcustomer" OnClientClick="ShowProgress();" Style="display: none" runat="server" Text="Serchcustomer" OnClick="btsearchcustomer_Click" />
                <asp:Button ID="btlookup" Style="display: none" runat="server" OnClick="btlookup_Click" Text="Button" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

