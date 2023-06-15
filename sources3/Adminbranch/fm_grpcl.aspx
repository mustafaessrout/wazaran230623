<%@ Page Title="" Language="C#" MasterPageFile="~/adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_grpcl.aspx.cs" Inherits="admin_fm_grpcl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
  <%--  <div class="container">--%>
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Group Credit Limit Setup</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Group</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control-static"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">CL</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txcreditlimit" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-primary" OnClick="btsave_Click">Save</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mygrid" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Group">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusgrcd" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name">
                                <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Limit">
                                <ItemTemplate><%# Eval("credit_limit") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="ACTION" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-12">
                <div class="navi">
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-success btn-lg">Print</asp:LinkButton>
                </div>
            </div>
        </div>
  <%--  </div>--%>
</asp:Content>

