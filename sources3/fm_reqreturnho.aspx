<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqreturnho.aspx.cs" Inherits="fm_reqreturnho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
    <script>
        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('lookupreqretho.aspx', 'xtf', '900', '500');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Return To HO Request</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Reason</label>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbreason" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary"><i class="fa fa-search"></i></asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Prod Spv</label>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbprodspv" onchange="javascript:ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprodspv_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-5">
                    <asp:GridView ID="grd" runat="server" OnSelectedIndexChanging="grd_SelectedIndexChanging" AutoGenerateColumns="False"
                        CssClass="mGrid" Width="100%" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging" PageSize="5">
                        <Columns>
                            <asp:TemplateField HeaderText="Prod Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblprod_cd" runat="server" Text='<%#Eval("prod_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product Name">
                                <ItemTemplate><%#Eval("prod_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Item List</label>
                <div class="col-md-4">
                </div>
                <div class="col-md-5">
                    <asp:GridView ID="grdItem" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%" CellPadding="0">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate><%#Eval("item_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%#Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand Name">
                                <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-3">Total Retur Value Approved (SAR)</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txamt" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btsaved" CssClass="btn btn-primary" runat="server" OnClick="btsaved_Click">Request Now!</asp:LinkButton>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lbmessage" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="form-group" style="text-align: center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-success" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btnSupItemMapping" CssClass="btn btn-success" runat="server" OnClick="btnSupItemMapping_Click">Prod Spv Item Mapping</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

