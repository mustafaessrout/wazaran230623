<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_discountlist.aspx.cs" Inherits="fm_discountlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">List Of Discount </div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row divgrid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grddiscount" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" OnRowEditing="grddiscount_RowEditing" OnRowCancelingEdit="grddiscount_RowCancelingEdit" OnRowUpdating="grddiscount_RowUpdating" AllowPaging="True" CellPadding="0"  GridLines="None" OnSelectedIndexChanging="grddiscount_SelectedIndexChanging">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Discount Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Point">
                                <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <%# Eval("start_dt") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate><%# Eval("end_dt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbpropno" runat="server" Text='<%# Eval("proposal_no") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txpropno" runat="server" Width="200" BackColor="AliceBlue"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Action" ShowSelectButton="True" ShowEditButton="True" EditText="Add Proposal No." />
                        </Columns>

                         <EditRowStyle CssClass="table-edit"/>
                         <FooterStyle CssClass="table-footer" />
                         <HeaderStyle CssClass="table-header"/>
                         <PagerStyle CssClass="table-page" />
                         <RowStyle  />
                         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <SortedAscendingCellStyle BackColor="#E9E7E2" />
                         <SortedAscendingHeaderStyle BackColor="#506C8C" />
                         <SortedDescendingCellStyle BackColor="#FFFDF8" />
                         <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
       
        </div>
    </div>
    
</asp:Content>

