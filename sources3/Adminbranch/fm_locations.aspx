<%@ Page Title="" Language="C#" MasterPageFile="~/adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_locations.aspx.cs" Inherits="fm_locations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="../css/newbootstrap.css" rel="stylesheet" />
    <script src="../css/jquery-1.9.1.js"></script>--%>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <%--<div class="container">--%>
    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">Master Locations</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Loc Code</label>
            <div class="col-md-2">
                <asp:TextBox ID="txlocno" CssClass="form-control ro" onkeydown="return false;" onpaste="return false;" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">

            <label class="control-label col-md-1">Location Type</label>
            <div class="col-md-2">
                <asp:RadioButtonList ID="rbloc" AutoPostBack="true" runat="server" CssClass="form-control" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblock_SelectedIndexChanged">
                    <asp:ListItem Value="city">City</asp:ListItem>
                    <asp:ListItem Value="dist">District</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <label class="control-label col-md-1">City</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbcity" CssClass="form-control ro" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cbcity_SelectedIndexChanged"></asp:DropDownList>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rbloc" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbcity" EventName="SelectedIndexChanged" />                        
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Location Name</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txloc_nm" CssClass="form-control ro" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btsave" CssClass="btn btn-primary" runat="server" OnClick="btsave_Click">SAVE</asp:LinkButton>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <div class="overflow-y" style="width: 100%; max-height: 360px;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" class="mygrid" OnRowDeleting="grd_RowDeleting" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Location Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloc" runat="server" Text='<%# Eval("loc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location Name">
                                        <ItemTemplate><%# Eval("loc_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
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
        </div>

        <div class="text-center">
            <asp:LinkButton ID="btnew" CssClass="btn btn-success" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
        </div>
    </div>
    <%-- </div>--%>
</asp:Content>

