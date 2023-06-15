<%@ Page Title="" Language="C#" MasterPageFile="~/adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_destpostpone.aspx.cs" Inherits="admin_fm_destpostpone" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <style>
        .main-content {
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="container">
        <h3 class="divheader">Postpone Destroy Stock
        </h3>
        <img src="../div2.png" class="divid" style="width:100%;"/>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%" CssClass="table" OnRowCommand="gvData_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ref">
                            <ItemTemplate>
                                <asp:Label ID="trnstkno" runat="server" Text='<%# Eval("trnstkno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WHS Code">
                            <ItemTemplate><%# Eval("whs_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="trnstkremark">
                            <ItemTemplate><%# Eval("trnstkremark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="trnstkDate">
                            <ItemTemplate><%# Eval("trnstkDate","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="trn_trnstkdate ">
                            <ItemTemplate>
                                <asp:Label ID="trn_trnstkdate" runat="server" Text=' <%# Eval("trn_trnstkdate","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Postpone (days)">
                            <ItemTemplate>
                                <asp:Label ID="postpone_day" runat="server" Text='<%# Eval("postpone_day") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:CommandField HeaderText="Detail" ShowSelectButton="True" SelectText="detail" />--%>
                        <%--<asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="postpone" />--%>
                        <asp:ButtonField  HeaderText="Detail" ButtonType="Link" CommandName="detail" Text="detail"  />
                        <asp:ButtonField HeaderText="Action" ButtonType="Link" CommandName="postpone" Text="postpone" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width:90%; max-width: 100%;">

        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 id="trnnost" class="modal-title"></h4>
            </div>
            <div class="modal-body">
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grddet" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%" CssClass="table table-bordered" >
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Cd.">
                                    <ItemTemplate>
                                        <asp:Label ID="item_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%# Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded Name">
                                    <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="qty">
                                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unitprice">
                                    <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate><%# Eval("UOM") %></ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate><%# Eval("Amount") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
            </div>
        </div>

        </div>
    </div>
</asp:Content>
