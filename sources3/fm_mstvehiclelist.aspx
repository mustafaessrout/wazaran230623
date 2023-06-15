<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstvehiclelist.aspx.cs" Inherits="fm_mstvehiclelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Vehicle List
    </div>
    <div class="h-divider"></div>
    <div class="container-fluid divgrid">
        <div class="row">
            <asp:GridView ID="grdvehicle" runat="server" CssClass="table table-striped table-hover mygrid" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdvehicle_PageIndexChanging" OnRowEditing="grdvehicle_RowEditing" OnRowCancelingEdit="grdvehicle_RowCancelingEdit" OnRowUpdating="grdvehicle_RowUpdating" CellPadding="0" GridLines="None">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Vehicle Code">
                        <ItemTemplate>
                            <asp:Label ID="lbvhccode" runat="server" Text='<%# Eval("vhc_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><%# Eval("vhc_typ_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Capacity">
                        <ItemTemplate><%# Eval("capacity") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate><%# Eval("uom_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%# Eval("vhc_sta_nm") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Driver/Salesman">
                        <ItemTemplate><%# Eval("emp_nm") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbemployee" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salespoint">
                        <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle  CssClass="table-edit"  />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div class="row navi margin-bottom">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn bt-add" OnClick="btnew_Click" />
            <asp:Button ID="Button1" runat="server" CssClass="btn-info btn btn-print" OnClick="Button1_Click" Text="Print" />
        </div>
    </div>

    
</asp:Content>

