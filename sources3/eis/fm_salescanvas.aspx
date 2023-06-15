<%@ Page Title="" Language="C#" MasterPageFile="~/eis/eis.master" AutoEventWireup="true" CodeFile="fm_salescanvas.aspx.cs" Inherits="eis_fm_salescanvas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Sales Canvasser</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Salespoint</label>
            <div class="col-md-4">
                <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Period</label>
            <div class="col-md-4">
                <asp:DropDownList ID="cbperiod" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click">Search</asp:LinkButton>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp Code">
                            <ItemTemplate><%#Eval("emp_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman Name">
                            <ItemTemplate><%#Eval("fullname") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Van">
                            <ItemTemplate><%#Eval("vhc_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Code"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Qty"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Amt"></asp:TemplateField>
                    </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

