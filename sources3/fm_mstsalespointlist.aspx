<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstsalespointlist.aspx.cs" Inherits="fm_mstsalespointlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <style>
        .custom-tabel > tbody > tr > td:first-child{
            width: 150px;
        }
        .custom-tabel > tbody > tr > td:nth-child(2){
            width: 250px;
        }
        .custom-tabel > tbody > tr > td:nth-child(3){
            width: 200px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Sales Point</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row overflow-x">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False"  AllowPaging="True" OnPageIndexChanging="grdsp_PageIndexChanging" CellPadding="4" GridLines="None" OnRowCancelingEdit="grdsp_RowCancelingEdit1" OnRowEditing="grdsp_RowEditing" OnRowUpdating="grdsp_RowUpdating" CssClass="table table-striped  table-hover mygrid custom-tabel">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Salespoint Code" ControlStyle-Width="100px">
                                <ItemTemplate><%# Eval("salespointcd") %>
                            <asp:HiddenField runat="server" id="hdsalespointcd" value='<%# Eval("salespointcd") %>'></asp:HiddenField>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Name">
                                <ItemTemplate>
                                    <%# Eval("salespoint_nm") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txsalespoint_nm" runat="server" CssClass="form-control input-sm input-foc" Text=<%# Eval("salespoint_nm") %>></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Name">
                                 <ItemTemplate><%# Eval("salespoint_sn") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txsalespoint_sn" runat="server" CssClass="form-control input-sm" Text=<%# Eval("salespoint_sn") %> ></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%# Eval("salespoint_typ_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Area"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure you want to update?'); "></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="Linkbutton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer"/>
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle/>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div class="navi row margin-bottom">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
        </div>
    </div>
    
</asp:Content>

