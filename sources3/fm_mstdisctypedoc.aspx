<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstdisctypedoc.aspx.cs" Inherits="fm_mstdisctypedoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Document Assignment related discount</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row clearfix">
            <div class="col-sm-11 no-padding">
                <div class="clearfix col-sm-3 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Promo Code</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbpromo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbdisctype_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-sm-3 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Promo Type</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                               <asp:DropDownList ID="cbpromotype" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbpromotype_SelectedIndexChanged">
                        </asp:DropDownList>
                           </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbpromo" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-3 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Document</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbdocument" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-sm-3 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">DIC</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbdic" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-1 no-padding">
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-success btn-sm btn-add" OnClick="btadd_Click" />
            </div>
           
        </div>
    </div>

    
    <div class="h-divider"></div>

    <div class="row margin-bottom">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" CellPadding="0"  GridLines="None"  OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging">
            <AlternatingRowStyle  />
            <Columns>
                <asp:TemplateField HeaderText="Group Promo ABV">
                    <ItemTemplate>
                        <asp:Label ID="lbdisctype" runat="server" Text='<%# Eval("promo_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grp Promo Name">
                    <ItemTemplate><%# Eval("promo_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Promo Type ABV">
                    <ItemTemplate>
                        <asp:Label ID="lbpromotype" runat="server" Text='<%# Eval("promo_typ") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Promo Type Name">
                    <ItemTemplate><%# Eval("promotyp_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doc Code">
                    <ItemTemplate>
                        <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doc Name">
                    <ItemTemplate><%# Eval("doc_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dept. In Charge">
                    <ItemTemplate><%# Eval("dic_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DEL" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle CssClass="table-edit" />
            <FooterStyle CssClass="table-footer"/>
            <HeaderStyle CssClass="table-header" />
            <PagerStyle CssClass="table-page"/>
            <RowStyle />
            <SelectedRowStyle CssClass="table-edit" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cbpromotype" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        
    </div>
</asp:Content>

