<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_appreturn.aspx.cs" Inherits="Adminbranch_fm_appreturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Customer Return</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row margin-bottom">
                   <div class="col-md-12">
                       <asp:GridView ID="grd" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" CellPadding="0" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit">
                           <Columns>
                               <asp:TemplateField HeaderText="Return No">
                                   <ItemTemplate>
                                       <asp:Label ID="lbreturno" CssClass="control-label" runat="server" Text='<%#Eval("retur_no") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Date">
                                   <ItemTemplate>
                                       <%#Eval("retur_dt","{0:d/M/yyyy}") %>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Customer">
                                   <ItemTemplate>
                                       <%#Eval("cust_desc") %>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Salesman">
                                   <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Amount">
                                   <ItemTemplate><%#Eval("totamt") %></ItemTemplate>
                                   <EditItemTemplate>
                                       <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                                   </EditItemTemplate>
                               </asp:TemplateField>
                               <asp:CommandField ShowEditButton="True" />
                           </Columns>
                       </asp:GridView>
                   </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

