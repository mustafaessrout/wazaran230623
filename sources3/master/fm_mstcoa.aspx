<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_mstcoa.aspx.cs" Inherits="master_fm_mstcoa" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Branches Chart Of Account</h4>
        <div class="h-divider"></div>
        <div class="form-group">
           <%-- <label class="col-md-1 control-label">COA</label>--%>
            <div class="col-md-7">
                 <asp:TreeView ID="tvwcoa" runat="server" ImageSet="BulletedList" LeafNodeStyle-CssClass="leafNode" NodeStyle-CssClass="treeNode" Width="100%" OnSelectedNodeChanged="tvwcoa_SelectedNodeChanged" ShowLines="True" CssClass="treeview" RootNodeStyle-CssClass="rootNode" SelectedNodeStyle-CssClass="selectNode" PathSeparator="-" Font-Size="X-Small" LineImagesFolder="~/TreeLineImages">
                </asp:TreeView>
            </div>
            <div class="col-md-5">
                <div class="form-group">
                   <label class="control-label col-md-4">COA Code</label>
                   <div class="col-md-8">
                       <%--<asp:DropDownList ID="cbcoacode" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                       <asp:TextBox ID="txcoacode" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>
                </div>
                 <div class="form-group">
                   <label class="control-label col-md-4">Desc</label>
                   <div class="col-md-8">
                   <asp:TextBox ID="txdesc" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>
                </div>
                 <div class="form-group">
                   <label class="control-label col-md-4">Arabic</label>
                   <div class="col-md-8">
                       <asp:TextBox ID="txarabic" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>
                </div>
                 <div class="form-group">
                   <label class="control-label col-md-4">Parent</label>
                   <div class="col-md-8">
                       <asp:DropDownList ID="cbparent" CssClass="form-control" runat="server"></asp:DropDownList>
                   </div>
                </div>
                 <div class="form-group">
                   <label class="control-label col-md-4">Type</label>
                   <div class="col-md-8">
                       <asp:DropDownList ID="cbtype" CssClass="form-control" runat="server"></asp:DropDownList>
                   </div>
                </div>
                <div class="h-divider"></div>
                <div class="form-group">
                    <div class="col-md-12">
                        <div style="text-align:center">
                            <asp:LinkButton ID="btnew" CssClass="btn btn-success" runat="server" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;</i>New</asp:LinkButton>
                            <asp:LinkButton ID="btedit" CssClass="btn btn-warning" runat="server" OnClick="btedit_Click"><i class="fa fa-edit">&nbsp;</i>Edit</asp:LinkButton>
                            <asp:LinkButton ID="btdelete" CssClass="btn btn-danger" runat="server" OnClick="btdelete_Click"><i class="fa fa-eraser">&nbsp;</i>Delete</asp:LinkButton>
                            <asp:ConfirmButtonExtender ID="btdelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure want to deleted?" TargetControlID="btdelete">
                            </asp:ConfirmButtonExtender>
                            <asp:LinkButton ID="btsave" CssClass="btn btn-primary" runat="server" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;</i>Save</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

