<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appcreditnote.aspx.cs" Inherits="fm_appcreditnote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  class="divheader">Debit Note / Credit Note Approval</div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="row">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdapp" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="grdapp_RowCancelingEdit" OnRowEditing="grdapp_RowEditing" OnRowUpdating="grdapp_RowUpdating" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grdapp_PageIndexChanging" Width="100%" OnPreRender="grdapp_PreRender" CssClass="table table-hover">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%# Eval("cust_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CN/DN No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbarcn_no" runat="server" Text='<%# Eval("arcn_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <%# Eval("arcn_date","{0:d/M/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <%# Eval("arcn_type") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Note">
                                <ItemTemplate>
                                    <%# Eval("arcn_note") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Creator">
                                <ItemTemplate>
                                    <%# Eval("createdby") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approve/Reject">
                                <ItemTemplate>
                            <%# Eval("status") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cbapp" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CN/DN Ref No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblcndnrefno" runat="server" Text='<%# Eval("cndnrefno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcndnrefno" runat="server" Text='<%# Eval("cndnrefno") %>'></asp:TextBox>
                                </EditItemTemplate>                                     
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="ACTION" ShowEditButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
    <div class="divgrid">

       

    </div>
</asp:Content>

