<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_rps.aspx.cs" Inherits="Adminbranch_fm_rps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">RPS</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <label class="control-label col-md-1">Salesman</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged"></asp:DropDownList>
                    
                </div>
                 <label class="control-label col-md-1">Day</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbday" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbday_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing">
                    
                    <Columns>
                        <asp:TemplateField HeaderText="Day">
                            <ItemTemplate>
                                <asp:Label ID="lbday" runat="server" Text='<%#Eval("day_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sequence">
                            <ItemTemplate>
                                <%#Eval("sequenceno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <%#Eval("cust_desc") %>
                                <asp:HiddenField ID="hdcust" Value='<%#Eval("cust_cd") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

