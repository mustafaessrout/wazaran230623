<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_customer.aspx.cs" Inherits="master_fm_customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
   <%-- <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        $(document).ready(function () {
            $('#pnlmsg').hide();
        });

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Customer List</h4>
        <div class="h-divider"></div>
        <div class="form-group">
        <div class="col-md-12">
            <table class="tab tab-container">
                <tr><th>Salespoint</th><th>Action</th></tr>
                <tr><td>
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control"></asp:DropDownList></td>
                  
                    <td>
                        <asp:LinkButton ID="btview" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btview_Click"><span class="fa fa-search"></span></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate><%#Eval("salespoint_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate><%#Eval("cust_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Addr">
                            <ItemTemplate><%#Eval("addr") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Limit">
                            <ItemTemplate><%#Eval("credit_limit") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TOP"><ItemTemplate><%#Eval("payment_term") %></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Channel">
                            <ItemTemplate><%#Eval("otlcd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group">
                            <ItemTemplate><%#Eval("cusgrcd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Map">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkmap" runat="server" ImageUrl="~/gmap.png" NavigateUrl="javascript:window.open('/lookup_map.aspx');" ImageWidth="10px" ImageHeight="10px"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:LinkButton ID="btprint" CssClass="btn btn-primary" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
    </div>
 <%--   <div id="pnlmsg" class="loading-cont">
        <div>
            <img src="/image/loading2.gif" class="spiner" />
        </div>
     </div>--%>
</asp:Content>

