<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstreasonlist.aspx.cs" Inherits="fm_mstreasonlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .custom-table1>tbody>tr>td:first-child{
            width:120px;
        }
        .custom-table1>tbody>tr>td:nth-child(2),
        .custom-table1>tbody>tr>td:nth-child(3),
        .custom-table1>tbody>tr>td:nth-child(4){
            min-width:150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Master Reason</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="clearfix col-md-6 col-sm-12 form-group">
                            <label class="col-sm-2 control-label">Reason Type </label>
                            <div class="drop-down col-sm-6">
                                <asp:DropDownList ID="cbreason" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbreason_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary btn-search" />
                            </div>
                         </div>
                    </div>

                    <div class="row">
                        <div class="overflow-x">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                     <asp:GridView ID="grdreason" runat="server" CssClass="table table-striped table-hover mygrid custom-table1" AutoGenerateColumns="False" OnRowDeleting="grdreason_RowDeleting" AllowPaging="True" OnPageIndexChanging="grdreason_PageIndexChanging" >
                                         <Columns>
                                             <asp:TemplateField HeaderText="Reason Code" ControlStyle-Width="80px">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbreasoncode" runat="server" Text='<%# Eval("reasn_cd") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Reason Name">
                                                 <ItemTemplate><%# Eval("reasn_nm")%></ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Reason Arabic">
                                                 <ItemTemplate><%# Eval("reasn_arabic") %></ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Reason Type">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbreasontype" runat="server" Text='<%# Eval("reasn_typ_nm") %>'></asp:Label>
                                                      <asp:HiddenField ID="hdreasontype" runat="server" Value='<%# Eval("reasn_typ")%>'/>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField ShowHeader="False">
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?'); "></asp:LinkButton>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="table-page" />
                                     </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
       
                        </div>
                    </div>

                    <div class="row navi margin-bottom margin-top">
                        <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
                    </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     
    
    
</asp:Content>

