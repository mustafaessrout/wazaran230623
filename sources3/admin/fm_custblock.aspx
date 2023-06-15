<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_custblock.aspx.cs" Inherits="admin_fm_custblock" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <h3>Customer Block Activated</h3>
        <div class="form-group">
            <div class="col-md-4">
            <label class="control-label">Channel</label>
            <asp:DropDownList ID="cbotlcd" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="control-label">Date Blocked</label>
                <asp:TextBox ID="dtblock" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="dtblock_CalendarExtender" runat="server" TargetControlID="dtblock" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <div class="col-md-2">
                  <label class="control-label">Add</label>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btadd_Click" />
            </div>
        </div>
        <div class="form-group">
          
            <div class="col-md-8 col-md-offset-2">
                <br />
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Channel">
                            <ItemTemplate>
                                <asp:Label ID="lbchannel" runat="server" Text='<%# Eval("fld_valu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Block">
                            <ItemTemplate><%# Eval("fld_desc","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </div>
         </div>
    </div>
</asp:Content>

