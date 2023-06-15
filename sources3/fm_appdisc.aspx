<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appdisc.aspx.cs" Inherits="fm_appdisc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Discount Need Approval</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="divgrid row">
            <asp:GridView ID="grdapp" CssClass="table table-striped mygrid row-no-padding" runat="server" AutoGenerateColumns="False" OnRowEditing="grdapp_RowEditing" OnRowUpdating="grdapp_RowUpdating" OnRowCancelingEdit="grdapp_RowCancelingEdit" GridLines="None" OnSelectedIndexChanging="grdapp_SelectedIndexChanging">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Discount Code">
                        <ItemTemplate>
                            <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate><%# Eval("start_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <%# Eval("end_dt","{0:dd-MMM-yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                             <%# Eval("remark") %>
                        </ItemTemplate>
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salespoint">
                        <ItemTemplate>
                            <%# Eval("salespoint_nm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proposal No">
                        <ItemTemplate><%# Eval("proposal_no") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catalog Image">
                        <ItemTemplate>
                       
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:FileUpload ID="upl" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%# Eval("disc_sta_nm") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbstatus" runat="server" BackColor="AliceBlue" CssClass="form-control input-sm"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField EditText="App/Reject" HeaderText="Action" ShowEditButton="True" SelectText="Detail" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle CssClass="table-edit"/>
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle/>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
    
</asp:Content>

