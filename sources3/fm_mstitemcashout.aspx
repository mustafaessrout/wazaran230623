<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstitemcashout.aspx.cs" Inherits="fm_mstitemcashout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Master Item Cash OUT/IN </div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="row">
            <div class="margin-bottom col-md-6">
                <label for="cbitemtype" class="col-sm-2 control-label">Item Type</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbitemtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbitemtype_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="margin-bottom col-md-6">
                <label for="cbroutine" class="col-sm-2 control-label">Routine</label>
                <div class="col-sm-10 drop-down">
                     <asp:DropDownList ID="cbroutine" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbroutine_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="margin-bottom col-md-6">
                    <label for="cbinout" class="col-sm-2 control-label">In/Out</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbinout" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
        </div>
        
        <div class="row margin-top">
            <div class="margin-bottom col-md-6">
                <label for="txitemcode" class="col-sm-2 control-label">Item Code</label>
                <div class="col-sm-10">
                    <asp:Panel runat="server" ID="txitemcodePnl">
                        <asp:TextBox ID="txitemcode" runat="server" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>
                </div>
            </div>
            <div class="margin-bottom col-md-6">
                <label for="txitemname" class="col-sm-2 control-label"> Item Name</label>
                <div class="col-sm-10">
                    <asp:Panel runat="server" ID="txitemnamePnl">
                        <asp:TextBox ID="txitemname" runat="server" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>
                </div>
            </div>
        </div>
       
        <div class="row">
            <div class="divgrid">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                          <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped top-devider" CellPadding="0"  GridLines="None" AllowPaging="True" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowCancelingEdit="grd_RowCancelingEdit" OnPageIndexChanging="grd_PageIndexChanging" OnRowUpdating="grd_RowUpdating">
                            <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("itemco_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# Eval("itemco_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate><%# Eval("cashout_typ") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inout">
                                    <ItemTemplate>
                                        <asp:Label ID="lbinout" runat="server" Text='<%# Eval("inout") %>'></asp:Label></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cbinout" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Routine">
                                    <ItemTemplate>
                                        <asp:Label ID="lbroutine" runat="server" Text='<%# Eval("routine") %>'></asp:Label></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cbroutine" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbitemtype" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbinout" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
      
            </div>
        </div>
        
        <div class="row">
            <div class="h-divider"></div>
            <div class="navi text-center margin-bottom">
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save btn-req" OnClick="btsave_Click" />
                <asp:Button ID="btprint" runat="server" CssClass="btn btn-info btn-print" Text="Print" OnClick="btprint_Click" />
            </div>
        </div>
    </div> 
    
</asp:Content>

