<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_ActiveDriver.aspx.cs" Inherits="fm_ActiveDriver" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Driver List</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix form-group col-md-6 ">
                <label class="col-sm-2 control-label">Driver Types</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbDriver" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbDriver_SelectedIndexChanged">
                        <asp:ListItem Text="Active Driver" Value="ActiveDriver" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="All Driver" Value="AllDriver" ></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False"
                GridLines="None" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging"
                CellPadding="0" CssClass="table table-striped table-hover mygrid">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lbempcd" runat="server" Text='<%# Eval("emp_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><%# Eval("empName") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>

</asp:Content>

