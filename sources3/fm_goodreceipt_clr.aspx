<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_goodreceipt_clr.aspx.cs" Inherits="fm_goodreceipt_clr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <div class="alert alert-info text-bold">Stock in without GDN Clereance</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" AllowPaging="True" EmptyDataText="No GRN found  for clreareance !" OnSelectedIndexChanging="grd_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="GRN No.">
                            <ItemTemplate>
                                <asp:Label ID="lbreceiptno" runat="server" Text='<%#Eval("receipt_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GRN Date">
                            <ItemTemplate><%#Eval("register_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Doc Whs Date">
                            <ItemTemplate><%#Eval("receipt_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GDN No">
                            <ItemTemplate>
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbnav" AutoPostBack="true" onchange="ShowProgress();" OnSelectedIndexChanged="cbnav_SelectedIndexChanged" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Driver">
                            <ItemTemplate><%#Eval("driver_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" SelectText="Preview Data" />
                        <asp:TemplateField HeaderText="Post Data">
                            <ItemTemplate>
                                <asp:LinkButton ID="btpost" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btpost_Click" runat="server">Post Data</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
            <div class="row margin-bottom">
                <div class="alert alert-success">Detail Transaction</div>
                
                <div class="col-sm-6">
                    <div class="alert alert-danger text-bold text-italic">Detail GRN Wazaran</div>
                    <asp:GridView ID="grdgrdtl" CssClass="table table-bordered input-sm alert-danger" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Wazaran">
                                <ItemTemplate>
                                    <%#Eval("item_cd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Navision">
                                <ItemTemplate><%#Eval("item_cd_nav") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate><%#Eval("qty","{0:N2}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
              
                <div class="col-sm-6">
                      <div class="alert alert-warning text-bold text-italic">Detail GDN Factory</div>
                    <asp:GridView ID="grddtlnav" CssClass="table table-bordered input-sm alert-warning" runat="server" AutoGenerateColumns="False" OnRowDataBound="grddtlnav_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Wazaran">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Nav">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemnav" runat="server" Text=' <%#Eval("DO_Item_No_1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemname" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Order">
                                <ItemTemplate><%#Eval("DO_Qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%#Eval("DO_Uom") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" runat="server" OnClientClick="ShowProgress();" OnClick="btnew_Click" CssClass="btn btn-primary btn-sm">New</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

