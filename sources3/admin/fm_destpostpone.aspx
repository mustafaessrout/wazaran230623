<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_destpostpone.aspx.cs" Inherits="admin_fm_destpostpone" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <h3 class="divheader">Postpone Destroy Stock</h3>
        <img src="../div2.png" class="divid" style="width:100%;"/>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%" CssClass="table" OnSelectedIndexChanging="grd_SelectedIndexChanging">
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
                        <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="postpone" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
