<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmcustomercategory_doc.aspx.cs" Inherits="frmcustomercategory_doc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="divheader">Document Customer Category</div>
    <div class="h-divider"></div>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div class="container-fluid ">
                <div class="row">
                    <div class="clearfix form-group col-sm-5">
                        <label class="control-label col-md-2 col-sm-3">Channel</label>
                        <div class="col-md-10 col-sm-9 drop-down">
                            <asp:DropDownList ID="cbcustcate_cd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcustcate_cd_SelectedIndexChanged" CssClass="form-control" ></asp:DropDownList>
                     
                        </div> 
                    </div>
                    <div class="clearfix form-group col-sm-5">
                        <label class="control-label col-md-2 col-sm-3">Document</label>   
                        <div class="col-md-10 col-sm-9 drop-down">
                                <asp:DropDownList ID="cbdoc_cd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcustcate_cd_SelectedIndexChanged" CssClass="form-control" ></asp:DropDownList>
                        </div>   
                
                    </div>
                    <div class="clearfix form-group col-sm-2 ">
                        <div class="col-sm-12">
                            <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-block btn-success btn-add" OnClick="btadd_Click" />
                        </div>
                    </div>
                </div>
            </div>

                <div class="h-divider"></div>

                <div class="container-fluid padding-bottom">
                    <div class="row">
                        <div class="divgrid overflow-y" style="width:100%;max-height:335px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnRowDeleting="grd_RowDeleting" CssClass="table table-striped table-hover mygrid table-fix">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Doc Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbdoc_cd" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc Name">
                                            <ItemTemplate>
                                                <%# Eval("doc_nm") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcustcate_cd" runat="server" Text='<%# Eval("custcate_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" Text="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-edit" />
                                    <RowStyle />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

