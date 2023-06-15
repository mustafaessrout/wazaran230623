<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_setupreturninvoice.aspx.cs" Inherits="fm_setupreturninvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Setup Customer for return must have invoice</h4>
        <div class="h-divider"></div>
        <div class="row margin-bottom">
            <label class="col-md-1">Customer</label>
            <div class="col-md-3">
                <asp:TextBox ID="txcustomersearch" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomersearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" CompletionSetCount="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" UseContextKey="true" OnClientItemSelected="CustSelected" TargetControlID="txcustomersearch">
                </asp:AutoCompleteExtender>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server" OnClientClick="ShowProgress();" OnClick="btadd_Click">ADD</asp:LinkButton>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-md-12">
                <asp:GridView ID="grd" CssClass="table table-responsive table-bordered table-condensed" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcustcode" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%#Eval("cust_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate><%#Eval("typ_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group">
                            <ItemTemplate><%#Eval("cusgrcd_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
      <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

