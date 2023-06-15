<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_autopolist.aspx.cs" Inherits="fm_autopolist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Auto PO generation</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
         <div class="divgrid">
             <div class="overflow-y" style="max-height:350px;"> 
                 <asp:GridView ID="grdauto" runat="server" AutoGenerateColumns="False" CssClass="table table-fix table-striped table-hover mygrid">
                    <Columns>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdids" Value='<%# Eval("ids") %>' runat="server" />                    
                             <%# Eval("created_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Order">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                </asp:GridView>
             </div>
        

    </div>
    
        <div class="navi margin-bottom margin-top">
            <asp:Button ID="btpo" runat="server" Text="Create PO To Head Office" CssClass="btn-primary btn btn-create" OnClick="btpo_Click" />
        </div>
    </div>
   
 </asp:Content>

