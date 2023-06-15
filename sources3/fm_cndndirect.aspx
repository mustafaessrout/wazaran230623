<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cndndirect.aspx.cs" Inherits="fm_cndndirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function EmployeeSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
            $get('<%=btlookup.ClientID%>').click();
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btlookupcust.ClientID%>').click();

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

        function selectcn(val) {
            $get('<%=hdcn.ClientID%>').value = val;
            $get('<%=btcn.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdemp" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdapproval" runat="server" />
    <asp:HiddenField ID="hdcn" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Credit / Debit Note direct to Employee</h4>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <label class="control-label col-md-1">CNDN No</label>
            <div class="col-md-2">
                <div class="input-group">
                    <asp:TextBox ID="txcndnno" runat="server" CssClass="form-control input-group-sm"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearchcndn" CssClass="btn btn-primary" runat="server" OnClick="btsearchcndn_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label col-md-1">Employee</label>
            <div class="col-md-3">
                <asp:TextBox ID="txemployeesearch" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:AutoCompleteExtender ServiceMethod="GetEmployeeList" EnableCaching="false" FirstRowSelected="false" UseContextKey="true" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="EmployeeSelected" ID="txemployeesearch_AutoCompleteExtender" runat="server" TargetControlID="txemployeesearch">
                </asp:AutoCompleteExtender>
            </div>
            <div class="col-md-4" style="margin-top: 0px">
                <table class="table table-responsive table-bordered table-condensed" style="font-weight: bold; margin-top: 0px">
                    <tr>
                        <td style="background-color: yellow">Job Title</td>
                        <td>
                            <asp:Label ID="lbjobtitle" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="background-color: yellow">Salespoint</td>
                        <td>
                            <asp:Label ID="lbsalespoint" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label col-md-1">CNDN</label>
            <div class="col-md-2 drop-down">
                <asp:DropDownList ID="cbcndn" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbcndn_SelectedIndexChanged" ></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Type Of</label>
            <div class="col-md-2 drop-down">
                <asp:DropDownList ID="cbtype" CssClass="form-control" onchange="javascript:ShowProgress();" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">
                    <asp:ListItem Value="D">Direct Credit/Debit</asp:ListItem>
                    <asp:ListItem Value="T">Transfer Balance From Customer</asp:ListItem>
                </asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Customer</label>
            <div class="col-md-4">
                <asp:TextBox ID="txcustomer" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" FirstRowSelected="false" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" UseContextKey="true" ServiceMethod="GetCustomerList" OnClientItemSelected="CustSelected" runat="server" TargetControlID="txcustomer">
                </asp:AutoCompleteExtender>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label col-md-1">HO Ref No.</label>
            <div class="col-md-2 require">
                  <asp:TextBox ID="txhoref" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txhoref_TextChanged"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom" >
            <label class="control-label col-md-1">Invoice</label>
            <div class="col-md-5">
                <asp:GridView ID="grd" CssClass="table table-condensed table-bordered table-responsive" runat="server" AutoGenerateColumns="False" EmptyDataText="No invoice found" ShowHeaderWhenEmpty="True" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit" ShowFooter="True" BorderStyle="Solid">
                    <Columns>
                        <asp:TemplateField HeaderText="Sys No">
                            <ItemTemplate>
                                <asp:Label ID="lbinvoice" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Man No">
                            <ItemTemplate><%#Eval("manual_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate><%#Eval("balance") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt CNDN">
                            <ItemTemplate>
                                <%#Eval("amtcndn") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txcndnamt" runat="server" Text='<%#Eval("amtcndn") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotcndn" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                    <EmptyDataRowStyle BackColor="Yellow" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" BorderColor="Black" />
                    <FooterStyle BackColor="Yellow" Font-Bold="True" Font-Size="Large" />
                </asp:GridView>
            </div>
            <label class="control-label col-md-1">Document</label>
            <div class="col-md-4">
                <asp:FileUpload ID="fupl" CssClass="form-control" runat="server" />
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label col-md-1">Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtcndn" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtcndn_CalendarExtender" runat="server" CssClass="black" Format="d/M/yyyy" TargetControlID="dtcndn">
                </asp:CalendarExtender>
            </div>

            <label class="control-label col-md-1">Reason</label>
            <div class="col-md-3 drop-down">
                <asp:DropDownList ID="cbreason" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Manual No</label>
            <div class="col-md-3 require">
                <asp:TextBox ID="txmanualno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label col-md-1">VAT/Non VAT</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbvat" CssClass="form-control" runat="server">
                    <asp:ListItem Value="N">Non VAT</asp:ListItem>
                    <asp:ListItem Value="V">VAT</asp:ListItem>
                </asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Amount</label>
            <div class="col-md-2">
                <asp:TextBox ID="txamt" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">Approval</label>
            <div class="col-md-2">
                <asp:Label ID="lbapproval" CssClass="form-control" runat="server" Text=""></asp:Label>
            </div>
             <label class="control-label col-md-1">Status</label>
            <div class="col-md-2">
                <asp:Label ID="lbstatus" CssClass="form-control" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <div class="center">
                <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none" />
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" CssClass="btn btn-info" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-warning" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:Button ID="btlookupcust" runat="server" OnClick="btlookupcust_Click" Text="Button" style="display:none" />
                <asp:Button ID="btcn" runat="server" OnClick="btcn_Click" Text="Button" style="display:none" />
            </div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

